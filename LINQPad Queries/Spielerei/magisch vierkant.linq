<Query Kind="Statements" />

var magischVierkant =( 
   from x1 in Enumerable.Range(1, 9)
   from x2 in Enumerable.Range(1, 9) where x2 != x1 
   from x3 in Enumerable.Range(1, 9) where x3 != x1 && x3 != x2
   from y1 in Enumerable.Range(1, 9) where y1 != x1 && y1 != x2 && y1 != x3
   from y2 in Enumerable.Range(1, 9) where y2 != x1 && y2 != x2 && y2 != x3 && y2!= y1
   from y3 in Enumerable.Range(1, 9) where y3 != x1 && y3 != x2 && y3 != x3 && y3!= y1 && y3 != y2
   from z1 in Enumerable.Range(1, 9) where z1 != x1 && z1 != x2 && z1 != x3 && z1!= y1 && z1 != y2 && z1 != y3
   from z2 in Enumerable.Range(1, 9) where z2 != x1 && z2 != x2 && z2 != x3 && z2!= y1 && z2 != y2 && z2 != y3 && z2 != z1
   from z3 in Enumerable.Range(1, 9) where z3 != x1 && z3 != x2 && z3 != x3 && z3!= y1 && z3 != y2 && z3 != y3 && z3 != z1 && z3 != z2
   let 
	 som = x1 + x2 + x3
   where 
     // som rijen
	 x1 + x2 + x3 == som && y1 + y2 + y3 == som && z1 + z2 + z3 == som && 
	 // som kolommen
	 x1 + y1 + z1 ==som && x2 + y2 + z2 ==som && x3 + y3 + z3 ==som &&
	 // som diagonalen
	 x1 + y2 + z3 == som &&  x3 + y2 + z1 == som
	 
  select new {x1, x2, x3, y1, y2, y3, z1, z2, z3, som}).ToList();
  
  magischVierkant.Dump("De volgende " + magischVierkant.Count() + " oplossingen gevonden");   
  
  foreach (var oplossing in magischVierkant)
  {
    Console.WriteLine ("-------------");	
    Console.WriteLine ("|{0} {1} {2}|", oplossing.x1, oplossing.x2, oplossing.x3);
	Console.WriteLine ("|{0} {1} {2}|", oplossing.y1, oplossing.y2, oplossing.y3);
	Console.WriteLine ("|{0} {1} {2}|", oplossing.z1, oplossing.z2, oplossing.z3);	
  }
  Console.WriteLine ("-------------");