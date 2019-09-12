<Query Kind="Statements">
  <Namespace>System.Xml.Schema</Namespace>
</Query>

XDocument doc = new XDocument(
	new XElement("KING_ORDERS",
		new XElement("ORDERS", 
			new XElement("ORDER",
				new XElement("ORDERKOP", 
					new XElement("ORK_DEBITEURNUMMER", "12000002"),
					new XElement("ORK_PRIJZENINEXBTW", "EXCLBTW")
				),
				new XElement("ORDERREGELS",
					new XElement("ORDERREGEL",
						new XElement("ORR_SOORT", "ART"),
						new XElement("ORR_ARTIKELNUMMER", "Mozzarella"),
						new XElement("ORR_AANTALBESTELD", 5),
						new XElement("ORR_PRIJS", 3.95)
						)
					)
				)
			)	
		)			
	);
	
doc.Element("KING_ORDERS").Element("ORDERS").Element("ORDER").Element("ORDERKOP").Element("ORK_DEBITEURNUMMER").Value.Dump();
doc.Dump();
return;

// save document to file
string filename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Orders_linqPad.xml";
using (var output = File.Create(filename))
{

	var settings = new XmlWriterSettings {Indent=true, Encoding = Encoding.UTF8};
	using (XmlWriter xw = XmlWriter.Create(output, settings))
  		doc.Save(xw);	
	
	// Valideren tegen het KingOrders.xsd
	XmlSchemaSet schemas = new XmlSchemaSet();
	schemas.Add("", XmlReader.Create(File.Open(@"D:\King_Dakar\APP\sys\XmlImport\Xml\KingOrders.xsd", FileMode.Open)));
	doc.Validate(schemas, (o, e) =>
					 {
						 e.Dump("Schema validatie fout");						 
					 });
}