<Query Kind="Statements" />

string filename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Kostendragers_linqPad.xml";
using (var output = File.Create(filename))
{
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

	var settings = new XmlWriterSettings {Indent=true, Encoding = Encoding.UTF8};
	using (XmlWriter xw = XmlWriter.Create(output, settings))
  		doc.Save(xw);
	doc.Dump();
}