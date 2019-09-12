<Query Kind="Statements">
  <Namespace>System.Xml.Schema</Namespace>
</Query>

//string vlsFilename = @"D:\King_Enter\Sources\RAP\Vls\OfferteRange.xml";

XmlSchemaSet schemas = new XmlSchemaSet();
//using (var fs = File.Open(@"D:\King_Trunk\Sources\RAP\Vls\xsd\Voorloopscherm.xsd", FileMode.Open))
//{
//	using (var reader = XmlReader.Create(fs))
//	{
		string targetnamespace = "urn:iso:std:iso:20022:tech:xsd:pain.001.001.03";
//		schemas.Add(targetnamespace , reader);

		//schemas.Add(targetnamespace , @"D:\King_Trunk\Sources\RAP\Vls\xsd\Voorloopscherm.xsd");
    schemas.Add(targetnamespace , @"C:\Users\dg\Documents\King\Bovk\pain.001.001.03.xsd");
    var file = @"C:\Users\dg\Documents\King\Bovk\PAIN_20171030_001.XML";
			try
			{			
				XDocument xdoc = XDocument.Load(XmlReader.Create(file), LoadOptions.None);			
				xdoc.Validate(schemas, (o, e) => (file + "\n\t\t" + e.Message).Dump());				
				(file + "\t\t OK").Dump();
			}
			catch (Exception ex)
			{
				(file + "\n\t\t" + ex.Message).Dump();
			}
	
//	}
//}