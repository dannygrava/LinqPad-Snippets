<Query Kind="Statements">
  <Reference Relative="..\..\..\..\Documents\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\ICSharpCode.SharpZipLib.dll">&lt;MyDocuments&gt;\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\ICSharpCode.SharpZipLib.dll</Reference>
  <Reference Relative="..\..\..\..\Documents\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\Id3Lib.dll">&lt;MyDocuments&gt;\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\Id3Lib.dll</Reference>
  <Reference Relative="..\..\..\..\Documents\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\Mp3Lib.dll">&lt;MyDocuments&gt;\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\Mp3Lib.dll</Reference>
  <Namespace>Id3Lib</Namespace>
  <Namespace>Mp3Lib</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

string folder = @"\Podcast\LieveMarianne".FromDownloadFolder();;
List<string> output = File.ReadAllLines(Path.Combine(folder, "ExtraOutput.txt")).ToList();

Func <DateTime, string>  downloadLieveMarianne = (DateTime date) => 
	{
	string baseUrl = @"http://media.radio538.nl/download/radio538/fragmenten/";		
	//string baseUrl = @"http://media2.538.nl/audio/{0:yyMMdd}/";	
	//string baseUrl = @"http://cdn.media.538.nl/audio/{0:yyMMdd}/";	
	//string baseUrl = @"http://cdn.media.538.nl/video/{0:MMdd}/";	
	//http://cdn.media.538.nl/audio/150911/20150911-eso_heerschop.mp3	
	//http://cdn.media.538.nl/audio/160129/20160129-eso_peterheerschop.mp3
	string [] patterns = new string[] {	
		"{0:yyyyMMdd}-peterheerschop.mp3",	
		"{0:yyyyMMdd}-heerschop.mp3",	
		"{0:yyyyMMdd}-peter.mp3",
		"{0:yyyyMMdd}-peterh.mp3",
		"{0:yyyyMMdd}-lievemarianne.mp3",	
		"{0:yyyyMMdd}-peterlievemarianne.mp3",	
		"{0:yyyyMMdd}-peterviggo.mp3",
		"{0:yyyyMMdd}-viggo.mp3",		
		"{0:yyyyMMdd}-eso_peterheerschop.mp3",	
		"{0:yyyyMMdd}-eso_heerschop.mp3",	
		"{0:yyyyMMdd}-eso_peter.mp3",
		"{0:yyyyMMdd}-eso_peterh.mp3",
		"eso_heerschop.mp3",	
		"{0:yyyyMMdd}-eso_lievemarianne.mp3",	
		"{0:yyyyMMdd}-eso_peterviggo.mp3",
		"{0:yyyyMMdd}-eso_viggo.mp3",
		"{0:yyyyMMdd}-LIEVE_MARIANNE_-_EVERS_STAAT_OP",
		"{0:yyyyMMdd}-PETERHEERSCHOP-ESO",		
		}
		//.Select(p => Path.ChangeExtension (p, "mp3"))
		.ToArray()
		;		
		
	var urls = patterns
		.Concat(	
			patterns.Select (x => Path.ChangeExtension (x, Path.GetExtension(x).ToUpper())))
		.Concat(	
			patterns.Select (x => x.ToUpper()))
		.Concat(patterns.Select (x => x.Replace('-', '_')))
		.Concat(patterns.Select (x => x.ToUpper().Replace('-', '_')))
		.Concat(patterns.Select (x => x.Replace("-", "")))
		.Select (s => string.Format(baseUrl, date) + string.Format(s, date));
	
	var downloadLocation = folder;
	
	//urls.Dump(0);
	using (WebClient client = new WebClient ())
	{	
		foreach (string url in urls)
		{
			//url.Dump();
			try
			{
				//("Trying to download " + url + "...").Dump();
				if (new FileInfo(Path.Combine(downloadLocation, Path.GetFileName(url))).Exists)
				{
					return $"File ({url}) already downloaded!";
				}
				client.DownloadFile(url, Path.Combine(downloadLocation, Path.GetFileName(url)));
				output.Add(url);
				return string.Format ("Downloaded {0} to {1}", url, Path.Combine(downloadLocation, Path.GetFileName(url)));					
			}
			catch (WebException e)
			{
				//("Download failed with message: " + e.Message).Dump();		
			}	
		}
		
		//urls.Dump();
		return "All download attempts failed";
	}	
};

var mydate = new DateTime(2005, 3, 29);
while (mydate > new DateTime(2005, 1, 1))
{
	$"Verwerken dag {mydate}".Dump();
	downloadLieveMarianne(mydate).Dump();	
	mydate = mydate.AddDays(-1);	
}
output.Dump();
File.WriteAllLines(Path.Combine(folder, "ExtraOutput.txt"), output);
return;

var vandaag = DateTime.Today.AddDays(0);
var eersteVrijdagInVerleden = Enumerable.Range(0, 7).Select (x => vandaag.AddDays(-x)).First(d=> d.DayOfWeek == DayOfWeek.Friday);

//downloadLieveMarianne(vandaag.AddDays(-2)).Dump("Eergisteren");
//downloadLieveMarianne(vandaag.AddDays(-1)).Dump("Gisteren");
//downloadLieveMarianne(vandaag).Dump("Vandaag");
//downloadLieveMarianne(eersteVrijdagInVerleden).Dump("Afgelopen vrijdag");
//Enumerable.Range(0, 4)
//	.Select (x => downloadLieveMarianne(eersteVrijdagInVerleden.AddDays(-x*7)))
//	.Dump();
	
var dates = Enumerable.Range(0, 300)
	.Select (x => eersteVrijdagInVerleden.AddDays(-x))	
	;

	foreach(var date in dates.Where(d => d > new DateTime(2015, 12, 1)))
	{
		string.Format("Processing date: {0:dd-MM-yyyy}", date).Dump();
		downloadLieveMarianne(date).Dump();	
	}

// TODO DG: herschrijven dat gelijk na de download de tag wordt aangepast.
// Het SearchPattern werkt vanaf 2020 ook niet meer.

// Nu mp3 tags verwerken
const string searchPattern = @"201*.mp3";

foreach (var filename in Directory.EnumerateFiles(folder, searchPattern).OrderBy(s => s))
{
	var mp3File = new Mp3File(filename);
	var tagHandler = mp3File.TagHandler;
	
	tagHandler.Album = "Lieve Marianne";
	DateTime dateInTitle = DateTime.ParseExact(Path.GetFileNameWithoutExtension(filename).Substring(0, 8), "yyyyMMdd", null);
	tagHandler.Title = string.Format("Lieve Marianne, {0:D}", dateInTitle); 
	tagHandler.Artist = "Peter Heerschop";
	// note Google Play voor android kijkt alleen naar de eerste drie karakters van tracknummer, daarom pakken we de dag van het jaar. 
	// Dit geeft wel een verkeerde volgorde rond de jaarwisseling, maar daar is mee te leven.
	tagHandler.Track  = dateInTitle.DayOfYear.ToString(); 
	mp3File.Update();
}

var mp3s = Directory
	.EnumerateFiles(folder, searchPattern)
	.OrderBy(s => s)
	//.Select (s => new TagHandler(new Mp3File(s).TagModel))
	.Select (s => (new Mp3File(s)).TagHandler)
	.Select (th => new {Track=th.Track, Title=th.Title, Artist=th.Artist, Album=th.Album})
	.Dump();