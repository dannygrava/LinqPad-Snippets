<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

const string SITE_URL = @"http://mutec-nas.univ-lille3.fr/MUSIQUE/Musique/Paolo%20Conte/Paolo%20Conte%20-%20The%20Collection%20%5BDisc%201%5D/";
const string DOWNLOAD_FOLDER = @"D:\Users\dg\Music\Misc\Paolo Conte\The Collection";
const string PATTERN_HREF = @"href=""(?'url'[^""]+?)""";

using (WebClient client = new WebClient ())
{
	var content = client.DownloadString(SITE_URL);
	var links = Regex.Matches (content, PATTERN_HREF)
		.Cast<Match>()
		.Select (m => m.Groups ["url"].Value)
		.Dump(0)
		.Where(s => s.EndsWith(".m4a") || s.EndsWith(".mp3"))
		.ToList();
	
	links.Dump("Alle relevante links op de pagina", 0);
	
	if (!Directory.Exists(DOWNLOAD_FOLDER)) 
    {
		// Try to create the directory.
		Directory.CreateDirectory(DOWNLOAD_FOLDER);
    }                    
	
	// Download nu de links
	foreach (var link in links)
	{
		Uri url = new Uri (link, UriKind.RelativeOrAbsolute);
		if (!url .IsAbsoluteUri)
		{
			url = new Uri(new Uri(SITE_URL), link);
		}		
		
		string filename = Path.GetFileName(Uri.UnescapeDataString(url.ToString()));
		bool alreadyDownloaded = new FileInfo(Path.Combine(DOWNLOAD_FOLDER, Path.GetFileName(filename))).Exists;		
		
		if (!alreadyDownloaded)
		{
			$"Downloading file '{filename}' from '{url}' to '{DOWNLOAD_FOLDER}'".Dump();			
			client.DownloadFile(url, Path.Combine(DOWNLOAD_FOLDER, Path.GetFileName(filename)));
			"File downloaded!".Dump();
		}
	}		
}