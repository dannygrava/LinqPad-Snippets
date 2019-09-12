<Query Kind="Program" />

void Main()
{
	new Hyperlinq("http://www.problemotd.com/problem/next-life/", "Problem of the day: Next Life").Dump();	
	
	var generation = new string[] {
	    "111",
	    "110",
	    "100"
	};
	
	generation.Dump("First generation");
	next_life(generation).Dump("Next generation");
	
	// even: 0
	// oneven: 1
	//Answer = ["101", "001", "110"]
}

string[] next_life(string[] generation)
{
	string[] temp = new string [generation.Length];
	
	for (int i = 0; i < generation.Length; i++)
	{		
		
		for (int j = 0; j < generation.Length; j++)
		{	
			int sum = 0;
			sum += i > 0 && j > 0 && generation [i-1] [j-1] == '1' ? 1 : 0;			
			sum += i > 0 && generation [i-1] [j] == '1' ? 1 : 0;
			sum += i > 0 && j+1 < generation [i-1].Length && generation [i-1] [j+1] == '1' ? 1 : 0;
			
			sum += j > 0 && generation [i] [j-1] == '1' ? 1 : 0;
			sum += j+1 < generation [i].Length && generation [i] [j+1] == '1' ? 1 : 0;

			sum += i+1 < generation.Length && j > 0 && generation [i+1] [j-1] == '1' ? 1 : 0;
			sum += i+1 < generation.Length && generation [i+1] [j] == '1' ? 1 : 0;
			sum += i+1 < generation.Length && j < generation [i+1].Length-1 && generation [i+1] [j+1] == '1' ? 1 : 0;		
			
			//sum.Dump();
			
			// Rules voor Conway's Game of Life
			if (sum > 3)
				temp[i] += "0";
			else if (sum == 3)
				temp [i] += "1";
			else
				temp[i] += generation [i] [j].ToString();
		}		
	}	
	
	return temp;	
}