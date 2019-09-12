<Query Kind="Statements" />

string source = "Message=Ğiği, hexadecimale waarde 0x1A, is een ongeldig teken."; 

source.Any(c => !XmlConvert.IsXmlChar(c)).Dump("Bevat invalid chars");
var sb = new StringBuilder();

foreach(char c in source)
{
  if (XmlConvert.IsXmlChar(c))
    sb.Append(c);
}

sb.ToString().Dump();


source
  .Where(c => XmlConvert.IsXmlChar(c))
  .Aggregate(new StringBuilder(), (stringBuilder, c) => stringBuilder.Append(c))
  .Dump("Short version"); 




