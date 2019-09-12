<Query Kind="Statements">
  <Reference>D:\KingInternetBankieren\KIBDomain\bin\Debug\KIBDomain.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IO.Compression.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Quadrant.KIB.Betalingsopdrachten</Namespace>
  <Namespace>Quadrant.KIB.DocumentModels.Bovk</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.IO.Compression</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

const string OverigeFout       = "Overige fout";
const string WaarschuwingOms   = "Omschrijving aangepast";
const string FoutOpdrachtDatum = "Opdrachtdatum fout";
const string FoutBankrekNaam   = "Naam bankrekening niet ingevuld";

string GetBekendeFouten(string code, string  id)
{
  const int WARNING = 200;
  int foutcode = int.Parse(code);
  if (foutcode >= WARNING && id == "Oms")
    return WaarschuwingOms;
  if (foutcode < WARNING  && id =="OpdrachtDatum")
    return FoutOpdrachtDatum;
  if (foutcode < WARNING && id =="Bankrek.Naam")
    return FoutBankrekNaam;
    
  return OverigeFout;
}

XNamespace ns = "urn:iso:std:iso:20222:tech:xsd:crvk.001.001.01";
var serializer = JsonSerializer.Create();

foreach (var filename in Directory.GetFiles(@"FouteBovks".FromDownloadFolder(), "*.txt"))
{  
  using (var reader = new StreamReader(new FileStream(filename, FileMode.Open)))
  //using (var reader = new JsonTextReader(new StreamReader(new FileStream(filename, FileMode.Open))))
  {
    var bovk = (BovkDocument) serializer.Deserialize(reader, typeof(BovkDocument));
    //var bovk = serializer.Deserialize<BovkDocument>(reader);
    //bovk.Dump();
    reader.BaseStream.Position = 0;
    var bovk0 = (BovkDocument) serializer.Deserialize(reader, typeof(BovkDocument));
        
    var archive = new ZipArchive(BovkVerwerker.Verwerk(bovk.Bovk).Archive);
    //archive.Entries.Dump("Entries");    
    var xdoc = XDocument.Load(archive.Entries.ElementAt(0).Open());    
    var fouten = xdoc
      .Descendants()
      .Where(x => x.Name==ns+"Melding")    
      .Select(x => new {E2E=x.Parent.Parent.Element(ns+"E2E").Value, Code=x.Element(ns+"Code").Value, Id=x.Element(ns+"ID").Value, Oms=x.Element(ns+"Oms").Value, Fout=GetBekendeFouten(x.Element(ns+"Code").Value, x.Element(ns+"ID").Value)})      
      ;
    
    fouten
      .GroupBy(f => f.Fout)
      .Select (g => new {Fout=g.Key, Aantal=g.Count(), items=g.Select(f => new {f, kop=bovk.Bovk.AlleOpdrachten.Single(opr => opr.Opdrachten.Any(o=> o.E2E ==f.E2E)) , origineel= bovk0.Bovk.AlleOpdrachten.SelectMany(x => x.Opdrachten).Single(x => x.E2E ==f.E2E), naverwerking=bovk.Bovk.AlleOpdrachten.SelectMany(x => x.Opdrachten).Single(x => x.E2E ==f.E2E)})})
      
      .Dump(filename, 1);        
  }
}