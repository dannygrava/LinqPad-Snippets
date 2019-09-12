<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

//((int)'9').Dump();
StringBuilder sb = new StringBuilder();
for (char c = '0'; c <= 'z'; c++)
{
	if (Char.IsDigit(c) || (char.IsLetter(c) && char.IsLower(c)))
 		sb.Append(c);	
}
var values = sb.ToString();

Random r = new Random();
//r.Next().Dump();
var allValues = (
	from a in values
	from b in values
	from c in values
	select ""+a+b+c
	)
	.OrderBy(x => r.Next())
	.OrderBy(s => !s.Any(c => Char.IsLetter(c)))
	.OrderBy(s => !s.Contains("5"))
	.ToList();
//allValues.Count().Dump();
//allValues.Dump();

string DOWNLOAD_FOLDER = @"Podcast\MaxmondoPremium\".FromDownloadFolder();

var dc = new DumpContainer ("Processing").Dump();
//Double step = 100d / (double) allValues.Count;
//Double percent = 0;

int step = 0;

using (WebClient client = new WebClient ())
{
	foreach (string value in allValues)
	{
		step++;		
		dc.Content = $"Stap {step} van {allValues.Count} ({step * 100d / allValues.Count:F2}%)";		
		string podcastUri = $"http://traffic.libsyn.com/maxmondo1it/mm-ii-prem-027-{value}.mp3";
		try
		{
			//podcastUri.Dump("URI", 0);
			//Thread.Sleep(5);
			client.DownloadFile(podcastUri, Path.Combine(DOWNLOAD_FOLDER, Path.GetFileName(podcastUri)));				
			$"Gelukt ({podcastUri})".Dump();
			break;
		}
		catch (Exception e)
		{
			//e.Dump("Exception for " + podcastUri);
			//skippedUris.Add(podcastUri);
		}
	}	
}

"Done!".Dump();