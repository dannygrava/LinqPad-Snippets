<Query Kind="Statements" />

var doc = new XDocument(
	new XElement("Voorbeeld",
		new XElement("WijzigDatum", "2010-11-01"),
		new XElement("AanmaakDatum", XmlConvert.ToString(DateTime.Today, "yyyy-MM-dd"))
		)
	);
doc.Dump();
DateTime wijzigdatum = (DateTime) doc.Element("Voorbeeld").Element("WijzigDatum");
DateTime aanmaakdatum = (DateTime) doc.Element("Voorbeeld").Element("AanmaakDatum");
string sAanmaakdatum = doc.Element("Voorbeeld").Element("AanmaakDatum").Value; 
// of 
//string sAanmaakdatum = (string) doc.Element("Voorbeeld").Element("AanmaakDatum"); 
wijzigdatum.Dump("wijzigdatum");
aanmaakdatum.Dump("aanmaakdatum");
sAanmaakdatum.Dump("sAanmaakdatum");