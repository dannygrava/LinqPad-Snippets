<Query Kind="Statements" />

var lines = File.ReadAllLines("ExportedDays.csv".FromLinqpadDataFolder());

char recordSeparator = ',';

var results = lines
	.Skip(1) // skip line with headers
	.Select(l => l.Split(new char[]{recordSeparator}, StringSplitOptions.RemoveEmptyEntries))
	.Select(items => new {Year=int.Parse(items[0]), Month=int.Parse(items[1]), Day=int.Parse(items[2]), Steps=int.Parse(items[3]),
		Distance=int.Parse(items[4]), NetCal=decimal.Parse(items[5]), GrossCal=decimal.Parse(items[6]), ActTime=int.Parse(items[8]),
		})
	.ToList()
	; 

//results.Dump("Raw");
// 1. Totalen per maand
results
	.GroupBy(x => x.Year*100 + x.Month)
	.Select(grp => new {Month=new DateTime(grp.Key/100, grp.Key%100, 1).ToString("MMMM yyyy"), TotalSteps=grp.Sum(x => x.Steps), TotalDistanceKm=grp.Sum(x => x.Distance)/1000m, BestStep=grp.Max(x => x.Steps), BestStepDay=new DateTime(grp.Key/100, grp.Key%100, grp.OrderByDescending(x=>x.Steps).First().Day).ToString("dddd dd MMMM yyyy"), GoldMedals=grp.Count(x => x.Steps >= 10000),
		totalNetCals = grp.Sum(x=> x.NetCal), totalGrossCals = grp.Sum(x=> x.GrossCal)/*items=grp*/})
	.Dump();
// 2. Meeste per maand