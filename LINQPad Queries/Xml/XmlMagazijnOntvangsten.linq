<Query Kind="Statements" />

const string xmlFilename = @"D:\Users\dg\Documents\King\Imports\XmlMagazijnOntvangsten.xml";
XElement magazijnOntvangsten = XElement.Load(xmlFilename);

// Duplicate XElement middels de constructor! (http://msdn.microsoft.com/en-us/library/bb297950(v=vs.110).aspx)

int mokNummer = 100;
foreach (var element in magazijnOntvangsten.DescendantNodes().OfType<XElement>().Where (x => x.Name == "MOK_ONTVANGSTNUMMER"))
{	
	element.Value = XmlConvert.ToString (mokNummer); 
	mokNummer++;
}

var ontvangstenMetSerienummers = magazijnOntvangsten.DescendantNodes().OfType<XElement>()
	.Where (x => x.Name == "MAGAZIJNONTVANGST" && x.DescendantNodes().OfType<XElement>().Any(el => el.Name == "MORD_SERIENUMMER"))
	.Dump();	
	
foreach (var element in ontvangstenMetSerienummers.ToList())
{
	element.Remove();		
}

foreach (var element in magazijnOntvangsten.DescendantNodes().OfType<XElement>().Where (x => x.Name == "MOR_BESTELNUMMER" ||x.Name == "MOR_BESTELREGELNUMMER" ).ToList())
{	
	element.Remove();		
}

magazijnOntvangsten.DescendantNodes().OfType<XElement>().Where (x => x.Name == "MOK_ONTVANGSTNUMMER").Dump();
magazijnOntvangsten.Save(xmlFilename);

//	.Element("KING_MAGAZIJNONTVANGSTEN")
//	.Elements("MAGAZIJNONTVANGSTEN")
//	.Elements("MAGAZIJNONTVANGST")
//	.Elements("MAGAZIJNONTVANGSTKOP")
//	.Elements("MOK_ONTVANGSTNUMMER")
//	.Dump();
	