<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.Formatters.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.Formatters.Soap.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
</Query>

var puertoEndpoint = "https://wms-dashboard-test3.azurewebsites.net/";
//var puertoEndpoint = "https://localhost:44348/";
//var token = @"eyJhbGciOiJSUzI1NiIsImtpZCI6IjZCN0FDQzUyMDMwNUJGREI0RjcyNTJEQUVCMjE3N0NDMDkxRkFBRTEiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJhM3JNVWdNRnY5dFBjbExhNnlGM3pBa2ZxdUUifQ.eyJuYmYiOjE1NjQ3Mjc5ODIsImV4cCI6MTU2NTkzNzU4MiwiaXNzIjoiaHR0cHM6Ly9pbnRlcm4ta2lzLWdhdGV3YXkuYXp1cmV3ZWJzaXRlcy5uZXQiLCJhdWQiOlsiaHR0cHM6Ly9pbnRlcm4ta2lzLWdhdGV3YXkuYXp1cmV3ZWJzaXRlcy5uZXQvcmVzb3VyY2VzIiwia2luZzUiXSwiY2xpZW50X2lkIjoicHVlcnRvIiwic3ViIjoiMTI1OTkwMDI6ZGFubnkiLCJhdXRoX3RpbWUiOjE1NjQ3Mjc5ODIsImlkcCI6ImxvY2FsIiwibmFtZSI6IkRhbm55IEdyYXZhIiwiS2luZzVDcmVkZW50aWFsIjoiZGFubnkiLCJMaWNlbnRpZU51bW1lciI6IjEyNTk5MDAyIiwiU2Vzc2lvbklkIjoiZmYzOWVjNzM1ZjY1NDE0NDgzMDUzZTdlMjdiYzYyNGUiLCJzY29wZSI6WyJraW5nNSJdLCJhbXIiOlsicHdkIl19.mspeVE7q285yznwweVBlUotp1jv57rYFfvJMCWGvtAWHkkMFMYr-4cenFcP-EafYRunSz65D86IzvneguLVuwWUXUHFkAnHIDZMCn2zFpQqixQLHqeOCbkRfzo9WBM3693mqCie_8h7UJMtewjv_ajpBSNkZRhJH0xg9ClHnV4LGhM0pJRdv7QC4mwb8UooH3oykovEe99RT07RdMkc4HlJ7tTJA7IDY38H5wMG0ozX8Br4QpKjrzQDTWjwKkQsn3AQKdrh_n1GikLUY5diVGTiY6z_bjotGdzvz8hIEbEuVwoE-Aj-cx1WJ7FT33e2q6kUBc08QREniY2foz14vaQ";

var loginClient = new HttpClient { BaseAddress = new Uri(puertoEndpoint) };
loginClient.DefaultRequestHeaders.Accept.Clear();
loginClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//loginClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

var requestData = new {__context= new {Licentienummer="12599002", Admincode="DemoArt", Gebruikersnaam="Danny"}};

var content = new StringContent(JsonConvert.SerializeObject(requestData).Dump("JSON"));
content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

HttpResponseMessage responseMessage = await loginClient.PostAsync("OrderpickInstelling/Get", content);            
var responseString = await responseMessage.Content.ReadAsStringAsync();

if (responseMessage.StatusCode == HttpStatusCode.OK)
{
  responseString.Dump("Response");
}
else
{
  responseMessage.Dump("Response message");
  responseString.Dump("Response content");
}