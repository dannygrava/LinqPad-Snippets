<Query Kind="Program" />

void Main()
{	    
	bool inTestMode = false;
	string resultsFilename = "RoundRobinResults.csv".FromLinqpadDataFolder();
	char separator = ';';
	
	string[] lines  = File.ReadAllLines(resultsFilename);	
	
	var data = lines
		.Last()
		.Split(new Char[]{separator}, StringSplitOptions.RemoveEmptyEntries)
		.Select(fields => new {name=fields.Split(new char[]{'@'}, StringSplitOptions.RemoveEmptyEntries).ElementAt(5), rat=int.Parse(fields.Split(new char[]{'@'}, StringSplitOptions.RemoveEmptyEntries).ElementAt(1))})		
		.OrderBy(x => x.rat % 10 + (x.rat / 10) % 10) // prevent fixed ordering
		.ToArray();		

	int MAX_PARTS = data.Length-1;		
	
	// Step 1: Create the scores
	int [][] scores = new int [MAX_PARTS+1][];
	for (int i = 0; i<MAX_PARTS+1; i++)
	{
		scores[i] = new int [MAX_PARTS+1];
	}		

	for (int i = 0; i<MAX_PARTS; i++)
	{			
		for (int j = i; j<MAX_PARTS; j++)
		{	
			if (i != j)
			{
				int score = GetScore(getExpectedScore(data[i].rat, data[j].rat));			
		   		scores[i][j] = score;
				scores[j][i] = 2-score;
			}
			else
			{
				// Als i==j dan is opponent MAX_PARTS+1 oftewel de laatste
				int score = GetScore(getExpectedScore(data[i].rat, data.Last().rat));			
				scores[i][MAX_PARTS] = score;
				scores[MAX_PARTS][i] = 2-score;
				scores[i][j] = 0;				
			}
		}			
	}	
	
	// Step 2: calculate totals + SB
	IList<Ranking> rankings = scores.Select((x,i) => new Ranking {
			Name=data.ElementAt(i).name, 
			Score=x.Sum(), 			
			Sb=x.Select ((s,j) => s*scores.ElementAt(j).Sum()).Sum(),
			Oldrat=data.ElementAt(i).rat, 
			// Minder zuiver: hogere ratings hebben een licht voordeel, terwijl lagere ratings een licht nadeel hebben
			//Newrat=Sonas((int) rats.ElementAt(i), (int) Math.Round(rats.Average()),  (decimal) x.Sum()/(MAX_PARTS)/2)
			// onderstaande is zuiverder
			Newrat=Sonas((int) data.ElementAt(i).rat, (int) (data.Sum(d=>d.rat)-data.ElementAt(i).rat)/MAX_PARTS,  (decimal) x.Sum()/(MAX_PARTS)/2),
			// Evt. Maximeren hier Math.Min(1450, Sonas(etc.)).Dump();
		})		
		.OrderByDescending (x => x.Score)
		.ThenByDescending(x=>x.Sb)
		.ThenBy(x => x.Oldrat)
		.ToList();		
	
	// Stap 3: Corrigeer voor verdwijnende (of verschijnende rating punten); uitdelen van 1 naar de rest; nu alleen voor verdwijnende punt; later mss ook voor nieuwe punten	
	int ratDif = rankings.Sum(r => r.Oldrat) - rankings.Sum(r => r.Newrat);
	//// Deel bonuspunten uit op basis van rank
	//var sortedRankings = rankings.ToArray();
	
	// Deel bonuspunten uit op basis prestatie en dan score(rank)
	var sortedRankings = rankings.Reverse().ToArray();
	
	if (ratDif > 0)	
  {
		sortedRankings = sortedRankings.Where(x => x.Name == "TG" /*|| x.Name == "CvdV"*/).ToArray();
  }
  else
  {
    sortedRankings = sortedRankings.Where(x => x.Name == "VR").ToArray();
  }
  
  for (int i = 0; i < Math.Abs(ratDif); i++)
	{	
		sortedRankings [i % sortedRankings.Length].Newrat += (ratDif > 0 ? 1 : -1);
		sortedRankings [i % sortedRankings.Length].Bonus = true;		
	}	    
  
//	if (ratDif < 0) 
//		sortedRankings = sortedRankings.ToArray();	
		
	//ratDif.Dump("ratDif");	
	//sortedRankings.Dump("Test");

	
	// Step 4: Create progress table string	
	StringBuilder progTable = new StringBuilder();
	
	// Draw Header of table
	progTable.Append(" #|");
	progTable.Append("|".PadLeft(6));
	
	foreach (Ranking r in rankings)
	{			
		progTable.Append(string.Format("{0,3}|", new string(r.Name.Take(3).ToArray())));				
	}
	progTable.Append(string.Format("{0,9}|{1,5}|{2,5}| {3}\n", "Pt.", "SB", "new", "dif"));
	progTable.Append(new string('-', (4*MAX_PARTS) + 42));
	progTable.Append("\n");			

	int rnk = 0;
	// Build contents
	foreach (Ranking rank in rankings)
	{
		rnk++;
		progTable.Append(rnk.ToString().PadLeft(2));
		progTable.Append('|');
		progTable.Append(rank.Name.PadLeft(5));
		progTable.Append("|");
		int index = Array.FindIndex(data, d => d.name == rank.Name);				
		foreach(var opp in rankings)
		{
			if (rank.Name == opp.Name)
			{
				progTable.Append(" X |");
			}
			else
			{
				//progTable.Append(string.Format (" {0} ", scores [index] [Array.IndexOf(names, opp.Name)]));
				progTable.Append(string.Format (" {0} ", scores [index] [Array.FindIndex(data, d => d.name == opp.Name)]));
				progTable.Append("|");
			}
		}
		
		progTable.Append(string.Format("{0,4}({4,3:+#;-#;0})|{1,5}|{2,5}|{3: +#; -#;  0}{5}\n", rank.Score, rank.Sb, rank.Newrat, rank.Newrat - rank.Oldrat, rank.Score-MAX_PARTS, rank.Bonus?"*":""));
	}		
	
	// Step 5: Write results to file	
	StringBuilder resultSb = new StringBuilder();
	resultSb.AppendLine();
	DateTime stamp = DateTime.Now;
	foreach(var name in data.Select(x => x.name))
	{	
		var rank = rankings.Single(r => r.Name == name);		
		string resultField = string.Format("{0:s}@{1}@{2}@{3}@{4}@{5}@{6};", stamp, rank.Newrat, rank.Score, rank.Sb, rankings.IndexOf(rank)+1, name, rank.Oldrat);
		resultSb.Append(resultField);		
	}
	
	if (!inTestMode)
		File.AppendAllText(resultsFilename, resultSb.ToString(), Encoding.UTF8);
	
	// Step 6: Build a schedule
	int [,] sched = new int [MAX_PARTS+1, MAX_PARTS+1];	
	int seed = random.Next(MAX_PARTS);
	
	for (int i = 0; i<MAX_PARTS + 1; i++)
	{
		for (int j = i; j<MAX_PARTS+1; j++)
		{	
			int day = (i + j + seed) % MAX_PARTS;		   		   
			sched[i, j] = day;			
			sched[j,i] = sched[i,j];
		}			
		
		// Als i==j dan is opponent MAX_PARTS+1 oftewel de laatste
		sched[i, MAX_PARTS] = sched [i,i];
		sched[MAX_PARTS, i] = sched [i,i];
		sched[i, i] = -1;	
	}
	
	// Step 7: Write round-per-round standings		
	for (int round = 0; round < MAX_PARTS; round++)
	{
		var table = scores.Select((x,i) => new {			
			Name = data.ElementAt(i).name, 			
			Rating = data.ElementAt(i).rat,			
			Scores = x.Select((score, j)=> new {Score=score, Round=sched[i, j]}).Where (item => item.Round <= round && item.Round >= 0).OrderBy(item => item.Round),
			Opp = x.Select((score, j)=> new {Score=score, Round=sched[i, j], Name = data.ElementAt(j).name}).Single (item => item.Round == round).Name,
			Opps = x.Select((score, j)=> new {index=j, Round=sched[i, j], score}).Where (item => item.Round <= round && item.Round >= 0).Select(item => new {item.index, data.ElementAt(item.index).name, item.score}),						
			}
		)
		.ToList();			
		
		table
			.Select (x => new {
				x.Name, 
				Results= x.Scores.Sum(item => item.Score), 
				results2=x.Opps.Sum(opp => opp.score), 
				Progress=string.Join(" ", x.Scores.Select(item => item.Score)), 
				Sb=x.Opps.Sum(opp => opp.score * table.Single(y => y.Name == opp.name).Scores.Sum(item => item.Score)), 
				Result=x.Scores.Last().Score,
				x.Opp, 
				x.Rating,
        L=x.Scores.Any(item => item.Score == 0)? " ":"*"
				})			
			.OrderByDescending (x => x.Results)
			.ThenByDescending(x => x.Sb)			
			.ThenBy(x => x.Rating)
			.Select ((x, n) => new {Rnk=n+1, x.Name, x.Results, x.Result, x.Opp, x.Sb, x.Rating, x.Progress, x.L})
			.Dump("R" + (round + 1));		
	}			
	Util.WithStyle(progTable.ToString(), "font-family:Lucida Console").Dump();	
}

