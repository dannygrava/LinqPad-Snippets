<Query Kind="Statements">
  <Reference>D:\KingInternetBankieren\KIBDomain\bin\Debug\KIBDomain.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IO.Compression.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Quadrant.KIB</Namespace>
  <Namespace>Quadrant.KIB.DocumentModels.Ravk</Namespace>
  <Namespace>Quadrant.KIB.Rekeningafschriften</Namespace>
  <Namespace>Quadrant.KIB.Rekeningafschriften.Camt</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.IO.Compression</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

//"C:\Users\dg\Downloads\FouteCamts\20171201_104059_3lhpgxmj.xml"
//"C:\Users\dg\Downloads\FouteCamts\20171201_104327_ii5kdmk3.xml"
//"C:\Users\dg\Downloads\FouteCamts\20171201_104538_dm52siff.xml"
//"C:\Users\dg\Downloads\FouteCamts\20171201_104835_tghfqot4.xml"
//"C:\Users\dg\Downloads\FouteCamts\20171201_105107_4rvh5u34.xml"
//"C:\Users\dg\Downloads\FouteCamts\20171201_105245_dhjkopx0.xml"
//"C:\Users\dg\Downloads\FouteCamts\20171201_120909_qjczea5z.xml"

StreamType streamType;
foreach (var filename in Directory.GetFiles(@"FouteCamts".FromDownloadFolder(), "*.xml"))
{
  filename.Dump("Bestand");
  Stream stream = new FileStream(filename, FileMode.Open);
  
  var ravk = RekeningafschriftConverter.VerwerkStream(stream, out streamType);  
  
  if (ravk.Kop.Meldingen.Any().Dump("Meldingen op de kop"))
      ravk.Kop.Meldingen.Dump();
  ravk.Afschriften.Where (x => x.Meldingen.Any(m => m.Code < 200)).Dump("Melding op Afschrift");
  ravk.Afschriften.SelectMany(a => a.Regels).Where(r => r.Meldingen.Any(m => m.Code < 200)).Dump("Meldingen op Regels");
}