<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

XNamespace _ns = "urn:iso:std:iso:20022:tech:xsd:pain.008.001.02";

var doc = XDocument.Load(@"C:\Users\dg\Downloads\Incasso_4.xml");

var AmountPattern = @"^[\+]{0,1}[0-9]{1,15}[.][0-9]{2}$";
var IntegerPattern = @"^[\+]{0,1}[0-9]{1,18}$";

var orgnlDbtrAccts = doc.Descendants(_ns + "OrgnlDbtrAcct");
foreach (var acct in orgnlDbtrAccts)
{
	acct.Dump("Before");
	acct.RemoveAll();	
	acct.Add(new XElement(_ns + "Id", new XElement(_ns + "Othr", new XElement(_ns + "Id", "SMNDA"))));
	acct.Dump("After");
}

//doc.Save(@"C:\Users\dg\Downloads\Incasso_Kunstkwartier.xml");
	

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