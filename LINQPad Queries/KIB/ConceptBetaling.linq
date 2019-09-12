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
  var uri = new Uri("https://kibserviceacceptatie.cloudapp.net/");
  //var uri = new Uri("https://localhost:44300/");
  
	client.BaseAddress = uri;
		
	var request = new  
  {
    Ticket = "rkQiVYghKyKMtqw2fctDxuzaF50IsNHwkYvRuFcYx0CbZy1prSlf8KgzlS/mbnx3TPrbTICr0BP00UbM5/95we+mnS9xhwPlMwcaUrC5EPdwNOwk5bE45PwUGfY7F8PEZMnG2hHEzzmSy2xo390oIuo9O7KQ7PHSgF4Ncx42na/ie6xyTuZQptlJ6d3ctfsM",    
    RekNr = "NL97BUNQ9900135121",
    BankomgevingId = "16",
    RekNrBegunstigde = "NL02ABNA0123456789",
    NaamBegunstigde = "1",
    Oms = "",
    Bedrag = 12.00m,
    Valuta = "EUR"
  };
  
  JsonConvert.SerializeObject(request).Dump();  
  StringContent content = new StringContent(JsonConvert.SerializeObject(request));  
  content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
  
	HttpResponseMessage response = await client.PostAsync("api/conceptbetaling", content);	
	
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