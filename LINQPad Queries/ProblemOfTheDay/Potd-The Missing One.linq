<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/the-missing-one/", "Problem of the day: The Missing One").Dump();
var rand = new Random();
var unsortedInts = Enumerable.Range(1, 1000000).OrderBy(x => rand.Next()).ToArray();

var missing = unsortedInts.First();
//unsortedInts.Skip(1).Dump("Unsorted ints");

var foundInts = new bool[unsortedInts.Length+1];

foreach(var num in unsortedInts.Skip(1))
{	
	foundInts[num] = true;
}

missing.Dump("Missing int was:");

for (int i=1; i < foundInts.Length; i++)
{
	if (!foundInts[i])
	{
		i.Dump("Missing int found:");
		break;
	}
}

