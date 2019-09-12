<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

var jobParams = new 
{
  rapportsoort = 410,
  layoutnummer = 905,
  selecties = new {
    Selectie1 = "Nummer",
    Selectie2 = "groep"
    },
  vlsparams = new {
    ArtCode_Van =  "COMPUTERKAST001", 
    ArtCode_Tot =  "COMPUTERKAST001",
    OpbrGrpNummer_Van = "00",
    OpbrGrpNummer_Tot = "99",    
    AantalEtiketten = 2, /* Aantal zelf op te geven*/
    AantalCopieEtiketten = 5,
    NietAfdrukkenIndien= "0,1,2,3",
    Taalcode = "N",
    Sortering =0,
    Ascending = true
  }    
};

var jobParamsMagOntvangsten = new 
{ 
  rapportsoort = 421, // Etiketten magazijnontvangsten
  layoutnummer = 904,
  //selecties = new {},
  vlsparams = new {
    MokNummer_Van =  11, 
    MokNummer_Tot =  11
  }    
};

JsonConvert.SerializeObject(jobParamsMagOntvangsten).Dump("Unformatted job params MagazijnOntvangsten");

var jsonJobParams = JsonConvert.SerializeObject(jobParams).Dump("Unformatted");
JsonConvert.SerializeObject(jobParams, new JsonSerializerSettings() {Formatting = Newtonsoft.Json.Formatting.Indented}).Dump("Indented");
var admin = "DemoArt";
var jobnr = "099";

//jsonJobParams =@"{""rapportsoort"":410,""layoutnummer"":904,""selecties"":{""Selectie1"":""Nummer"",""Selectie2"":""groep""},""vlsparams"":{""ArtCode_Van"":""MICROSOFT OFFICE2013"",""ArtCode_Tot"":""MICROSOFT OFFICE2013"",""OpbrGrpNummer_Van"":""00"",""OpbrGrpNummer_Tot"":""99"",""AantalEtiketten"":2,""AantalCopieEtiketten"":5,""NietAfdrukkenIndien"":""0,1,2,3"",""Taalcode"":""N"",""Sortering"":0,""Ascending"":true}}" ;

// Execute KingJob
Process process = new Process();
process.StartInfo.FileName = @"D:\King_Trunk\Run\KingJob.exe";        
process.StartInfo.Arguments = $"EA {admin} Job {jobnr} JOBPARAMS {jsonJobParams}";
process.StartInfo.Arguments.Dump("Arguments");

process.StartInfo.UseShellExecute = false;
process.StartInfo.RedirectStandardOutput = true;        
process.Start();

// Synchronously read the standard output of the spawned process. 
StreamReader reader = process.StandardOutput;
string output = reader.ReadToEnd();

// Write the redirected output to this application's window.
//Console.WriteLine(output);
output.Dump("KingJob met JobsParam");

process.WaitForExit();
process.ExitCode.Dump("Exit code");
process.Close();