static Random random = new Random();

public static double getExpectedScore (int p1, int p2){
	const double C = 200;
	double diff = p1-p2;
	return .5d + diff / (4 * C);
}

int Sonas (int player, int opponent, decimal result)
{
	// Sonas systeem
	//http://www.chessbase.com/newsdetail.asp?newsid=562
	// Exp  | opp.rat.
	//_____________
	// 100% | +390
	// 0%   |-460
	
	// Gebruik een gelijkvormige verdeling +400 en -400
	// Merk op dat dit dus neer komt op de lineaire methode: K×(W-L)/2 plus K/(4C)   
  //const decimal K = 32;
  //const decimal K = 32;
  const decimal K = 48; // = C / (200/32)
  //new rating = old rating + K×(W-We), where K=10, W=actual score, and We=expected score.
  decimal newRating = player + K*(result - ExpectedScore(player, opponent));
  return (int) Math.Round(newRating);
}

decimal ExpectedScore (int player, int opponent)
{
  const decimal C = 200;
  //decimal result = (decimal)((player - opponent) + C) / (2*C);
  //decimal result = 0.5m + (decimal) (player - opponent) / (2 * C);
  decimal result = 0.5m + (decimal) (player - opponent) / (4 * C);  
  //return Math.Max(Math.Min(result, 1), 0);
  return result;
}

