<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

//https://ait.newsinslowitalian.com/2015/nov1815/ait1-2.mp3
string DOWNLOAD_FOLDER = @"Podcast\SlowItalian\".FromDownloadFolder();
DateTime ep00Date = new DateTime(2015, 11, 18).AddDays(-7); 
Func<DateTime, int> getEpisodeForDate = (date) => ((date.AddDays(-1) - ep00Date).Days / 7) + 1; 
Func<int, DateTime> getDateForEpisode = (episode) => (ep00Date.AddDays(7 * episode));

var downloadedUris = new List<string>();
var skippedUris = new List<string>();

// Datum van welke te downloaden
DateTime vanDatum = new DateTime(2017, 10, 1);

var dc = new DumpContainer().Dump("Bezig met");

using (WebClient client = new WebClient ())
{
  int episode = getEpisodeForDate(vanDatum);
  DateTime date = getDateForEpisode(episode);
  while (date <= DateTime.Today)
  {
    const int MAX_PARTS = 10;
		for (int part = 1; part <= MAX_PARTS; part++)
		{			
			string podcastUri = $"https://ait.newsinslowitalian.com/{date:yyyy}/{date.ToString("MMMddyy", CultureInfo.InvariantCulture).ToLower()}/ait{episode}-{part}.mp3";
			if (!new FileInfo(Path.Combine(DOWNLOAD_FOLDER, Path.GetFileName(podcastUri))).Exists)
		    {
				try
				{
					dc.Content = $"Downloading {podcastUri}";					
					client.DownloadFile(podcastUri, Path.Combine(DOWNLOAD_FOLDER, Path.GetFileName(podcastUri)));				
					downloadedUris.Add(podcastUri);				
				}
				catch (Exception e)
				{
					e.Dump($"Exception for {podcastUri}");
					break;
				}
			}
			else
			{
				skippedUris.Add(podcastUri);						
			}				
		}
    episode++;
    date = getDateForEpisode(episode);          
	}		
}
dc.Content = "done!";
downloadedUris.Dump("Downloaded", 0);
skippedUris.Dump("Skipped", 0);