<Query Kind="Program">
  <Reference Relative="..\..\..\..\Documents\Visual Studio 2013\Projects\Getting Started with ASP.NET Web API (Tutorial Sample)\C#\packages\Newtonsoft.Json.6.0.4\lib\net45\Newtonsoft.Json.dll">&lt;MyDocuments&gt;\Visual Studio 2013\Projects\Getting Started with ASP.NET Web API (Tutorial Sample)\C#\packages\Newtonsoft.Json.6.0.4\lib\net45\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Reference Relative="..\..\..\..\Documents\Visual Studio 2013\Projects\Getting Started with ASP.NET Web API (Tutorial Sample)\C#\packages\Microsoft.AspNet.WebApi.Client.5.2.3\lib\net45\System.Net.Http.Formatting.dll">&lt;MyDocuments&gt;\Visual Studio 2013\Projects\Getting Started with ASP.NET Web API (Tutorial Sample)\C#\packages\Microsoft.AspNet.WebApi.Client.5.2.3\lib\net45\System.Net.Http.Formatting.dll</Reference>
  <GACReference>System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a</GACReference>
  <GACReference>System.Runtime.Serialization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</GACReference>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Formatting</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static bool useJson = true; // Als False dan gebruiken we XML

// Major fuckup: als er iets fout gaat met het deserializen van Xml dan wordt er een exceptie geraised die lijkt te suggereren dat de
// XmlMediaTypeConverter niet geregistreerd is.

void Main()
{	
	// Note: Run de server ProductsApp first	
    RunAsync().Wait();
}

static async Task RunAsync()
{
  // Methode 1: Via HttpClient en al zijn extra's
//  using (var client = new HttpClient())
//  {
//  	
//	// New code:
//	client.BaseAddress = new Uri("http://dannysproeftuin.cloudapp.net:80/");
//	client.DefaultRequestHeaders.Accept.Clear();	
//	if (useJson)	
//		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//	else
//		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));	
//		
//		
//	var data = new CamtData();
//	data.CamtXml = "<KING_KOSTENDRAGERS><KOSTENDRAGERS><KOSTENDRAGER><KD_NUMMER>202</KD_NUMMER><KD_ZOEKCODE>ZC202</KD_ZOEKCODE><KD_OMSCHRIJVING>Kostendrager 202</KD_OMSCHRIJVING></KOSTENDRAGER><KOSTENDRAGER><KD_NUMMER>203</KD_NUMMER><KD_ZOEKCODE>ZC203</KD_ZOEKCODE><KD_OMSCHRIJVING>Kostendrager 203</KD_OMSCHRIJVING><KD_AANMAAKDATUM>2015-05-07T00:00:00+02:00</KD_AANMAAKDATUM></KOSTENDRAGER></KOSTENDRAGERS></KING_KOSTENDRAGERS>";
//	data.Param = "Test";	
//
//	// New code:
//	HttpResponseMessage response = await client.PostAsync("api/KostendragerConverter", data, new JsonMediaTypeFormatter());
//	//new XmlMediaTypeFormatter()
//	
//	if (response.IsSuccessStatusCode)
//	{
//		string prod = await response.Content.ReadAsStringAsync();
//		prod.Dump("Product string");
//		IEnumerable<Kostendrager> kstd = await response.Content.ReadAsAsync<IEnumerable<Kostendrager>>();
//		kstd.Dump("Kostendragers");
//		// Expliciete serialisatie van de string
//		//Product product = Deserialize<Product>(new XmlMediaTypeFormatter(), prod);				
//	}
//  }
  
	// Methode 2: via WebClient
	using (WebClient wc = new WebClient())
	{
		wc.Proxy = null;
		if (useJson)
			wc.Headers.Add("Accept","application/json");
		else
			wc.Headers.Add("Accept","application/xml");
			
		var data = new System.Collections.Specialized.NameValueCollection();
		data.Add("CamtXml", "<KING_KOSTENDRAGERS><KOSTENDRAGERS><KOSTENDRAGER><KD_NUMMER>202</KD_NUMMER><KD_ZOEKCODE>ZC202</KD_ZOEKCODE><KD_OMSCHRIJVING>Kostendrager 202</KD_OMSCHRIJVING></KOSTENDRAGER><KOSTENDRAGER><KD_NUMMER>203</KD_NUMMER><KD_ZOEKCODE>ZC203</KD_ZOEKCODE><KD_OMSCHRIJVING>Kostendrager 203</KD_OMSCHRIJVING><KD_AANMAAKDATUM>2015-05-07T00:00:00+02:00</KD_AANMAAKDATUM></KOSTENDRAGER></KOSTENDRAGERS></KING_KOSTENDRAGERS>");
		data.Add("Param", "Test");
		
		byte[] result = wc.UploadValues("http://dannysproeftuin.cloudapp.net:80/api/KostendragerConverter", "POST", data);
		Encoding.UTF8.GetString(result).Dump("Via WebClient");
	}
}

// Define other methods and classes here

//class Product
//{
//   public string Name { get; set; }
//   public double Price { get; set; }
//   public string Category { get; set; }
//}

[DataContract(Name = "Artikel", Namespace = "urn:iso:std:iso:20022:tech:xsd:camt.053.001.02")]
public class Product
{
   [DataMember(Name = "Gid")]
   public int Id { get; set; }
   [DataMember]
   public string Name { get; set; }
   [DataMember]
   public string Category { get; set; }
   [DataMember]
   public decimal Price { get; set; }
}

public class CamtData
{
	public string CamtXml { get; set; }
    public string Param { get; set; }
}

static T Deserialize<T> (MediaTypeFormatter formatter, string str) where T : class
{
  // Write the serialized string to a memory stream.
  Stream stream = new MemoryStream();
  StreamWriter writer = new StreamWriter(stream);
  writer.Write(str);
  writer.Flush();
  stream.Position = 0;
  // Deserialize to an object of type T
  return formatter.ReadFromStreamAsync(typeof(T), stream, null, null).Result as T;
}

public class Kostendrager
{
   public int Nummer { get; set; }
   public string Zoekcode { get; set; }
   public string Omschrijving { get; set; }
   public DateTime AanmaakDatum { get; set; }
}