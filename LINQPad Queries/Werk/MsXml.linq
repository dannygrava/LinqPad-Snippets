<Query Kind="Statements" />

{
	new Hyperlinq("http://msdn.microsoft.com/en-us/library/ms760218(v=vs.85).aspx", "MSDN XML DOM Objects/Interfaces").Dump();
	dynamic doc = Activator.CreateInstance(Type.GetTypeFromProgID("Msxml2.DOMDocument.6.0"));
	doc.appendChild(doc.createProcessingInstruction("xml", "version=\"1.0\" standalone=\"yes\""));	

	dynamic id = doc.createAttribute("id");
	dynamic status = doc.createAttribute("status");
	id.Value = "123";
	status.Value = "archived";
	
	dynamic firstname = doc.createElement("firstname");
	dynamic lastname = doc.createElement("lastname");
	firstname.appendChild(doc.createTextNode("Jim"));
	lastname.appendChild(doc.createTextNode("Bo"));
	
	dynamic customer = doc.CreateElement("customer");
	customer.attributes.setNamedItem(id);
	customer.attributes.setNamedItem(status);
	customer.appendChild(lastname);
	customer.appendChild(firstname);	
	
	doc.appendChild(customer);
	
	//doc.insertBefore(doc.createProcessingInstruction("xml", "version=\"1.0\""), doc.childNodes.item(0));
	LINQPad.Extensions.Dump(doc.xml, "XmlDocument");	
	LINQPad.Extensions.DumpFormatted(XDocument.Parse(doc.xml));
}


{
	// XmlDocument building...lijkt sterk op MsXml
	XmlDocument doc = new XmlDocument();
	doc.AppendChild(doc.CreateXmlDeclaration("1.0", null, "yes"));
	
	XmlAttribute id = doc.CreateAttribute("id");
	XmlAttribute status = doc.CreateAttribute("status");
	id.Value = "123";
	status.Value = "archived";
	
	var firstname = doc.CreateElement("firstname");
	var lastname = doc.CreateElement("lastname");
	firstname.AppendChild(doc.CreateTextNode("Jim"));
	lastname.AppendChild(doc.CreateTextNode("Bo"));
	
	XmlElement customer = doc.CreateElement("customer");
	customer.Attributes.Append(id);
	customer.Attributes.Append(status);
	customer.AppendChild(lastname);
	customer.AppendChild(firstname);
	
	doc.AppendChild(customer);
	
	//doc.OuterXml.Dump();
	doc.DumpFormatted();
}



//dynamic xmlDoc = Activator.CreateInstance(Type.GetTypeFromProgID("Msxml2.DOMDocument.6.0"));
//
//xmlDoc.async = false;
//xmlDoc.loadXML(@"<root>
//	<child/>
//</root>");
//
//dynamic pi = xmlDoc.createProcessingInstruction("xml", "version=\"1.0\"");
//xmlDoc.insertBefore(pi, xmlDoc.childNodes.item(0));
//
//dynamic element = xmlDoc.createElement("ARTIKEL");
////element.nodeValue = "124";
//
////element.appendChild(xmlDoc.createTextNode("COMPUTERKAST001"));
//element.Text = "COMPUTERKAST001";
//xmlDoc.documentElement.appendChild(element);
//
//element = xmlDoc.createElement("ARTIKEL");
//element.appendChild(xmlDoc.createTextNode("COMPUTERKAST001"));
//xmlDoc.documentElement.appendChild(element);
//
//
//dynamic textNode = xmlDoc.createTextNode("ARTIKEL_CODE");
//textNode.nodeValue = "COMPUTERKAST002";
//
//xmlDoc.documentElement.appendChild(textNode);
////xmlDoc.documentElement.createTextNode("BR-DVD");
//LINQPad.Extensions.Dump(xmlDoc.xml);