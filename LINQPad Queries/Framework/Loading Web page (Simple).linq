<Query Kind="Statements">
  <Namespace>System.Net</Namespace>
</Query>

string url = @"https://www.mnot.net/blog/2014/05/09/if_you_can_read_this_youre_sniing"; //SNI requiring site

WebClient client = new WebClient ();
// Add a user agent header in case the 
// requested URI contains a query.
//client.Headers.Add ("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

using (StreamReader reader = new StreamReader (client.OpenRead (url)))
{        
	string content = reader.ReadToEnd ();
	//Util.DisplayWebPage("This will be displayed in a web page");
	//Util.DisplayWebPage(content);
	//Util.DisplayWebPage(client.OpenRead (url));
	Console.WriteLine (content);
	// process content here
}

 
 
//// Je kunt ook een instantie van een WebClient class gebruiken
//WebRequest request = WebRequest.Create (url);
// 
//// For HTTP, cast the request to HttpWebRequest
//// allowing setting more properties, e.g. User-Agent.
//// An HTTP response can be cast to HttpWebResponse. 
//using (WebResponse response = request.GetResponse())
//{
//   // Ensure that the correct encoding is used. 
//   // Check the response for the Web server encoding.
//   // For binary content, use a stream directly rather
//   // than wrapping it with StreamReader. 
//   using (StreamReader reader = new StreamReader (response.GetResponseStream(), Encoding.UTF8))
//   {
//	   string content = reader.ReadToEnd();
//	   content.Dump("Inhoud van " + url);
//	   // process the content
//   }
//}