// expectedMeanScore value between 0..1
//public static int GetScore(double expectedMeanScore)
//{
//	double variance = .4; // effectively determines drawing perc.
//	if (expectedMeanScore > .9 || expectedMeanScore < .1) 
//	{
//		variance = .3;
//	}
//	
//	int score  = (int) Math.Round(2 * expectedMeanScore + variance * getNormal());
//	if (score < 0)
//		return 0;
//	if (score > 2)
//		return 2;
//	return score;
//}

public static int GetScore(double expectedMeanScore)
{
	return getBinomial(2, expectedMeanScore);
}

// Get normal (Gaussian) random sample with mean 0 and standard deviation 1
private static double getNormal()
{
	// Use Box-Muller algorithm
	double u1 = random.NextDouble();
	double u2 = random.NextDouble();
	double r = Math.Sqrt( -2.0*Math.Log(u1) );
	double theta = 2.0*Math.PI*u2;
	return r*Math.Sin(theta);
}

// Random number from a Binomial Distribution
// n: range of numbers
// p: mean of distribution
// http://stackoverflow.com/questions/1241555/algorithm-to-generate-poisson-and-binomial-random-numbers
private static int getBinomial(int n, double p) {
  int x = 0;
  for(int i = 0; i < n; i++) {
	if(random.NextDouble() < p)
	  x++;
  }
  return x;
}

public class Ranking
{
	public string Name {get;set;}
	public int Score {get;set;}
	public int Sb {get;set;}
	public int Oldrat {get;set;}
	public int Newrat {get;set;}
	public bool Bonus {get;set;}
}