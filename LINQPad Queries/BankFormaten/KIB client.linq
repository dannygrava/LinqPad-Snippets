<Query Kind="Program">
  <Reference Relative="..\..\..\..\Documents\Visual Studio 2013\Projects\Getting Started with ASP.NET Web API (Tutorial Sample)\C#\packages\Newtonsoft.Json.6.0.4\lib\net45\Newtonsoft.Json.dll">&lt;MyDocuments&gt;\Visual Studio 2013\Projects\Getting Started with ASP.NET Web API (Tutorial Sample)\C#\packages\Newtonsoft.Json.6.0.4\lib\net45\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Reference Relative="..\..\..\..\Documents\Visual Studio 2013\Projects\Getting Started with ASP.NET Web API (Tutorial Sample)\C#\packages\Microsoft.AspNet.WebApi.Client.5.2.3\lib\net45\System.Net.Http.Formatting.dll">&lt;MyDocuments&gt;\Visual Studio 2013\Projects\Getting Started with ASP.NET Web API (Tutorial Sample)\C#\packages\Microsoft.AspNet.WebApi.Client.5.2.3\lib\net45\System.Net.Http.Formatting.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.WebRequest.dll</Reference>
  <GACReference>System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a</GACReference>
  <GACReference>System.Runtime.Serialization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</GACReference>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Formatting</Namespace>
  <Namespace>System.Net.Http.Handlers</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Security.Cryptography.X509Certificates</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
  bool useJson = false; // Als False dan gebruiken we XML
  
  // Setup client certificate
  var handler = new WebRequestLogginHandler();
  using (var fs = new FileStream(@"D:\KingInternetBankieren\KIB-Client-Develop (pw=client).pfx", FileMode.Open))
  {
  	byte[] certBytes = new byte[fs.Length];
  	fs.Read(certBytes, 0, (int) fs.Length);
  	X509Certificate clientCertificate = new X509Certificate2(certBytes, "client");
  	handler.ClientCertificates.Clear();
      handler.ClientCertificates.Add(clientCertificate);		
  }
  
  using (var client = new HttpClient(handler))
  {
  
  	client.Timeout.Dump("TimeOut");
  	//client.BaseAddress = new Uri("https://kibserviceacceptatie.cloudapp.net/");
    client.BaseAddress = new Uri("https://internetbankieren.quadrant.nl:443/");  
  	client.DefaultRequestHeaders.Accept.Clear();	
  	if (useJson)	
  		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
  	else
  		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));	
  		
  	HttpResponseMessage dummy = await client.GetAsync("api/rekeningafschriften");		
  		
  	var data = File.ReadAllBytes(@"c:\users\dg\documents\king\camt\UitHetVeld\Camt053.xml");
  	
  	var content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString());     
  	StreamContent streamContent = new StreamContent(new MemoryStream(data));
  	content.Add(streamContent, "File", "Camt053.xml");        
  	// New code:
  	HttpResponseMessage response = await client.PostAsync("api/rekeningafschriften", content);	
  	
  	if (response.IsSuccessStatusCode)
  	{
  		string prod = await response.Content.ReadAsStringAsync();
  		prod.Length.Dump("Lengte van response");
  		if (useJson)
  			prod.Dump("Ravk als Json");
  		else
  		{
  			XDocument ravk = XDocument.Load(new StringReader(prod));
  			ravk.Dump("Ravk als Xml");
  		}
  	}
  	else
  	{
  		response.Dump("Fail");
  	}
  }
}

// Define other methods and classes here
// Handler implementatie die requests en response logt
// http://stackoverflow.com/questions/18924996/logging-request-response-messages-when-using-httpclient
public class WebRequestLogginHandler : WebRequestHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {

        //using (StreamWriter sw = new StreamWriter(new FileStream("_Requests.log", FileMode.Append)))
        {
            Console.WriteLine("Request:");
            Console.WriteLine(request.ToString());
            if (request.Content != null)
            {
                Console.WriteLine(await request.Content.ReadAsStringAsync());
            }
            Console.WriteLine();

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            Console.WriteLine("Response:");
            Console.WriteLine(response.ToString());
            if (response.Content != null)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            Console.WriteLine();

            return response;
        }
    }
}


