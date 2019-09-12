<Query Kind="Statements">
  <Output>DataGrids</Output>
</Query>

string filename = @"RoundRobinResults.csv".FromLinqpadDataFolder();
char recordSeparator = ';';
char fieldseparator = '@';

string contents = File.ReadAllText(filename);

var results = contents	
	.Split(new char[]{recordSeparator}, StringSplitOptions.RemoveEmptyEntries)
	.Select (entry => entry.Split(new char[]{fieldseparator}, StringSplitOptions.RemoveEmptyEntries))
//	.Where(field => field.Length != 7)
//	.Dump("Temp")
	.Select (field => new {Name=field[5],  Datum=DateTime.ParseExact(field[0].Trim(), "s", null), Rating=int.Parse(field[1]), Score=int.Parse(field[2]), Sb=int.Parse(field[3]), Rank=int.Parse(field[4]), PrevRating=int.Parse(field[6])})				
	.ToList()
	; 	
	
	var startNewPeriod = new DateTime(2017, 11, 01, 00, 00, 00);
	var laatsteDatum = results.Last().Datum;		
	
	var quitters = results.GroupBy(r => r.Name).Where(g => !g.Any(s => s.Datum == laatsteDatum)).Select(g => g.Key).ToList();
	//results = results.ToList();
	
	results.Where(r => r.Datum == laatsteDatum).Sum(r=>r.Rating).Dump("Sum");
  (24m* 1333m).Dump("Verwacht");
	var avgRat = results.Where(r => r.Datum == laatsteDatum).Average(r=>r.Rating);
	avgRat.Dump("Avg");
	var avgScore = results.Where(r => r.Datum == laatsteDatum).Average(r=>r.Score);
	
	Func<double, double> calcRating = score => Math.Round(avgRat + (score/(2*avgScore) -.5d) * 800);
	
	results			
		.Where(x => !quitters.Contains(x.Name))
		.GroupBy (x => x.Name)
		.Select (results2 => new {
			results2,
			Rating = results2.Last().Rating,			
			TopRanks= results2.Count(r=>r.Rank==1),			
			TotScore= results2.Sum(r=>r.Score)			
		})		
		.OrderByDescending (x => x.Rating)
		.ThenByDescending (x=> x.TopRanks)
		.ThenByDescending (x=> x.TotScore)		
		.Select((x, rank) => new {
			r=rank+1, 
			Name=x.results2.Key,
			x.Rating,
			Prest15 = calcRating (x.results2.OrderByDescending(r => r.Datum).Take(15).Average(r => r.Score)), 
			//Diff = string.Format("{0:+#;-#; 0}", results2.Last().Rating - results2.First(r => r.Datum >= startNewPeriod).Rating),			
			Diff = x.results2.Last().Rating - x.results2.First(r => r.Datum >= startNewPeriod).PrevRating,			
			FiRatingBeginNewPeriod = x.results2.First(r => r.Datum >= startNewPeriod).PrevRating,
			LoRatingNewPeriod = x.results2.Where(r => r.Datum > startNewPeriod).Min(r=>r.Rating),			
			HiRatingNewPeriod = x.results2.Where(r => r.Datum > startNewPeriod).Max(r=>r.Rating),			
			LoRankNewPeriod   = x.results2.Where(r => r.Datum > startNewPeriod).Max(r=>r.Rank),
			HiRankNewPeriod   = x.results2.Where(r => r.Datum > startNewPeriod).Min(r=>r.Rank),
			HiRating= x.results2.Max(r=>r.Rating),
			LoScoreNewPeriod = x.results2.Where(r => r.Datum > startNewPeriod).Min(r=>r.Score),
			HiScoreNewPeriod = x.results2.Where(r => r.Datum > startNewPeriod).Max(r=>r.Score),			
			TopRanksNewPeriod= x.results2.Count(r=>r.Rank==1 && r.Datum > startNewPeriod), 			
			x.TotScore
			})
		.Dump("Ranking")
		;		
	
	results
		.OrderByDescending (r => r.Datum)
		.Take(15*24)
		.GroupBy(r => r.Name)
		.Select(g => new {Name=g.Key, TopRanks=g.Count(r=> r.Rank == 1), Avg=Math.Round(g.Average(x=>x.Score), 2), Prest = calcRating (g.Average(x=>x.Score)), Act= g.First().Rating, Values=g, Delta=g.First().Rating - g.Last().Rating})		
		.OrderByDescending (x => x.Avg)		
		.Select ((x, r) => new {r=r+1, x.Name, x.Avg, x.Prest, x.Act, x.Values, x.TopRanks, x.Delta})
		.Dump("Score laatste 15 ronden")
		;
		
	results		
		.Where(r => r.Rank == 1)
		.OrderByDescending (r => r.Datum)		
		.Dump("Top ranks")
		.GroupBy(r => r.Score)		
		.Select(g => new {g.Key, count=g.Count()})
		.OrderBy(g => g.Key)
		.Dump("Grouped Top Ranks")
		;
	
	results
		.Where (r => r.Score >= 35)
		.OrderByDescending(r=>r.Datum)
		.Dump("35+");
		
	results
		.Where (r => r.Score <= 10)
		.OrderByDescending(r=>r.Datum)
		.Dump("10-");	
		
