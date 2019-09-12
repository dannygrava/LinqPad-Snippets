<Query Kind="Statements" />

//var doc = new XDocument(new XDeclaration("1.0","ISO-8859-1", "no"), new XElement("test", "data"));
var doc = new XDocument(new XElement("test", "€"));
doc.Dump();
var output = new StringBuilder();
//var output = File.Create(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/test.xml");
//var encoding = Encoding.GetEncoding(28605);
var encoding = Encoding.UTF8;
//var encoding = Encoding.GetEncoding(1252);
//var encoding = Encoding.GetEncoding(28591);
var settings = new XmlWriterSettings {Indent=true, Encoding = encoding};
//var settings = new XmlWriterSettings {Indent=true, Encoding = Encoding.UTF8};
using (XmlWriter xw = XmlWriter.Create(output, settings))
  doc.Save(xw);
  
output.Dump();

encoding.Dump();
//output.Close();