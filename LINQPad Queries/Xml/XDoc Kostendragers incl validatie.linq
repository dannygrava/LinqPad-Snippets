<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Namespace>System.Xml.Schema</Namespace>
</Query>

XDocument doc = new XDocument(
		new XElement("KING_KOSTENDRAGERS",
			new XElement("KOSTENDRAGERS", 
				new XElement("KOSTENDRAGER",
					new XElement("KD_NUMMER", "202"),
					new XElement("KD_ZOEKCODE", "ZC202"),
					new XElement("KD_OMSCHRIJVING", "Kostendrager 202") 					
				),
				new XElement("KOSTENDRAGER",
					new XElement("KD_NUMMER", "203"),
					new XElement("KD_ZOEKCODE", "ZC203"),
					new XElement("KD_OMSCHRIJVING", "Kostendrager 203"),
					new XElement("KD_AANMAAKDATUM", DateTime.Today)
				)				
			)			
		)
	);

System.Xml.Schema.XmlSchemaSet schemas = new XmlSchemaSet();
using (var fs = File.Open(@"D:\King_Trunk\Sources\App\Sys\XmlImport\Xml\KingKostendragers.xsd", FileMode.Open))
{
	using (var reader = XmlReader.Create(fs))
	{
		schemas.Add("", reader); // "" geeft aan dat er geen namespace prefix is
		doc.Validate(schemas, (o, e) => Console.WriteLine(e.Message));
	}
}
doc.Validate(schemas, (o, e) => {Console.WriteLine("{0}", e.Message);});