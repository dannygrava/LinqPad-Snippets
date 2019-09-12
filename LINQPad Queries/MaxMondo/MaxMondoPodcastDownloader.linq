<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

//http://traffic.libsyn.com/maxmondo1it/mm-ii-pod-270.mp3

string DOWNLOAD_FOLDER = @"Podcast\Maxmondo\".FromDownloadFolder();

var downloadedUris = new List<string>();
var skippedUris = new List<string>();

var dc = new DumpContainer().Dump("Bezig met");

using (WebClient client = new WebClient ())
{
	for (int i = 400; i>200; i--)
	{
		string podcastUri = $"http://traffic.libsyn.com/maxmondo1it/mm-ii-pod-{i:D3}.mp3";
		//http://traffic.libsyn.com/maxmondo1it/mm-ii-prem-quaderni-001-a56e.mp3
		//http://traffic.libsyn.com/maxmondo1it/mm-ii-prem-022-5sr.mp3
		if (!new FileInfo(Path.Combine(DOWNLOAD_FOLDER, Path.GetFileName(podcastUri))).Exists)
		//if (true)
		{
			try
			{
				dc.Content = $"Dowloading {podcastUri}";
				//Task.Delay (100).Wait();
				client.DownloadFile(podcastUri, Path.Combine(DOWNLOAD_FOLDER, Path.GetFileName(podcastUri)));				
				downloadedUris.Add(podcastUri);				
			}
			catch (Exception e)
			{
				//e.Dump("Exception for " + podcastUri);
				skippedUris.Add(podcastUri);
			}
		}
		else
		{
			skippedUris.Add(podcastUri);			
		}				
	}	
}
dc.Content = "done!";
downloadedUris.Dump("Downloaded", 0);
skippedUris.Dump("Skipped", 0);

//http://traffic.libsyn.com/maxmondo1it/mm-ii-pod-272.mp3