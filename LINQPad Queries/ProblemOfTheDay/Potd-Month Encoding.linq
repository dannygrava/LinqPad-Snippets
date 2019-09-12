<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/month-encoding/", "Problem of the day: Month Encoding").Dump();
//Can you figure out the pattern and solve for the rest of the year?
//
//January = 101025
//February = 6525
//March = 13188
//April = 1912
//May = 13125
//June = 10215
//July = ?
//August = ?
//September = ?
//October = ?
//November = ?
//December = ?
//
// Het patroon is:
// Eerste letter, letter overeenkomend met maandnummer (als maandnummer groter dan aantal maanden dan doortellen), Laatste Letter
// Dus jjy, fey, mrh, ail, may, jue, etc.
string [] months = new string[] {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November","December"};
int [] numbers = new int []{101025, 6525, 13188, 1912, 13125, 10215};

numbers
	//.Select(x => Convert.ToString(x, 16))
	.Dump();

//months.Zip(numbers, (s, x) => new {x, s}).Dump();
//months.SelectMany(numbers, (s, x) => new {x, s}).Dump();

months	
	.Select(s => s.ToLower())
	//.Select(s => s.ToLower().Select(c => c-'a'+1))	
	.Select ((s, i) => string.Format ("{0}{1}{2}", s[0]-'a'+1, s[i % s.Length]-'a'+1, s[s.Length-1]-'a'+1, s[s.Length-2]-'a'+1))
	.Dump();
	
months
	.Select(s => s.ToLower())
	.Select(s => s.ToLower().Select(c => c-'a'+1))
	.Dump();		
//months[0].ToLower().Select(c => c-'a'+1).Dump();