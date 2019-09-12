<Query Kind="Program" />

void Main()
{
	Tuple<string, int> [] tuples = new Tuple<string, int>[] {	    
	    new Tuple<string, int>("1015", 0),
		new Tuple<string, int>("1216", -2),
		new Tuple<string, int>("1116", 1),		
		new Tuple<string, int>("1115", 3),		
		new Tuple<string, int>("0913", -3),
		new Tuple<string, int>("1014", -2),
		new Tuple<string, int>("0914", 2),
		
	};	
	tuples.Dump("Origineel");	
	
	Array.Sort(tuples, 0, tuples.Length, new Comparer());
	tuples.Dump("Gesorteerd");
}

// Define other methods and classes here

public class Comparer : IComparer<Tuple<string, int>>{
		public int Compare(Tuple<string, int> x, Tuple<string, int> y)
		{
			return y.Item2 - x.Item2;
		}	
}