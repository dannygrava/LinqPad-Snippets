<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.WebRequest.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Security.Cryptography.X509Certificates</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async void Main()
{
	var handler = new WebRequestLogginHandler();
	// Add Client cert
	X509Certificate clientCertificate;
	X509Store store = null;
	store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
	store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);

	clientCertificate = store.Certificates.Find(X509FindType.FindByThumbprint, "601D244A1C15A5C5DF53504E754DCB011F515E75", false)
        .OfType<X509Certificate2>()
        .FirstOrDefault();

	clientCertificate.Dump("KIB-Client-Develop ontwikkelcertificaat");         
    handler.ClientCertificates.Clear();
	if (clientCertificate != null)
    	handler.ClientCertificates.Add(clientCertificate);

	using (var client = new HttpClient(handler))
	{
		HttpResponseMessage response = await client.GetAsync("https://www.microsoft.com");
		var responseTest = await response.Content.ReadAsStringAsync();
		responseTest.Dump();
	}
	// Etc/.
}

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