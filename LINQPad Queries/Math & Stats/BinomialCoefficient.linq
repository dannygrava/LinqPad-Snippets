<Query Kind="Program" />

void Main()
{
	const int NUM_BUCKETS = 9;
	const int NUM_ITEMS = 5;
//	Enumerable.Range(0, NUM_SPACES).Select(x=> bicoef(x,3)).Dump();
//	Enumerable.Range(0, NUM_SPACES).Select(x=> bicoef(x,2)).Dump();
//	Enumerable.Range(0, NUM_SPACES).Select(x=> bicoef(x,1)).Dump();
	const int INDEX_TO_SOLVE = 120;
	
	INDEX_TO_SOLVE.Dump("Index to be solved");
	var factors = Deindex(INDEX_TO_SOLVE, NUM_BUCKETS, NUM_ITEMS);
	factors.Dump("DeIndex");
//	int index = (bicoef(result.Item1, 1)  + bicoef(result.Item2, 2) + bicoef(result.Item3, 3));	
	factors.Select ((f,i) => bicoef(f, i+1)).Sum().Dump("INDEX!");
	var maxIndex = Enumerable.Range(1, NUM_ITEMS)		
		.Select (x => bicoef(NUM_BUCKETS-1, x))
		.Sum();
		
	maxIndex.Dump("MAX(INDEX)");
	Enumerable.Range(0, maxIndex+1).Select(x => Deindex(x, NUM_BUCKETS, NUM_ITEMS)).Dump("HELE RANGE");	
	
//	var results = Enumerable.Range(0, NUM_BUCKETS)
//		.SelectMany(x => Enumerable.Range(0, NUM_BUCKETS), (x, y) => new {x, y})
//		.SelectMany(x => Enumerable.Range(0, NUM_BUCKETS), (item, z) => new {x=item.x, y=item.y, z})
//		.Select (item => new {index=(bicoef(item.x, 1) + bicoef(item.y, 2) + bicoef(item.z, 3)), x=item.x, y=item.y, z=item.z})				
//		//.Dump()
//		;	
	//results.Max(item=>item.index).Dump("MAX INDEX");
	//results.SingleOrDefault(item => item.x == 0 && item.y == 1 && item.z == 2).Dump("INDEX");
}

private int bicoef(int n, int k)
{
	if (k < 0|  k > n)
	{
		return 0;
	}
		
	if (k > n - k)
	{
	// take advantage of symmetry
		k = n - k;
	}
	int c = 1;
	for (int i = 1;  i <= k; i++)
	{	
		c = c * (n - (k - i));
		c = c / i;
	}
	return c;
}

// We kunnen op basis van het index nummer de configuratie terugvinden op basis waarvan berekend is.
private int[] Deindex (int index, int numberOfBuckets, int numberOfItems)
{
	int target = index;
	int[] factors = new int[numberOfItems];
	
	for (int j = numberOfItems; j > 0; j--)
	{
		for (int i = numberOfBuckets-1; i>=0; i--)
		{
			int value = bicoef (i, j);
			if (value <= target)
			{
				factors[j-1] = i;
				target -= value;
				break;
			}		
		}	
	}	
	return factors;
}