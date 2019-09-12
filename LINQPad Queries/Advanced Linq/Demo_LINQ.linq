<Query Kind="Statements" />

// Paden naar de teken gescheiden bestanden: pas evt. de lokatie aan.
string pathArtikelen = @"Artikelen_DemoArt.txt".FromLinqpadDataFolder();
string pathCrediteuren = @"Crediteuren_DemoArt.txt".FromLinqpadDataFolder();

// Lees de artikelen in
var artikelen = (
  from regel in File.ReadAllLines(pathArtikelen) 
  let values = regel.Split(';') 
  select new {Code=values[0],ZoekCode=values[1],Oms=values[2],Groep=Convert.ToInt32(values[3]),KostPrijsEx=Convert.ToDecimal(values[4]),LeverancierNummer=values[5]});
  
// Lees de crediteuren in
var crediteurs = (
  from regel in File.ReadAllLines(pathCrediteuren)  
  let values = regel.Split(';')
  select new {Nummer=values[0],Naam=values[1],KredietLimiet=Convert.ToDecimal(values[2])});


// Alle artikelen
var AlleArtikelen = (from artikel in artikelen select artikel);
AlleArtikelen.Dump("Alle artikelen");


// Gesorteerd op Groep oplopend, LeverancierNummer aflopend
(from artikel in artikelen orderby artikel.Groep ascending, artikel.LeverancierNummer descending select artikel).Dump("Artikelen gesorteerd op Leveranciernummer aflopend");


// Alleen Groep 40
(from artikel in artikelen where artikel.Groep==40 select artikel).Dump("Alleen Artikelen binnen groep 40");


// Artikelen met leveranciergegevens op basis van join op LeverancierNummer
(from artikel in artikelen
 join crediteur in crediteurs on artikel.LeverancierNummer equals crediteur.Nummer 
 select new {artikel, crediteur}
).Dump("Artikelen met leveranciers op basis van join op LeverancierNummer");

// Leveranciers met al hun te leveren artikelen (hiërarchische join)
(from crediteur in crediteurs 
 join artikel in artikelen on crediteur.Nummer equals artikel.LeverancierNummer into leverbareArtikelen
 select new {crediteur.Nummer, leverbareArtikelen}).Dump("Leveranciers met al hun te leveren artikelen"); 
 
// Artikelen met leveranciergegevens op basis van SelectMany
(from artikel in artikelen
 from crediteur in crediteurs
 where artikel.LeverancierNummer == crediteur.Nummer
 select new {artikel, leverancier=crediteur}
).Dump("Artikelen met leveranciergegevens op basis van SelectMany");


// Artikelen met leveranciergegevens op basis van Subselect
(from artikel in artikelen
   let leverancier = (from crediteur in crediteurs where crediteur.Nummer == artikel.LeverancierNummer select crediteur)
   select new {artikel, leverancier}
 ).Dump("Artikelen met leveranciergegevens op basis van Subselect");


// Groeperen op leverancier
( from artikel in artikelen   
  group artikel by artikel.LeverancierNummer into gegroepeerd // let op groeperen op item en niet op de lijst artikelen
  select new {nummer=gegroepeerd.Key, artikels=(from art in gegroepeerd select art)}).Dump("Gegroepeerd op leverancier");
  

// Alternatieve (traditionele) syntax met Lambda Expressies: Codes met groep 40
artikelen.Where(artikel => artikel.Groep==40).Select (artikel => artikel.Code).Dump("Artikel codes met groep 40");