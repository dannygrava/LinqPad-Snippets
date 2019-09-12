<Query Kind="Statements" />

//http://projecteuler.net/index.php?section=problems&id=9

int startOfRange = 1;
int endOfRange = 1000;
int target = 1000;

var solution = (from a in Enumerable.Range (startOfRange, endOfRange)
	from b in Enumerable.Range (startOfRange, endOfRange)
	where (a + b < target) && (a < b)
	from c in Enumerable.Range (startOfRange, endOfRange)
	where /*(a < b && b < c) && */(a + b + c == target) && (Math.Pow(a, 2) + Math.Pow(b, 2) == Math.Pow(c, 2))	
	select new {a, b, c}).FirstOrDefault();
	
solution.Dump("De volgende oplossingen gevonden:");