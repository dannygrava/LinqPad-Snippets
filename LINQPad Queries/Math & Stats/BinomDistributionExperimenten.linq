<Query Kind="Program" />

void Main()
{
    const int trials = 4000;
	
	var results = Enumerable.Range(1,trials)
		//.Select(x => getBinomial(2, .8))
		.Select(x => getScore(1.8))
		.ToList();	
	
	((double) results.Count(x=> x==1) /trials)
		.Dump("Draw perc.");
		
	results
		.GroupBy(x=>x)
		.Select(g => new {value=g.Key, count=g.Count()})
		.OrderBy(x=>x.value)
		.Take(5)
		.Dump("Distribution");
		
	results.Sum().Dump("Sum");	
	results.Average().Dump("Avg");
	((double) (trials - results.Sum()) / trials).Dump("Dev");	
}

static Random random = new Random();

public static int getBinomial(int n, double p) 
{
  int x = 0;
  for(int i = 0; i < n; i++) {
	if(random.NextDouble() < p)
	  x++;
  }
  return x;
}

public static int getScore(double expectedMeanScore)
{
	const double variance = .3; // effectively determines drawing perc.
	int score  = (int) Math.Round(expectedMeanScore + variance * getNormal());
	if (score < 0)
		return 0;
	if (score > 2)
		return 2;
	return score;
}

// Get normal (Gaussian) random sample with mean 0 and standard deviation 1
public static double getNormal()
{
	// Use Box-Muller algorithm
	double u1 = random.NextDouble();
	double u2 = random.NextDouble();
	double r = Math.Sqrt( -2.0*Math.Log(u1) );
	double theta = 2.0*Math.PI*u2;
	return r*Math.Sin(theta);
}