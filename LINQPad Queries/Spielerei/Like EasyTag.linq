<Query Kind="Statements">
  <Reference>&lt;MyDocuments&gt;\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\ICSharpCode.SharpZipLib.dll</Reference>
  <Reference>&lt;MyDocuments&gt;\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\Id3Lib.dll</Reference>
  <Reference>&lt;MyDocuments&gt;\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\Mp3Lib.dll</Reference>
  <Namespace>Mp3Lib</Namespace>
</Query>

//string mp3Path = @"D:\Users\dg\Music\Guitarra clasica\Victor Villadangos\5 Preludios";
//string mp3Path = @"D:\Users\dg\Music\Guitarra clasica\Popular\Kazuhito Yamashita plays The Beatles";
string mp3Path = @"D:\Users\dg\Music\Popular\The Doors\L.A. Woman";
const string patternTitle = @"(?'title'.+)\.mp3";
const string patternTrackComposerTitle = @"(?'track'\d\d?)[\s\.\-]*(?'composer'[^\-]+)\-(?'title'.+)\.mp3";
const string patternTrackTitleComposer = @"(?'track'\d\d?)[\s\.\-]*(?'title'[^\-\(]+)[\-\(](?'composer'.+)[\-\)](?'ignore'.+)?\.mp3";
const string patternTrackTitle =  @"(?'track'\d\d?)[\s\.\-]*(?'title'.+)\.mp3";

string pattern = patternTrackTitle;
bool updateTags = false;
bool updateFiles = false;

DirectoryInfo dir = new DirectoryInfo (mp3Path);

var mp3s = dir.GetFiles("*.mp3");
//mp3s.Dump();

//Func<string, string> toTitleCase = s => s.Trim();
Func<string, string> toTitleCase = s => System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower().Trim());

var  toBeProcessed = mp3s	
	.Select(fi => new {input=fi.FullName, match=Regex.Match(fi.Name, pattern, RegexOptions.IgnoreCase)})
	.Select(m => new {
		Input = m.input,		
		Track = toTitleCase(m.match.Groups["track"].Value), 
		Composer = toTitleCase(m.match.Groups["composer"].Value), 
		Title = toTitleCase(m.match.Groups["title"].Value),
		Album = mp3Path.Split(Path.DirectorySeparatorChar).Reverse().ElementAt(0),
		//Album = "Musica Argentina en guitarra Vol.1",
		Artist= mp3Path.Split(Path.DirectorySeparatorChar).Reverse().ElementAt(1),
		SuggestedFilename = Path.Combine(mp3Path, m.match.Groups["composer"].Value == "" ? string.Format("{0} {1}.mp3", toTitleCase(m.match.Groups["track"].Value), toTitleCase(m.match.Groups["title"].Value)): string.Format("{0} {1} ({2}).mp3", toTitleCase(m.match.Groups["track"].Value), toTitleCase(m.match.Groups["title"].Value), toTitleCase(m.match.Groups["composer"].Value)))
		//Artist= mp3Path.Split(Path.DirectorySeparatorChar).Reverse().ElementAt(1)
		}) 
	.Dump()
	;

foreach (var mp3 in toBeProcessed)
{
	var mp3File = new Mp3File(mp3.Input);
	
	mp3File.TagHandler.Album = mp3.Album;
	mp3File.TagHandler.Title = mp3.Title; 
	mp3File.TagHandler.Artist = mp3.Artist;
	mp3File.TagHandler.Track  = mp3.Track;		
	mp3File.TagHandler.Composer = mp3.Composer;
		
	if (updateTags)
	{		
		mp3File.Update();
	}	
}

mp3s
	.Select (s => (new Mp3File(s)).TagHandler)
	.Select (th => new {Track=th.Track, Title=th.Title, Artist=th.Artist, Composer=th.Composer, Album=th.Album })
	.Dump("Tags");
	
foreach (var mp3 in toBeProcessed)
{	
	if (updateFiles)
		new FileInfo(mp3.Input).MoveTo(mp3.SuggestedFilename);
}