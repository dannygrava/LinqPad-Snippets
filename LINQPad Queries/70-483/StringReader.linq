<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

var stringWriter = new StringWriter();
using (XmlWriter writer = XmlWriter.Create(stringWriter))
{
writer.WriteStartElement("book");
writer.WriteElementString("price", "19.95");
writer.WriteEndElement();
writer.Flush();
}
string xml = stringWriter.ToString().Dump();

var stringReader = new StringReader(xml);
using (XmlReader reader = XmlReader.Create(stringReader))
{
	reader.ReadToFollowing("price");
	decimal price = decimal.Parse(reader.ReadInnerXml(), new CultureInfo("en-US")); // Make sure that you read the decimal part correctly
	price.Dump();
}