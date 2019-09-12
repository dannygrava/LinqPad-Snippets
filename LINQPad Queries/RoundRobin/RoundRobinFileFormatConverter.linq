<Query Kind="Statements" />

string filename = @"RoundRobinResults.csv".FromLinqpadDataFolder();
char recordSeparator = ';';
char fieldseparator = '@';

string contents = File.ReadAllText(filename);

Dictionary<string, string> substitutions = new Dictionary<string, string> {{"AS", "FD"}};
int initRat = 1346;

// Lezen bestand in pakken de laatste Ronde
var results = contents	
	.Split(new char[]{recordSeparator}, StringSplitOptions.RemoveEmptyEntries)
	.Select (entry => entry.Split(new char[]{fieldseparator}, StringSplitOptions.RemoveEmptyEntries))
	.Select (field => new {id = Guid.NewGuid(), Name=field[5],  Datum=DateTime.ParseExact(field[0].Trim(), "s", null), Rating=int.Parse(field[1]), Score=int.Parse(field[2]), Sb=int.Parse(field[3]), Rank=int.Parse(field[4]), OldRating=int.Parse(field[6])})		
	.GroupBy(x => x.Datum)
	.Last()
	.Select(x => x)
	.ToList()
	//.Dump();
	; 

// Berekenen het verschil in punten tussen de stoppers en nieuwkomers
var verschil = results.Where(x => substitutions.ContainsKey(x.Name)).Sum(x => x.Rating - initRat);
verschil.Dump("Verschil");

var targetSet = results.Where (x => !substitutions.ContainsKey(x.Name)).OrderByDescending(x => x.Rating).ToList();

// Maak een dictionary voor het berekenen van de nieuwe punten
Dictionary<Guid, int> _newrats = new Dictionary<Guid, int>();
results.ForEach(item => _newrats [item.id] = substitutions.Keys.Contains(item.Name) ? initRat : item.Rating);			

// Verdeel punten
while (verschil != 0)
{	
	foreach (var item in targetSet)
	{
		if (verschil == 0)
		{
			break;
		}
    if (verschil > 0)
    {
  		_newrats [item.id] += 1;
  		verschil--;
    }
    else
    {
  		_newrats [item.id] -= 1;
  		verschil++;    
    }   
	}
}

results
	.Select (item => new {
		Name=substitutions.ContainsKey(item.Name)? substitutions[item.Name]: item.Name, 
		item.Rating, 
		nr = _newrats[item.id],
		diff = _newrats[item.id] - item.Rating,
		})
	.Dump("Nieuw")
	.Average(x => x.nr).Dump("Avg");
//
//var rounds = results.Select (x => new {x.Datum, x.Rating, x.Score, x.Sb, x.Rank, x.Name, or=_oldrats[x.Id]}).GroupBy(x => x.Datum);
// Step : Write results to new file	format (with name)

var newValues = results.Select(field => string.Format("{0:s}@{1}@{2}@{3}@{4}@{5}@{6};", field.Datum, _newrats[field.id], field.Score, field.Sb, field.Rank, substitutions.ContainsKey(field.Name)? substitutions[field.Name]: field.Name, initRat));
newValues.Dump("Nieuwe waarden");
var result = contents + "\r\n" + (string.Join("", newValues) + "\r\n");
result.Dump("Nieuwe contents");
string resultsFilename = "RoundRobinResults_1.csv".FromLinqpadDataFolder();
File.WriteAllText(resultsFilename, result.Trim(), Encoding.UTF8);