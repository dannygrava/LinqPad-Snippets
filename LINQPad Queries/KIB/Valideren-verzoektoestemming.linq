<Query Kind="Statements">
  <Reference Relative="..\..\..\..\Documents\Visual Studio 2013\Projects\Getting Started with ASP.NET Web API (Tutorial Sample)\C#\packages\Newtonsoft.Json.6.0.4\lib\net45\Newtonsoft.Json.dll">&lt;MyDocuments&gt;\Visual Studio 2013\Projects\Getting Started with ASP.NET Web API (Tutorial Sample)\C#\packages\Newtonsoft.Json.6.0.4\lib\net45\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Reference Relative="..\..\..\..\Documents\Visual Studio 2013\Projects\Getting Started with ASP.NET Web API (Tutorial Sample)\C#\packages\Microsoft.AspNet.WebApi.Client.5.2.3\lib\net45\System.Net.Http.Formatting.dll">&lt;MyDocuments&gt;\Visual Studio 2013\Projects\Getting Started with ASP.NET Web API (Tutorial Sample)\C#\packages\Microsoft.AspNet.WebApi.Client.5.2.3\lib\net45\System.Net.Http.Formatting.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.WebRequest.dll</Reference>
  <GACReference>System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a</GACReference>
  <GACReference>System.Runtime.Serialization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</GACReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Formatting</Namespace>
  <Namespace>System.Net.Http.Handlers</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Security.Cryptography.X509Certificates</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

// KIB Recent

using (var client = new HttpClient())
{
  //var uri = new Uri("https://kibserviceacceptatie.cloudapp.net/");
  //var uri = new Uri("https://kibserviceacceptatie.cloudapp.net");
  var uri = new Uri("https://localhost:44300/");
  
	client.BaseAddress = uri;
  
	var request = new  
  {
    Ticket = "qFxq2JcG89Jefd5AL9wTfZFBLElmv9dxCmYf44Et6soW1BD64y19ppaPWyOdXYjOhsmbi5T+cJaCYYBq5Vo5UEKlRx5/bfDU7hHsbiczbVvdj+l+TqtSt8XR6PdoaB7PA9kQYH3s+8gttIOLIj4oVMATGYngQqthCCkxebKCKVijGCZI2K9XPA5CN6ArfpAJ",    
    RekNr = "NL97BUNQ9900135121"    
//    RekNrBegunstigde = "NL20INGB0001234567",
//    NaamBegunstigde = "King6 Crediteur",
//    Oms = "Test via nieuwe KIB api",
//    Bedrag = 0.99m,
//    Valuta = "EUR"
  };
  
  var json = JsonConvert.SerializeObject(request).Dump();  
  		
  StringContent content = new StringContent(json);  
  content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
  
	HttpResponseMessage response = await client.PostAsync("api/validerentoestemming", content);	
	
	if (response.IsSuccessStatusCode)
	{
		string prod = await response.Content.ReadAsStringAsync();
    prod.Dump();
	}
	else
	{
		response.Dump("Fail");
	}
}

