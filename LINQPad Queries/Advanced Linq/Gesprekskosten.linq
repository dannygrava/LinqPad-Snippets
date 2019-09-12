<Query Kind="Statements">
  <Namespace>System.Globalization</Namespace>
</Query>

string filename = @"telefoongesprekken.txt".FromLinqpadDataFolder();
string[] lines = File.ReadAllLines(filename);
string[] nummers = File.ReadAllLines(@"\telefoonnummers.txt".FromLinqpadDataFolder());

var nummerInfo = nummers.Select(line=>line.Split(';')).Select(values=>new{nummer=values[0], naam=values[1]}).ToList();

var gespreksInfo = lines.Select(line => line.Split(new string [] {"  ", "\t", "\n"}, StringSplitOptions.RemoveEmptyEntries))	
	.Select(splitValues => new {nummer=splitValues[1].Trim(), datumtijd = splitValues[0], duur=splitValues[3], kosten=splitValues[4]})
	.Where(x => x.nummer != "gekozen nummer")	
	.Select(x =>new {nummer=x.nummer, naam=nummerInfo.Where(y=>y.nummer==x.nummer).Select(y=>y.naam).SingleOrDefault(), datumtijd = Convert.ToDateTime(x.datumtijd), duur= TimeSpan.Parse(x.duur), kosten=Convert.ToDecimal(x.kosten, new NumberFormatInfo(){NumberDecimalSeparator = ","})});

//gespreksInfo.Sum(x=> x.duur.TotalMinutes).Dump();

gespreksInfo
    .Where(x => x.datumtijd >= new DateTime(2009, 10, 1))
    .GroupBy (x => new {x.nummer, x.naam})
	.Select(x=>new {nummer=x.Key, totKosten=x.Sum(y=>y.kosten), aantal=x.Count(), totDuurMin=x.Sum(y=> y.duur.TotalMinutes)/*, details=x*/})
	//.Where(x=>x.aantal >= 5)
	//.Where(x=> x.nummer.naam ==null || x.nummer.naam.StartsWith("Onbekend"))
	.OrderByDescending(x=>x.totKosten)	
	.Dump("Duurste nummers");
	
gespreksInfo
	.GroupBy(x => x.datumtijd.Year * 100 + x.datumtijd.Month )		
	.Select(x=>new {nummer=x.Key, totKosten=x.Sum(y=>y.kosten), aantal=x.Count(), totDuurMin=x.Sum(y=> y.duur.TotalMinutes)/*, details=x*/})
	.Dump("Kosten per maand");

gespreksInfo
	.Where(x => x.datumtijd >= new DateTime(2009, 10, 1))
	.OrderBy(x =>x.datumtijd)
	//.GroupBy(x =>x.datumtijd)
	//.Where(x=>x.Count() > 1)	
	//.Select(x=> new{datum=x.Key, aantal=x.Count()} )
	.Dump("Alles deze maand");