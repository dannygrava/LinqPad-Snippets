<Query Kind="Statements" />

XNamespace _ns = "urn:iso:std:iso:20022:tech:xsd:camt.053.001.02";

var doc = XDocument.Load(@"C:\Users\dg\Downloads\FouteCamts\20171218_104229_s3u4q0gm.xml");

var AmountPattern = @"^[\+]{0,1}[0-9]{1,15}[.][0-9]{2}$";
var IntegerPattern = @"^[\+]{0,1}[0-9]{1,18}$";

doc.Descendants(_ns + "Amt")
	.Select(x => new {x.Value, IsCorrectFormat = Regex.IsMatch(x.Value, AmountPattern)})
	//.Dump("Alle Amt tags")
	;

doc.Descendants(_ns + "ElctrncSeqNb")
	.Select(x => new {x.Value, x.Value.Length, IsCorrectFormat = Regex.IsMatch(x.Value, IntegerPattern)})
	//.Dump("Alle ElctrncSeqNb tags")
	;

doc.Descendants(_ns + "LglSeqNb")
	.Select(x => new {x.Value, x.Value.Length,  IsCorrectFormat = Regex.IsMatch(x.Value, IntegerPattern)})
	//.Dump("Alle LglSeqNb tags")
	;
	
doc.Descendants(_ns + "IBAN")
	.Select(node => node.Value)
	.Distinct()
	.Dump("Alle IBANS")
	;
	

//	XNamespace _ns = "urn:iso:std:iso:20022:tech:xsd:camt.053.001.02";
//var xmls = new DirectoryInfo (@"D:\Users\dg\Documents\King\CAMT")
//	.GetFiles("*.xml", SearchOption.AllDirectories);	
//
//foreach (var xmlFile in xmls)
//{
//	try
//	{
//		var doc = XDocument.Load(xmlFile.FullName);
//		if (doc.Descendants(_ns + "Stmt").Count() > 1)
//			xmlFile.FullName.Dump("Meer dan 1");	
//	}
//	catch
//	{}
//}