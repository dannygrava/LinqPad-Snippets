<Query Kind="Statements">
  <Reference>&lt;MyDocuments&gt;\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\ICSharpCode.SharpZipLib.dll</Reference>
  <Reference>&lt;MyDocuments&gt;\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\Id3Lib.dll</Reference>
  <Reference>&lt;MyDocuments&gt;\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\Mp3Lib.dll</Reference>
  <Namespace>Id3Lib</Namespace>
  <Namespace>Mp3Lib</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

const string folder = @"D:\Users\dg\Downloads\Podcast\LieveMarianne";

// TODO DG: herschrijven dat gelijk na de download de tag wordt aangepast.
// Het SearchPattern werkt vanaf 2020 ook niet meer.

// Nu mp3 tags verwerken
const string searchPattern = @"20*.mp3";

int track = 1;
foreach (var filename in Directory.EnumerateFiles(folder, searchPattern).OrderBy(s => s))
{
	var mp3File = new Mp3File(filename);
	
	var tagHandler = mp3File.TagHandler;
			
	tagHandler.Album = "Lieve Marianne";
	DateTime dateInTitle = DateTime.ParseExact(Path.GetFileNameWithoutExtension(filename).Substring(0, 8), "yyyyMMdd", null);
	tagHandler.Title = string.Format("{0:D}", dateInTitle); 
	tagHandler.Artist = "Peter Heerschop";
	// note Google Play voor android kijkt alleen naar de eerste drie karakters van tracknummer, daarom pakken we de dag van het jaar. 
	// Dit geeft wel een verkeerde volgorde rond de jaarwisseling, maar daar is mee te leven.
	
	tagHandler.Track  = (track++).ToString(); 
	try
	{
		mp3File.Update();
	}
	catch (Exception e)
	{
		e.Dump("Exceptie!");
	}	
}


var mp3s = Directory
	.EnumerateFiles(folder, searchPattern)
	.OrderBy(s => s)
	//.Select (s => new TagHandler(new Mp3File(s).TagModel))
	.Select (s => (new Mp3File(s)).TagHandler)
	.Select (th => new {Track=th.Track, Title=th.Title, Artist=th.Artist, Album=th.Album})
	.Dump();