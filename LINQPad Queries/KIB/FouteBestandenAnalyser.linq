<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

// Wordt aangevuld met de bestanden die we aantreffen als foute uploads richting KIB

/// <summary>
/// Bepaalt de encoding op basis van de eerste bytes van de aangeleverde byte array (de 'Preamble').
/// De array dient minstens vier lang te zijn.
/// Als geen Preamble kan worden gevonden (niet alle encodings hebben deze), dan wordt ASCII teruggegeven.
/// 
/// Voor een overzichtje preambles van de verschillende encodings ondersteund in .NET:
/// https://msdn.microsoft.com/en-us/library/system.text.encoding.getpreamble(v=vs.110).aspx
/// </summary>

Encoding GetEncoding(byte[] bytes)
{
    Debug.Assert(bytes.Length >= 4);

    // Analyze the BOM
    if (bytes[0] == 0x2b && bytes[1] == 0x2f && bytes[2] == 0x76) return Encoding.UTF7;
    if (bytes[0] == 0xef && bytes[1] == 0xbb && bytes[2] == 0xbf) return Encoding.UTF8;
    if (bytes[0] == 0xff && bytes[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
    if (bytes[0] == 0xfe && bytes[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
    if (bytes[0] == 0 && bytes[1] == 0 && bytes[2] == 0xfe && bytes[3] == 0xff) return Encoding.UTF32;
    return Encoding.ASCII;
}

Dictionary<string, string> namespaces = new Dictionary<string, string> {
  {"urn:iso:std:iso:20022:tech:xsd:camt.052.001.02", "Camt 052"},
  {"urn:iso:std:iso:20022:tech:xsd:camt.053.001.02", "Camt 053"},  
  {"urn:iso:std:iso:20022:tech:xsd:pain.001.001.03", "Pain betalingsopdracht"},
  {"urn:iso:std:iso:20022:tech:xsd:pain.008.001.02", "Pain incasso-opdracht"},
};

List<Tuple<string, string, string>> results = new List<Tuple<string, string, string>> ();

//foreach (var filename in Directory.GetFiles(@"King".FromDocumentsFolder()))
foreach (var filename in Directory.GetFiles(@"FouteCamts".FromDownloadFolder()))
//foreach (var filename in Directory.GetFiles(@"".FromDocumentsFolder()))
{  
  using (var stream = new FileStream(filename, FileMode.Open))
  {

    byte[] leadBytes = new byte[100];
    stream.Position = 0;
    stream.Read(leadBytes, 0, 100);   
       
    // Meest voorkomende PK\003\004 (https://en.wikipedia.org/wiki/Zip_(file_format)
    // In PK\005\006 (empty archive) en PK\007\008 (spanned archive) zijn we niet geinteresseerd
    if (leadBytes[0] == 'P' && leadBytes[1] == 'K' )
    {
      if (leadBytes[2] == 3 && leadBytes[3] == 4)
        results.Add(new Tuple<string, string, string> (filename, "Zip", ""));        
      if (leadBytes[2] == 5 && leadBytes[3] == 6)
        results.Add(new Tuple<string, string, string> (filename, "Empty archive", ""));        
      if (leadBytes[2] == 7 && leadBytes[3] == 8)      
        results.Add(new Tuple<string, string, string> (filename, "Spanned Zip archive", ""));
    }
    else if (leadBytes[0] == '%' && leadBytes[1] == 'P' && leadBytes[2] == 'D' && leadBytes[3] == 'F')
    {
      results.Add(new Tuple<string, string, string> (filename, "Pdf", ""));               
    }
    else if (leadBytes[0] == 'R' && leadBytes[1] == 'a' && leadBytes[2] == 'r' && leadBytes[3] == '!')
    {
      results.Add(new Tuple<string, string, string> (filename, "Rar", ""));               
    }    
    else if (leadBytes[0] == 'M' && leadBytes[1] == 'Z' && leadBytes[1] == 'P')
    {
      results.Add(new Tuple<string, string, string> (filename, "Win exe", ""));
    }        
    else if (leadBytes[0] == 'M' && leadBytes[1] == 'Z')
    {
      results.Add(new Tuple<string, string, string> (filename, "Dos exe", ""));               
    }        
    else
    {
      // NOTE DG Niet zomaar bytes checken, er kunnen BOM's e.d. in zitten
      // Misschien zuiverder om de BOM/Preamble uit de string moeten halen en dan StartsWith gebruiken,
      // maar dit werkt ook en mogelijk ook iets robuuster.
      string startOfStream = GetEncoding(leadBytes).GetString(leadBytes);
      //startOfStream.Dump();
      if (startOfStream.StartsWith("<?xml"))
      {
        stream.Position = 0;
          //"StreamType.Xml".Dump(filename);
        var doc = XDocument.Load(stream);
        if (namespaces.ContainsKey(doc.Root.GetDefaultNamespace().ToString()))
          results.Add(new Tuple<string, string, string> (filename, namespaces[doc.Root.GetDefaultNamespace().ToString()], ""));               
          
        else
          results.Add(new Tuple<string, string, string> (filename, "Xml, niet herkend", doc.Root.GetDefaultNamespace().ToString()));                         
      }
      else if (startOfStream.IndexOf("<!DOCTYPE html>", StringComparison.OrdinalIgnoreCase) >= 0)
      {
        results.Add(new Tuple<string, string, string> (filename, "Html", ""));                         
      }
      else
      {        
        // TODO Read with StreamReader
        if (string.Equals(Path.GetExtension(filename), ".csv", StringComparison.OrdinalIgnoreCase))
          results.Add(new Tuple<string, string, string> (filename, "Csv", startOfStream));                         
        else if (string.Equals(Path.GetExtension(filename), ".txt", StringComparison.OrdinalIgnoreCase))
          results.Add(new Tuple<string, string, string> (filename, "Txt", startOfStream));
        else          
        {          
          results.Add(new Tuple<string, string, string> (filename, "Bestand, niet herkend", startOfStream));                         
        }
      }
    }
  }
}

results.GroupBy (x => x.Item2).Dump(2);