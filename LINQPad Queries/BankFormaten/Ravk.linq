<Query Kind="Statements" />

XNamespace xs = "urn:iso:std:iso:20222:tech:xsd:ravk.001.001.01";
XNamespace xsiNs = "http://www.w3.org/2001/XMLSchema-instance";

XElement afschrift = new XElement(xs + "Afschrift");

var afschriften = new List<XElement> {afschrift, afschrift, afschrift};

XDocument xDoc = new XDocument(
  new XDeclaration("1.0", "UTF-8", "yes"),
  new XElement(xs + "Document",
    new XAttribute(XNamespace.Xmlns + "xsi", xsiNs),
    new XElement(xs + "Ravk", 
    	new XElement(xs + "Kop", 
    		new XElement(xs + "KIBVersie", "1"),
    		new XElement(xs + "Datum", DateTime.Today)
    	),
    	afschriften
    )
  )
);

var settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8};
var output = new StringBuilder();
using (XmlWriter xw = XmlWriter.Create(output, settings))
    xDoc.Save(xw);

output.ToString().Dump();


//xDoc.Element(xs+"Document").Element(xs+"Ravk").Element(xs+"Kop").Element(xs+"KIBVersie").Dump("Via Element");
xDoc.Descendants(xs+"KIBVersie").Dump("Via Descendants");

var namespaceManager = new XmlNamespaceManager(new NameTable());
namespaceManager.AddNamespace("ravk", xs.ToString());
namespaceManager.DefaultNamespace.Dump("Default namespace");
xDoc.XPathSelectElements("/ravk:Document/ravk:Ravk/ravk:Kop/ravk:KIBVersie", namespaceManager).Dump("Via XPathSelectElements").First().Value.Dump();
xDoc.Descendants().Where(xe => xe.Elements().Count() == 0).Dump("All");