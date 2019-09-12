<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/bit-flipping/", "Problem of the day: Bit Flipping").Dump();

//flip([1,0,0,1,0,0,1])

Func<int[], int[]> best_flip = (switches) => 
{
	int [] best = switches.ToArray();	
	var best_count = best.Count(x => x==1);
	int [] temp = new int []{};
	
	for(var i = 0; i < switches.Length; i++)
	{
		temp = switches.ToArray();
		for (var j = i; j < switches.Length; j++)
		{
			temp[j] = temp[j] == 1 ? 0 : 1;
			if (temp.Count(x => x==1) > best_count)
			{	
				best_count = temp.Count(x => x==1); 				
				best = temp.ToArray();
			}
		}
	}
	return best;
};

int[] test = new [] {1, 0, 0, 1, 0, 0, 1};

best_flip(test).Dump().Count().Dump();
best_flip(new [] {0, 1, 1, 1, 0, 0, 1, 0, 1, 0, 1, 1, 1, 0, 0, 1}).Dump().Count().Dump();