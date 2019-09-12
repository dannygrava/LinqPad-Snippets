<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Namespace>System.Xml.Schema</Namespace>
</Query>

//XDocument xDebiteuren = new XDocument(new XElement("KING_DEBITEUREN", new XElement("DEBITEUREN")));			
XElement xDebiteuren = new XElement("DEBITEUREN");			

for (int i = 0; i< 1000; i++)
{
  XElement xDebiteur = new XElement("DEBITEUR", 
    new XElement("NAW_ZOEKCODE", $"TEST{i:D3}"),
    new XElement("NAW_VALUTACODE", "EUR"),
    new XElement("NAW_TAALCODE", "N"),
    new XElement("NAW_VESTIGINGADRES", 
      new XElement("ADR_NAAM1", $"CRM Test {i:D3}")
    )    
  );
  xDebiteuren.Add(xDebiteur);
}

XDocument doc = new XDocument(new XElement("KING_DEBITEUREN", xDebiteuren));			
xDebiteuren.Dump();
doc.Save(@"C:\Users\dg\Documents\King\Imports\XmlDebiteurenDemoArt.xml");

