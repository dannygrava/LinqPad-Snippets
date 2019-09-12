<Query Kind="Program" />

void Main()
{
	Person magnus = new Person { FirstName = "Magnus", LastName = "Hedlund" };
	Person terry = new Person { FirstName = "Terry", LastName = "Adams" };
	Person charlotte = new Person { FirstName = "Charlotte", LastName = "Weiss" };
	Person arlene = new Person { FirstName = "Arlene", LastName = "Huff" };
	
	Pet barley = new Pet { Name = "Barley", Owner = terry };
	Pet boots = new Pet { Name = "Boots", Owner = terry };
	Pet whiskers = new Pet { Name = "Whiskers", Owner = charlotte };
	Pet bluemoon = new Pet { Name = "Blue Moon", Owner = terry };
	Pet daisy = new Pet { Name = "Daisy", Owner = magnus };
	
	// Create two lists.
	List<Person> people = new List<Person> { magnus, terry, charlotte, arlene };
	List<Pet> pets = new List<Pet> { barley, boots, whiskers, bluemoon, daisy };
	
	var query = from person in people.AsQueryable()
				join pet in pets on person equals pet.Owner into gj
				from subpet in gj.DefaultIfEmpty()
				select new { person.FirstName, PetName = (subpet == null ? String.Empty : subpet.Name) };
	
	query.Dump("Left outer join: comprehension syntax");	
	
	people.GroupJoin(
		pets, 		
		person => person,
		pet => pet.Owner, 
		//(person, subpets) => new {person, subpets = subpets}
		(person, subpets) => new {person, subpets = subpets.DefaultIfEmpty()}
		).				
		Dump("Tussenresultaat").
		// De select many slaat 'm weer plat
		SelectMany(item => item.subpets, (item, subpet) => new {FirstName = item.person.FirstName, PetName = subpet == null ? "" : subpet.Name}).				
		Dump("Group join + select many");
}

class Person
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
}

class Pet
{
  public string Name { get; set; }
  public Person Owner { get; set; }
}