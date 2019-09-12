<Query Kind="Statements" />

//https://developers.google.com/chart/image/docs/chart_params
new Hyperlinq("https://developers.google.com/chart/image/docs/chart_params").Dump("View Google Chart Api Documentation");
var url = @"https://chart.googleapis.com/chart?
cht=lc&
chs=800x300&
chd=t:{0}&
chds={2},{3}&
chxr=1,{2},{3},50|2,{2},{3},50&
chxt=x,y&
chm=x,0000ff,0,-1,6|x,0000ff,1,-1,6&
chco=00FF00,FF0000&
chl={1}";
//var url = @"https://chart.googleapis.com/chart?
//cht=lc&
//chs=500x300&
//chd=t:1423,1430,1419&
//chl=Jan|Feb|Maa";
 string filename = @"RoundRobinResults.csv".FromLinqpadDataFolder();
char recordSeparator = ';';
char fieldseparator = '@';

string contents = File.ReadAllText(filename);

var results = contents	
	.Split(new char[]{recordSeparator}, StringSplitOptions.RemoveEmptyEntries)
	.Select (entry => entry.Split(new char[]{fieldseparator}, StringSplitOptions.RemoveEmptyEntries))
	.Select (field => new {Name=field[5],  Datum=DateTime.ParseExact(field[0].Trim(), "s", null), Rating=int.Parse(field[1]), Score=int.Parse(field[2]), Sb=int.Parse(field[3]), Rank=int.Parse(field[4])})		
	.ToList()
	; 

//Random r = new Random();
//var beginDate = new DateTime(2011, 1, 1);

Func<string, IEnumerable<int>> ratingsFor = (name) => results
	.Where(x => x.Name == name)
	.GroupBy(x => new {Year = x.Datum.Year, Month = x.Datum.Month})
	//.Select (x => x.OrderBy(y => y.Datum).Last().Rating)	
	.Select (x => (int) Math.Round(x.Average(y => y.Rating)))
	.ToArray();

var dates = results
	.GroupBy(x => new {Year = x.Datum.Year, Month = x.Datum.Month})
	.Select(g => string.Format("{0}", g.Key.Month == 1? g.Key.Year : g.Key.Month)).ToArray();
	
var name1 = "AA";
var name2 = "BB";

var chartUrl = string.Format(url, ratingsFor (name1).AsCommaText() + "|" + ratingsFor (name2).AsCommaText() , dates.AsCommaText("|"), ratingsFor (name1).Concat(ratingsFor (name2)).Min(), ratingsFor (name1).Concat(ratingsFor (name2)).Max());

Util.Image(chartUrl).Dump();

ratingsFor (name1).OnDemand("View y-axis").Dump();
dates.OnDemand("View x-axis").Dump();
chartUrl.OnDemand("View chart url").Dump();