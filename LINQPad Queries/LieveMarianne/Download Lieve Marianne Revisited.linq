<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

//const string START_URI = "http://www.538.nl/gemist/filter/tag/lieve+marianne";
//const string START_URI = "http://www.538.nl/gemist/filter/pagina/70/type/video,audiofragmenten,uitzending-gemist/sortering/date/zoek/lieve%20marianne#gemist";
const string PATTERN_HREF = @"href=""(?'url'[^""]+?)""";
const string PATTERN_PODCAST_REF = @"<meta property=""og:(video|audio)"" content=""(?'url'[^""]+?)"".*/>";
string DOWNLOAD_FOLDER = @"\Podcast\LieveMarianne".FromDownloadFolder();
string LOG_FILENAME = DOWNLOAD_FOLDER + @"\LieveMarianne.xml";
const bool SKIP_DOWNLOAD = false;

var rss = XDocument.Load(LOG_FILENAME);
XElement channel = rss.Root.Element("channel");

using (WebClient client = new WebClient ())
{
//	foreach (int i in Enumerable.Range(1, 20).Reverse())
//	{
		//var content = client.DownloadString($"http://www.538.nl/gemist/filter/pagina/{i}/type/video,audiofragmenten,uitzending-gemist/sortering/date/zoek/lieve%20marianne#gemist");
		string url = "http://www.538.nl/gemist/filter/tag/lieve+marianne";
//		string url = "https://www.538.nl/programma-item/lieve-marianne";
		var content = client.DownloadString(url);
		
		var matches = Regex.Matches (content, PATTERN_HREF)
			.Cast<Match>()
			.Select (m => m.Groups ["url"].Value)
			.Dump("Alle hrefs op pagina", 0)
			.Where(m => m.Contains("peter"))
			.ToList();
			
		matches.Dump("Kandidaat links", 0);	
	
		Uri mainUri = new Uri("http://www.538.nl");
		// Download nu de gevonden pagina's
		foreach (var m in matches)
		{
			content = client.DownloadString(new Uri(mainUri, m));
			// Zoek de download link op
			var match = Regex.Match (content, PATTERN_PODCAST_REF);	
			var podcastUri = match.Groups["url"].Value;	
			podcastUri.Dump();

			// Controleren of niet reeds gedownload
			bool alreadyAddedToRss = channel.Elements("item").Any(x => x.Element("link").Value == podcastUri);
			bool mustSkipDownload = 
				SKIP_DOWNLOAD
				|| new FileInfo(Path.Combine(DOWNLOAD_FOLDER, Path.GetFileName(podcastUri))).Exists
				|| channel.Elements("item").Any(x => x.Element("link").Value == podcastUri);
//          Alleen files download die nog niet in de rss staan, zeker als het mp4 betreft!
//          Maar het zou uit te schakelen moeten zijn in mijn optiek
			if (mustSkipDownload)
			{
				"File skipped, was already downloaded!".Dump();
			}
			
			try
			{
				// Download het bestand				
				if (!mustSkipDownload)
				{
					"Downloading file!".Dump();
					client.DownloadFile(podcastUri, Path.Combine(DOWNLOAD_FOLDER, Path.GetFileName(podcastUri)));
					"File downloaded!".Dump();
				}
				
				if (!alreadyAddedToRss)
				{
					DateTime dateInTitle;
					string title = "Lieve Marianne";
					if (DateTime.TryParseExact(Path.GetFileNameWithoutExtension(Path.GetFileName(podcastUri)).Substring(0, 8), "yyyyMMdd", null, DateTimeStyles.None, out dateInTitle))
						title = $"{title}, {dateInTitle:D}";				 	
					XElement item  = new XElement("item");
					item.Add(new XElement("title", title));
					item.Add(new XElement("link", podcastUri));
					item.Add(new XElement("description"));
					channel.Add(item);
				}
			}
			catch (Exception e)
			{
				e.Dump("Exception for " + podcastUri);
			}

		}
//	} // for
	if (!SKIP_DOWNLOAD)
		rss.Save(LOG_FILENAME);
	else
		rss.Dump();
}