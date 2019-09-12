<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

//string [] lines = File.ReadAllLines("VerbiPiuCercati.csv".FromLinqpadDataFolder());
//string [] lines = File.ReadAllLines("WikiBook.csv".FromLinqpadDataFolder());
//string [] lines = File.ReadAllLines("Swadesh.csv".FromLinqpadDataFolder());

string [] lines = (
	File.ReadAllLines("Verbi.csv".FromLinqpadDataFolder())
//  .Concat(
//    File.ReadAllLines("Opgezocht.csv".FromLinqpadDataFolder())
//  File.ReadAllLines("VerbiPiuCercati.csv".FromLinqpadDataFolder()).Concat(
//	File.ReadAllLines("WikiBook.csv".FromLinqpadDataFolder())).Concat(
//	File.ReadAllLines("Swadesh.csv".FromLinqpadDataFolder())).Concat(  
//	File.ReadAllLines("Woordenschat.csv".FromLinqpadDataFolder())))  
	.ToArray()
	);

var woordjes = lines.Select(l => l.Split(';')).Select( l => new {Ita= l[0], Nld=l[1].Split(',').Select(x => x.Trim()).ToList()});
//
woordjes.Count().Dump("Totaal:");
//woordjes.GroupBy(x => x.Ita).Where(g => g.Count() > 1).Dump();
//return;
int aantalTeOverhoren = 50;
Random r = new Random();
var teOverhoren = woordjes.Select(x => new {x.Ita, x.Nld, seed=r.Next()}).OrderBy(x => x.seed).Take(aantalTeOverhoren).ToList();

int score = 0;
var dc = new DumpContainer().Dump();


List<Tuple<string, bool>> inputs = new List<Tuple<string, bool>>();
foreach(var woordje in teOverhoren)
{
	string input = Util.ReadLine<string>(woordje.Ita);
	bool isCorrect = woordje.Nld.Contains(input, StringComparer.CurrentCultureIgnoreCase);
	if (isCorrect)
	{
		score++;
		dc.Content = "Correct";				
	}		
	else
	{
	 	dc.Content = $"Fout {input} => {string.Join(",", woordje.Nld)}";		
	}
	inputs.Add(new Tuple<string, bool> (input, isCorrect));
}
dc.Content = $"Score: {score} uit {teOverhoren.Count}";

teOverhoren.Zip(inputs, (t, v) => new {t.Ita, v.Item1, Correct=v.Item2, Nld=string.Join(",", t.Nld)}).Dump("De antwoorden", 0);