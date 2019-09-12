<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
</Query>

// DG Lokaal
var puertoEndpoint = "https://intern-puerto-gateway.azurewebsites.net";
var userName = "Danny";
var password = "Capelle1";
var licentieId = 12599002;

var clientApplicationKey = "ae98971f-7b02-41ce-901f-f3d9d1268ec8";  // key voor GetBearerToken

var loginClient = new HttpClient { BaseAddress = new Uri(puertoEndpoint) };
loginClient.DefaultRequestHeaders.Accept.Clear();
loginClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

var formContent = new FormUrlEncodedContent(new[]
{
    new KeyValuePair<string, string>("grant_type", "password"),
    new KeyValuePair<string, string>("username", userName),
    new KeyValuePair<string, string>("password", password),
});

formContent.Headers.Add("LicenceId", licentieId.ToString());
formContent.Headers.Add("GatewayAccess", "K5"); //???
formContent.Headers.Add("ClientApplicationKey", clientApplicationKey.ToString());

HttpResponseMessage responseMessage = await loginClient.PostAsync("/Token", formContent);            
var responseString = await responseMessage.Content.ReadAsStringAsync();

if (responseMessage.StatusCode == HttpStatusCode.OK)
{
  //get access token from response body
  var jObject = JObject.Parse(responseString);

  jObject.GetValue("token_type").Dump("TokenType");
  ($"bearer {jObject.GetValue("access_token")}").Dump("AccessToken");
}
else
{
  responseMessage.Dump("Response message");
  responseString.Dump("Response content");
}