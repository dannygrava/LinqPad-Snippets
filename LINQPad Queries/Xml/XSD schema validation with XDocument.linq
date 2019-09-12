<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Namespace>System.Xml.Schema</Namespace>
</Query>

string xsdMarkup =
	@"<xsd:schema xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
	   <xsd:element name='Root'>
		<xsd:complexType>
		 <xsd:sequence>
		  <xsd:element name='Child1' minOccurs='1' maxOccurs='1'/>
		  <xsd:element name='Child2' minOccurs='1' maxOccurs='1'/>
		 </xsd:sequence>
		</xsd:complexType>
	   </xsd:element>
	  </xsd:schema>";
XmlSchemaSet schemas = new XmlSchemaSet();
schemas.Add("", XmlReader.Create(new StringReader(xsdMarkup)));

XDocument doc1 = new XDocument(
    new XDeclaration("1.0", "ISO-8859-1", "yes"),
	new XElement("Root",
		new XElement("Child1", "content1"),
		new XElement("Child2", "content1")
	)
);

XDocument doc2 = new XDocument(
	new XElement("Root",
		new XElement("Child1", "content1"),
		new XElement("Child3", "content1")
	)
);

xsdMarkup.Dump("Xsd");
doc1.Dump("Contents doc1");
doc2.Dump("Contents doc2");

//"Validating doc1".Dump();
bool errors = false;
doc1.Validate(schemas, (o, e) =>
					 {
						 $"{e.Message}".Dump("Validating doc1");
						 errors = true;
					 });
("doc1 " + (errors ? "did not validate" : "validated")).Dump();


errors = false;
doc2.Validate(schemas, (o, e) =>
					 {
						 $"{e.Message}".Dump("Validating doc2");
						 errors = true;
					 });
("doc2 " + (errors ? "did not validate" : "validated")).Dump();
