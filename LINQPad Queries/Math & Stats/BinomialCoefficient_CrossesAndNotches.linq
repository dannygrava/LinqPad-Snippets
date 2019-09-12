<Query Kind="Program" />

void Main()
{
	// 	((long)bicoef (31, 5) * bicoef (26, 5)).Dump("POSSIBLE 5kx5k");
	//return;
	const int NUM_BUCKETS = 9;
	int rangeCrosses = bicoef(NUM_BUCKETS,5);
	int rangeNotches = bicoef(NUM_BUCKETS,4);
	(rangeCrosses * rangeNotches).Dump();
	
	int c1=0, c2=1, c3=4, c4=8;
	int n1=2, n2=3, n3=6, n4=7;
	
	var index = bicoef(c1, 1) + bicoef(c2, 2) + bicoef(c3, 3) + bicoef(c4, 4) + /*bicoef(c5, 5) +*/ (bicoef(n1, 1) + bicoef(n2, 2) + bicoef(n3, 3) + bicoef(n4, 4))*rangeCrosses;
	index.Dump();	
	//index = 11000;
	int  remainder = index / rangeCrosses;
	Deindex(remainder, 9, 4).Dump("Notches");
	Deindex(index-remainder*rangeCrosses, 9, 4).Dump("Crosses");
}

// Define other methods and classes here
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
// Let op: de nummers moeten wel van laag naar hoog lopen.
// Het resultaat van Index (1,2,3) kan weer ontsleuteld worden, 
// Het resultaat van Index (3,2,1) niet! Dat is de aanname die we maken.
// Hoewel 3,2,1 hetzelfde is als 1,2,3 leidt dit tot andere indices ontsleutelen naar een andere oplossing.
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