//	results		
//		.OrderByDescending (r => r.Datum)
//		.GroupBy(r => r.Datum)
//		.Select (r => new {Datum = r.Key, N1 = r.Single(s=>s.Rank == 1), N2=r.Single(s=>s.Rank == 2), ScoreDiff =r.Single(s=>s.Rank == 1).Score - r.Single(s=>s.Rank == 2).Score})
//		.GroupBy (r => r.ScoreDiff)
//		.Select (r => new {Diff=r.Key, Aantal = r.Count(), items=r})
//		//.Where (r => r.ScoreDiff >= 4)		
//		.Dump("MonsterWins");

	
	DateTime startDate = results.OrderBy(r => r.Datum).First().Datum.Date;	
	Enumerable.Range(0, (DateTime.Today - startDate).Days + 1)
		.Select(d => DateTime.Today.AddDays(-d))
		//.Where (d => new [] {DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday}.Contains(d.DayOfWeek))  		
		.Where (d => d.DayOfWeek >= DayOfWeek.Monday && d.DayOfWeek <= DayOfWeek.Friday)  		
		.Select(d => new {Datum = d.Date.ToString("dd MMMM yyyy (dddd)"), Aantal=results.Where(r => r.Datum.Date == d.Date).Count() / 24})		
		//.Select(d => d.DayOfWeek)
		.OnDemand("Rondes per dag").Dump();

	Enumerable.Range(0, ((DateTime.Today - startDate).Days % 30)+1)
		.Select(d => DateTime.Today.AddMonths(-d))
		.TakeWhile (d => d.Month >= startDate.Month && d.Year >= startDate.Year)
		.Select(d => new {Datum = d.Date.ToString("MMMM yyyy"), Aantal=results.Where(r => r.Datum.Year == d.Year && r.Datum.Month == d.Month).Count() / 24})		
		.OnDemand("Rondes per maand").Dump();	
		
