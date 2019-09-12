<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

const string PATTERN_HREF = @"href=""(?'url'[^""]+?\.mp3)""";
string DOWNLOAD_FOLDER = @"\Abbey Road".FromDownloadFolder();
//Uri baseUri = new Uri("http://blogs.hatrix.fr/musiques/Pink%20Floyd/Pink%20Floyd%20-%20A%20Saucerful%20Of%20Secrets%20(from%2024-96%20Vinyl)%20(v0)/");
Uri baseUri = new Uri("http://195.122.253.112/public/mp3/Beatles/12%20Abbey%20Road/?C=M;O=A");


using (WebClient client = new WebClient ())
{	
	var content = client.DownloadString(new Uri (baseUri.ToString()));
	var matches = Regex.Matches (content, PATTERN_HREF)
		.Cast<Match>()
		.Select (m => m.Groups ["url"].Value)
		.Dump("Alle hrefs op pagina", 0)		
		.ToList();
		
	 matches.Dump("Kandidaat links", 0);		
	
	//http://hansdorrestijn.nl/cds/cirkels/1-09AlsIkEenDameZieLopen.mp3	
	foreach (var m in matches)
	{	
		var fileUri = new Uri(baseUri, m);
		//fileUri.Dump();
		var filename = Path.Combine(DOWNLOAD_FOLDER, WebUtility.UrlDecode(fileUri.Segments.Last()));		
		filename.Dump();
		if (!File.Exists(filename))
		{
			//content = client.DownloadString(new Uri(mainUri, m));
			"Downloading file!".Dump();
			client.DownloadFile(fileUri, Path.Combine(DOWNLOAD_FOLDER, filename));
			"File downloaded!".Dump();
		}
	}
}