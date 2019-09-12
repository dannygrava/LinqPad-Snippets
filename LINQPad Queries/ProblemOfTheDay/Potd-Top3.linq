<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/top-3/", "Problem of the day: Top 3").Dump();
int [] top3 = new int[3] {0,0,0};

var rand = new Random();
var nums = Enumerable.Repeat(0, 20).Select(x=> rand.Next(100)).ToList();

nums.Dump();

foreach(var num in nums)
{
	if (num > top3[0])
	{
		top3[2] = top3[1];
		top3[1] = top3[0];
		top3[0] = num;	
	}
	else if (num > top3[1])
	{
		top3[2] = top3[1];
		top3[1] = num;
	}
	else if (num > top3[2])
	{
		top3[2] = num;
	}	
}

top3.Dump("Top3");

//nums.OrderByDescending(x => x).Dump("Nums ordered by descending");