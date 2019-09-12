<Query Kind="Statements" />

// Paden naar de teken gescheiden bestanden: pas evt. de lokatie aan.
string pathArtikelen = @"Artikelen_DemoArt.txt".FromLinqpadDataFolder();
string pathCrediteuren = @"Crediteuren_DemoArt.txt".FromLinqpadDataFolder();

// Lees de artikelen in
var artikelen = File.ReadAllLines(pathArtikelen)
	.Select(regel=> regel.Split(';'))
	.Select(values=> new {Code=values[0],ZoekCode=values[1],Oms=values[2],Groep=Convert.ToInt32(values[3]),KostPrijsEx=Convert.ToDecimal(values[4]),LeverancierNummer=values[5]});

// Lees de crediteuren in
var crediteurs = File.ReadAllLines(pathCrediteuren)
	.Select(regel=> regel.Split(';'))
	.Select(values=> new {Nummer=values[0],Naam=values[1],KredietLimiet=Convert.ToDecimal(values[2])});
	
// Alle artikelen
artikelen.Dump("Alle artikelen");  

// Gesorteerd op Groep oplopend, LeverancierNummer aflopend
artikelen.OrderBy(artikel => artikel.Groep)
	.ThenByDescending(artikel => artikel.LeverancierNummer)
	.Dump("Artikelen gesorteerd op Leveranciernummer aflopend");
	
// Alleen Groep 40
artikelen.Where(artikel => artikel.Groep == 40).Dump("Alleen Artikelen binnen groep 40");

// Artikelen met leveranciergegevens op basis van join op LeverancierNummer
artikelen.Join(
	crediteurs, 
	artikel => artikel.LeverancierNummer, 
	crediteur => crediteur.Nummer, 
	(artikel, crediteur) => new {artikel, crediteur})
	.Dump("Artikelen met leveranciers op basis van join op LeverancierNummer");

// Leveranciers met al hun te leveren artikelen (hiërarchische join)
crediteurs.GroupJoin(
	artikelen, 
	(crediteur) => crediteur.Nummer, 
	(artikel)=> artikel.LeverancierNummer, 
	(crediteur, leverbareArtikelen) => new {crediteur.Nummer, leverbareArtikelen})
	.Dump("Leveranciers met al hun te leveren artikelen");

// Artikelen met leveranciergegevens op basis van SelectMany
artikelen.SelectMany(
	(artikel) => crediteurs,
	(artikel, crediteur)=> new {artikel, leverancier=crediteur})
  .Where(result => result.artikel.LeverancierNummer == result.leverancier.Nummer)  
  .Dump("Artikelen met leveranciergegevens op basis van SelectMany");
	
// Artikelen met leveranciergegevens op basis van Subselect
artikelen.Select(artikel=> new {artikel, leverancier = crediteurs.Where(crediteur => crediteur.Nummer == artikel.LeverancierNummer)})
	.Dump("Artikelen met leveranciergegevens op basis van Subselect");
	
// Groeperen op leverancier
artikelen.GroupBy(artikel => artikel.LeverancierNummer)
	.Select(gegroepeerd => new {nummer=gegroepeerd.Key, artikels=gegroepeerd})
	.Dump("Gegroepeerd op leverancier");