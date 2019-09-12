<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/factorial-finder/", "Problem of the day: Factorial").Dump();

Func<int, long> factorial = null;

factorial = (num) => 
{
	if (num == 0)
		return 1;
	if (num == 1)
		return 1;
	return factorial(num-1) * num;	
};

Enumerable
	.Range(0, 20)
	.Select(x => factorial(x))
	.Dump("Factorials")
	;