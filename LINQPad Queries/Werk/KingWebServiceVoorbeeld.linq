<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
</Query>

string adminName = "DemoFin";
string toegangscode = "Test";
using (var client = new HttpClient())
{
  // Installatie 
	client.BaseAddress = new Uri("http://localhost:8080");	
  
  // Vanuit de debugger
  //client.BaseAddress = new Uri("http://localhost:8082");		
	
	string request = @"{
""GeefFoutTerug"": 0,
}";

  request = @"{ 
  ""ArchiefSoort""         : ""PostIn"", 
  ""InterneCode""          : ""123123123"", 
  ""ExterneCode""          : ""321321321"", 
  ""AangemaaktDoor""       : ""Test"", 
  ""Verwerking""           : ""GEEN"", 
  ""DagboekCode""          : ""Inkoop"", 
  ""Opmerking""            : ""Description"", 
  ""Afgehandeld""          : ""1"", 
  ""NawBestand""           : ""C"", 
  ""NawNummer""            : ""17777780"", 
  ""DatumDocument""        : ""2017-12-28"", 
  ""ContactPersoonNummer"" : ""000"", 
  ""Bestand""              : ""{{base64 encoded PDF file}}"" 
";

	var content = new StringContent(request, Encoding.UTF8, "application/json");
	content.Headers.Add("ACCESS-TOKEN", toegangscode);	

//	HttpResponseMessage response = await client.PostAsync($"{adminName}/Webservice_Test", content);	
HttpResponseMessage response = await client.PostAsync($"{adminName}/Archiefstuk_Toevoegen", content);	
	
	string resp = await response.Content.ReadAsStringAsync();
	resp.Dump("Response");
}