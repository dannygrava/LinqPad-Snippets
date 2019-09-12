<Query Kind="Statements" />

var random = new Random();
var omzet = Enumerable
	.Repeat(1, 100)
	.Select (x => random.Next())
	.Select(r => new DateTime(2014, 1, 1).AddDays(r % 365))		
	.Select(d => new {Date = d, Omzet = Math.Round(random.NextDouble() * 10000, 2)})
	.ToList();

omzet
	.GroupBy (d => d.Date.Month)
	.Select (g => new {Month=g.Key, Sum=g.Sum(o => o.Omzet)})
	.OrderBy (a => a.Month)
	.Dump("Traditioneel met group by");
	
var Rozenshtein = new 
{
	Jan = omzet.Sum(d => (1 - Math.Abs(Math.Sign(1-d.Date.Month))) * d.Omzet),
	Feb = omzet.Sum(d => (1 - Math.Abs(Math.Sign(2-d.Date.Month))) * d.Omzet),
	Maa = omzet.Sum(d => (1 - Math.Abs(Math.Sign(3-d.Date.Month))) * d.Omzet),
	Apr = omzet.Sum(d => (1 - Math.Abs(Math.Sign(4-d.Date.Month))) * d.Omzet),
	Mei = omzet.Sum(d => (1 - Math.Abs(Math.Sign(5-d.Date.Month))) * d.Omzet),
	Jun = omzet.Sum(d => (1 - Math.Abs(Math.Sign(6-d.Date.Month))) * d.Omzet),
	Jul = omzet.Sum(d => (1 - Math.Abs(Math.Sign(7-d.Date.Month))) * d.Omzet),
	Aug = omzet.Sum(d => (1 - Math.Abs(Math.Sign(8-d.Date.Month))) * d.Omzet),
	Sep = omzet.Sum(d => (1 - Math.Abs(Math.Sign(9-d.Date.Month))) * d.Omzet),
	Okt = omzet.Sum(d => (1 - Math.Abs(Math.Sign(10-d.Date.Month))) * d.Omzet),
	Nov = omzet.Sum(d => (1 - Math.Abs(Math.Sign(11-d.Date.Month))) * d.Omzet),
	Dec = omzet.Sum(d => (1 - Math.Abs(Math.Sign(12-d.Date.Month))) * d.Omzet),
}.Dump("Volgens de Rozenshtein methode");