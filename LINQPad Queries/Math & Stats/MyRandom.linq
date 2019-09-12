<Query Kind="Program" />

void Main()
{	
	// 
	new Hyperlinq("http://en.wikipedia.org/wiki/Linear_congruential_generator").Dump("linear congruential generator");
	new Hyperlinq("http://franklinta.com/2014/08/31/predicting-the-next-math-random-in-java").Dump("Original article");
	//MyRandom.Next(10).Dump();		
	//(8 >> 1).Dump();
	const int ceil = 10;
	Enumerable
		.Repeat(1, 20)
		.Select(x => MyRandom.Next(ceil))
		.Dump()
		.OrderBy(x => x)
		.GroupBy(x => x)
		.Select (g => new {Value=g.Key, Count=g.Count()})
		.Dump();	
}

static class MyRandom
{
	private static ulong seed = (ulong) DateTime.Now.Ticks & mask;
	private static ulong multiplier = 0x5DEECE66DUL;
	private static ulong addend = 0xBUL;
	private static ulong mask = (1UL << 48) - 1;
	
	public static int Next(int bits)
	{
		seed = (seed * multiplier + addend) & mask;
		//return (int)(seed >> (48 - bits));
		return (int) (seed % (ulong) bits);
	}
	
}

// Define other methods and classes here