//	results		
//		.GroupBy(r => new {Year = r.Datum.Year, Month =r.Datum.Month})		
//		.Select (g => new {Year = g.Key.Year, Month = g.Key.Month, Aantal = g.Count() / 24})				
//		.OrderByDescending (x => x.Year)
//		.ThenByDescending (x => x.Month)
//		.OnDemand("Rondes per maand").Dump();		
	
	results		
		.GroupBy(r => r.Datum.Year)		
		.Select (g => new {Year = g.Key, Aantal = g.Count() / 24})				
		.OrderByDescending (x => x.Year)		
		.OnDemand("Rondes per jaar").Dump();		
		
	results
		.OrderBy (r => r.Datum)
		.ThenByDescending(r => r.Rating)
		//.GroupBy (r => new DateTime(r.Datum.Year, r.Datum.Month, r.Datum.Day))
		.GroupBy (r => r.Datum)
		.Select(g => new {Date =g.Key, StdDev = Math.Round(g.StdDev(item => item.Rating), 2), Diff=g.Max(item => item.Rating) - g.Min(item => item.Rating), 
			Diff12=g.First().Rating - g.Skip(1).First().Rating, 
			Diff13=g.First().Rating - g.Skip(2).First().Rating, 
			No1=g.First().Name, No1Rat= g.First().Rating, 
			No2=g.Skip(1).First().Name, No2Rat=g.Skip(1).First().Rating, 
			No3=g.Skip(2).First().Name, No3Rat=g.Skip(2).First().Rating, 
			Som123= g.Take(3).Sum(x=> x.Rating),
			CountPlus1400= g.Count(x => x.Rating >= 1400),
			CountMin1300 = g.Count(x => x.Rating < 1300),
			})
		.OrderByDescending (r => r.Date)		
		.Dump("Deviatie in de tijd")
		.GroupBy(r => r.No1)
		.Select(r => new {Name=r.Key, Count = r.Count()})
    .OrderByDescending(r => r.Count)
		.Dump("No1s");
		
	Enumerable.Range (0, 47).OrderByDescending(n => n).Select (n => new {Score=n, Perc=Math.Round(n/46d, 2), Rating=calcRating (n)}).Dump("Tabel");
	
	results
		.GroupBy (x => x.Datum).Skip(15)//.OrderByDescending(g => g.Key).Take(15)		
		.SelectMany (g => g.Select (y => new {Datum = g.Key, Name=y.Name, Rating=y.Rating, Prestatie = calcRating(results.Where(z => z.Datum <= g.Key && z.Name == y.Name).OrderByDescending(x=>x.Datum).Take(15).Average(x => x.Score))}))				
		.OrderByDescending (x => x.Prestatie)
		.GroupBy (x => x.Name)
		.SelectMany(x => x.Take(5))		
		.OnDemand("Prestaties").Dump();		
		
	results
		.OrderByDescending (x => x.Rating - x.PrevRating)
		.ThenByDescending(x => x.Datum)
		.Select (x => new {Diff=x.Rating - x.PrevRating, x.Name, x.Datum, x.Rank, x.Score, x.PrevRating,  x.Rating})
		.Where(x => Math.Abs(x.Diff) >= 10)
		.Dump("Deltas");
    
  results
    .GroupBy(x => x.Datum)
    .OrderByDescending(g => g.Key)
    .Skip(1)
    .First()
    .OrderBy(x => x.Rank)
    .Select(x => new {x.Rank, x.Name, x.Score, x.Sb, x.Rating, diff=x.Rating-x.PrevRating})
    .Dump("Last-1");
    
  results
    .GroupBy(x => x.Datum)
    .OrderByDescending(g => g.Key)
    //.Skip(1)
    
    .First()
    .OrderBy(x => x.Rank)
    .Select(x => new {x.Rank, x.Name, x.Score, x.Sb, x.Rating, diff=x.Rating-x.PrevRating})
    .Dump("Last");
    
  var names = new string[]{"AA", "BB", "CCCC"};
    
  results
    .Where(x => names.Contains(x.Name))
    .GroupBy(x => x.Datum)
    .OrderByDescending(g => g.Key)    
    .Where(g => g.Count() == 3)
    .Select(g => new {g.Key, items=g, cnt=g.Sum(x => x.Rank), cntPt=g.Sum(x => x.Score)})
    .OrderByDescending(x => x.cntPt)
    .ThenByDescending(x => x.cnt)
    .Select((g, i) => new {g.Key, g.items, g.cnt, g.cntPt, rank=i+1})
    .OrderByDescending(x => x.Key)
    .Dump("Test");