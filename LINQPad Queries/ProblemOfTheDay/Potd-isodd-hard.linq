<Query Kind="Statements" />

new Hyperlinq("http://http://www.problemotd.com/problem/isodd-hard/", "Problem of the day: isOdd Hard").Dump();
Func<int, bool> isOddHard = (num) => 
{
	return (num & 1) != 0;
};


Enumerable
	.Range(0, 1000)
	.Select(x => new {x, isOddHard=isOddHard(x), isOddEasy=(x%2!=0) })
	.Where(item => item.isOddHard != item.isOddEasy)
	.Dump()	
	;