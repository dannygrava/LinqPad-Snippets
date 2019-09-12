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
	.Select (field => new {Name=field[5],  Datum=DateTime.ParseExact(field[0].Trim(), "s", null), Rating=int.Parse(field[1]), Score=int.Parse(field[2]), Sb=int.Parse(field[3]), Rank=int.Parse(field[4])})	
	.ToList()
	//.Dump()
	; 	
	
//results.Where (x => x.Name == "Km").OrderByDescending(x => x.Datum).Take(15).Dump();

// *** largest changes ***
var found = new List<dynamic>();
const int targetDif = 5;
DateTime fromDate = new DateTime(2010, 10, 1);
//.Where(r => r.Datum.Year != 2013 && r.Datum.Month != 1 && r.Datum.Day != 9 && r.Datum.Hour != 14 && r.Datum.Minute != 12)
foreach (var grouping in results.Where(r => r.Datum >= fromDate).GroupBy(r => r.Name))
{	
	var items = grouping.OrderBy (r => r.Datum).ToList();
	for (int i = 1; i < items.Count; i++)
	{
		if (items[i].Datum.Year == 2013 && items[i].Datum.Month == 1 && items[i].Datum.Day == 9 && items[i].Datum.Hour == 14 && items[i].Datum.Minute == 12)
			continue;
			
		var dif = items[i].Rating - items[i-1].Rating;
		if (Math.Abs(dif) >= Math.Abs(targetDif))
		{
			found.Add(new {Name = items[i].Name, Diff = dif, OlrdRating = items[i-1].Rating, NewRating = items[i].Rating, Date = items[i].Datum, Score = items[i].Score, Entry=items[i]});	
		}
	}
}
found.OrderByDescending(x => x.Diff).ThenByDescending(x => x.Date).Dump("Biggest changes");

// *** streak counter ***

var streaks = new List<dynamic>();
string lastName = "";
int count = 0;
DateTime streakStartDate = DateTime.Now; 
foreach (var toprank in results.Where(r => r.Rank == 1).OrderBy (r => r.Datum))
{	
	if (lastName != toprank.Name)
	{
		if (count > 1)
		{
			streaks.Add (new {Name = lastName, Count = count, StartDate = streakStartDate, EndDate = toprank.Datum});
		}
	   	lastName = toprank.Name;
		count = 0;		
		streakStartDate = toprank.Datum;
	}	
	count++;
}
if (count > 1)
{
	streaks.Add (new {Name = lastName, Count = count, StartDate = streakStartDate, EndDate = DateTime.Today});
}

streaks.OrderByDescending (r => r.Count).ThenBy(r=> r.Name).ThenByDescending(r => r.StartDate).Dump("Streaks");