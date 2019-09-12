<Query Kind="Program">
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	new Hyperlinq ("http://www.mediawiki.org/wiki/API:Login", "Documentatie: Wikimedia api voor login").Dump();
	new Hyperlinq ("https://www.nczonline.net/blog/2009/05/05/http-cookies-explained/", "HTTP cookies explained").Dump();
	//******************************************************************************
	// NOTE Gebruikersnaam en wachtwoord om op de wiki in te loggen hier aanpassen!* 
	//******************************************************************************
	var username = Util.ReadLine("Gebruikersnaam Wiki");
	var password = Util.ReadLine("Wachtwoord");	
	//******************************************************************************
		
	var inlogUrl = string.Format("http://intranet.quadrant.local/wiki/api.php?action=login&lgname={0}&lgpassword={1}&format=xml", username, password);
	var janUrl = "http://intranet.quadrant.local/wiki/index.php?title=Gebruiker:Jan";
	
	var cookies = new CookieContainer();	
	
	// Stap 1: Inloggen op de wiki
	var response = SendRequest(inlogUrl, cookies);	
	// api.php geeft een dergelijke response terug:
	//<?xml version="1.0"?><api><login result="NeedToken" token="a4f9807aa2647f159f2ac083ae3a88a6" cookieprefix="wikidb" sessionid="2b3upir7qfehqcrv11ru77r1t1" /></api>
	var result = XDocument.Parse(response).Element("api").Element("login").Attribute("result").Value;
	
	// Stap 2: Nogmaals inloggen, maar nu met securitytoken dat bij de eerste call is teruggegeven
	if (result == "NeedToken")
	{
		var token = XDocument.Parse(response).Element("api").Element("login").Attribute("token").Value;		
		response = SendRequest(string.Format("{0}{1}{2}", inlogUrl, "&lgtoken=", token), cookies);
		
	}
	
	result = XDocument.Parse(response).Element("api").Element("login").Attribute("result").Value;
	
	if (result != "Success")
	{
		string.Format("Login failed; following result was returned: {0}", result).Dump("ERROR");
		return;
	}
	
	// Stap 3: De gewenste pagina daadwerkelijk openen een aantal maal
	var progress = new Util.ProgressBar ("Ophalen pagina's").Dump();
	bool firsttime = true;
	const int numRequests = 1; // 120;
	for (int i = 0; i < numRequests; i++)
	{
		response = SendRequest(janUrl, cookies);
		if (firsttime)
			response.OnDemand("Response").Dump("Wiki pagina Html");		
		firsttime = false;
		progress.Percent = 100 * (i+1)/ numRequests;
	}	
}


string SendRequest(string url, CookieContainer cookieContainer)
{
	// NOTE HttpWebRequest lijkt bedoeld voor eenmalig gebruik, instances zijn niet herbruikbaar, vandaar deze functie	
	var request = (HttpWebRequest)HttpWebRequest.Create(url);
	
	request.ContentLength = 0;
	request.Method = "POST";
	request.CookieContainer = cookieContainer;	
	HttpWebResponse response = (HttpWebResponse) request.GetResponse();
	request.Headers["Cookie"].Dump("Cookies endup in de Request Header under Cookie");
	response.Headers["Set-Cookie"].Dump("Cookies endup in de Response Header under Set-Cookie");
	
	if (response.Cookies.Count > 0)
		response.Cookies.Dump("Cookie container");
		
	Stream receiveStream = response.GetResponseStream();	
	
	string output;
	
	// Pipes the stream to a higher level stream reader with the required encoding format. 
	using (StreamReader readStream = new StreamReader( receiveStream, Encoding.UTF8))
	{
		output = readStream.ReadToEnd();
		// Releases the resources of the Stream.
		readStream.Close();
	}
	response.Close();		
	return output;
}