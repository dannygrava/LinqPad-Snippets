<Query Kind="Statements" />

foreach (string file in Directory.GetFiles(@"D:\King_Trunk\Sources\RAP\Vls\", "*.xml"))
{			
	XDocument xdoc = XDocument.Load(XmlReader.Create(file), LoadOptions.None);			
	var xquery = xdoc
		.Descendants("VLS_FIELD")
// Alternatief voor bovenstaande 2: 
//		.DescendantNodes()
//		.OfType<XElement>()
//		.Where(n => n.Name=="VLS_FIELD")
// Alternatief voor bovenstaande:
//	var xquery = xdoc
//		.Element("VOORLOOPSCHERM")
//		.Element("VLS_FIELDS")
//		.Elements("VLS_FIELD")
		.Where (x => x.Elements("FIELD_DATATYPE").Any())
		.Select(x => new {Fieldname = x.Element("FIELD_NAME").Value, DataType = x.Element("FIELD_DATATYPE").Value})
		.Dump()
		;
		
//	if (xquery.Any())
//	{
//		xquery.Dump(file);	
//		;
//	}
}