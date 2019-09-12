<Query Kind="Statements" />

//var bytes = Encoding.GetEncoding("Windows-1252").GetBytes("coöperatie").Dump();
var bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes("coöperatie").Dump();
var value = Encoding.UTF8.GetString(bytes).Dump();
Encoding.UTF8.GetBytes(value).Dump().Select(b=> b.ToString("X")).Dump();

((int)value[2]).ToString("X").Dump();

XDocument doc = new XDocument();
doc.Declaration = new XDeclaration("1.0", "utf-8", "yes");
doc.Add(new XElement("Document", new XElement("Test", value), new XElement("Test2", "coöperatie")));
doc.ToString().Dump();
doc.Save(@"C:\Users\Danny\Documents\test.xml");
var doc2 = XDocument.Load(@"C:\Users\Danny\Documents\test.xml").Dump();
doc2.Root.Element("Test2").Value.Dump();

//var fs = new FileStream(@"C:\Users\Danny\Documents\test.xml", FileMode.Open);
//fs.Read();

