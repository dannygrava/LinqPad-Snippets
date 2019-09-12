<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/largest-difference/", "Problem of the day: Largest Difference").Dump();

Random r = new Random();

var ints = Enumerable.Repeat(0, 100).Select(x => r.Next(100)).ToArray();

ints.Dump();

//ints.Aggregate()

var largestDiff = 0;
var index = 0;
for(int i = 0; i < ints.Length; i++)
{
	if (i > 0 && Math.Abs(ints[i]-ints[i-1]) > largestDiff)
	{		
		largestDiff = Math.Abs(ints[i]-ints[i-1]);
		index = i;
	}
}

string.Format("Largest diff={0} at index {1} between {2} and {3}", largestDiff, index, index == 0 ? ints[index] : ints[index -1], ints[index]).Dump();



