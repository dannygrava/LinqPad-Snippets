<Query Kind="Statements" />

// XHTML is weliswaar XML syntax maar bevat unresolved references als &nbsp welke staan in de http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd.
// Omdeze referentie op te lossen moet gebruik gemaakt worden van een resolver.
// Zie ook: http://blogs.pingpoet.com/overflow/archive/2005/07/20/6607.aspx

// In het kort de volgende leermomenten
// 1. Gebruik een resolver om XHTML te parsen voor het oplossen van &nbsp e.d.
// 2. XElement.Value() is eigenlijk een XName object, deze bevat een namespace.
// 3. XName heeft een impliciete operator die een string omzet naar een XName, de namespace wordt opgegegeven middels "{myNamespace}elementNaam"
// 4. Om een element te vinden op een onbekende plaats in de hierarchie: gebruik methode Descendants(). 
// 4a. XPath niet geschikt, omdat hier het pad naar het element moet worden opgegeven.

// Create a resolver with default credentials.
XmlUrlResolver resolver = new XmlUrlResolver();
resolver.Credentials = System.Net.CredentialCache.DefaultCredentials;

// Set the reader settings object to use the resolver.
XmlReaderSettings settings = new XmlReaderSettings();
settings.XmlResolver = resolver;
settings.ProhibitDtd = false;

// Create the XmlReader object.
XmlReader reader = XmlReader.Create("HTMLTable.htm".FromLinqpadDataFolder(), settings);

// Vanaf hier kunnen we verder spelen met XLINQ
var doc = XDocument.Load (reader);

var rootElements = from tr in doc.Elements() select tr;
var allRootChildren = 
  from tr in rootElements.First().Elements() 
  where tr.Name == "{http://www.w3.org/1999/xhtml}body" 
  select new {tr.Name.LocalName, tr.Name.Namespace, tr.Name.NamespaceName};

XNamespace ns = "http://www.w3.org/1999/xhtml"; // ns.ToString() returns "{http://www.w3.org/1999/xhtml}"
var allTables = 
  from table in doc.Elements (ns + "html").Elements (ns + "body").Elements()//Elements("{http://www.w3.org/1999/xhtml}table").Elements() 
  select table.Name;
  
var allTables2 = 
  from table in doc.Descendants(ns + "tr")
  //where table.Attribute("class").Value != "toc"
  select table;
  
allTables2.Dump("haal uit html alleen de tabel van het type class <> toc");
//allTables.Dump ("all tables");
//allRootChildren.Dump("Eerste tr");
//doc.XPathSelectElement("html").Dump();
//doc.Dump();