<Query Kind="Statements">
  <Connection>
    <ID>a664fc73-a723-448c-a41a-453cb78f36e4</ID>
    <Persist>true</Persist>
    <Server>.\SQLEXPRESS</Server>
    <Database>NorthWnd</Database>
  </Connection>
  <Reference Relative="..\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\ICSharpCode.SharpZipLib.dll">&lt;MyDocuments&gt;\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\ICSharpCode.SharpZipLib.dll</Reference>
  <Reference Relative="..\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\Id3Lib.dll">&lt;MyDocuments&gt;\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\Id3Lib.dll</Reference>
  <Reference Relative="..\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\Mp3Lib.dll">&lt;MyDocuments&gt;\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\Mp3Lib.dll</Reference>
  <Namespace>Mp3Lib</Namespace>
</Query>

DirectoryInfo dir = new DirectoryInfo (@"D:\Users\dg\Music\Nederlands\Neerlands Hoop Liedjes");
var mp3s = dir.GetFiles("*.mp3");

var pattern = @"\d\d[\s\-]+(?'title'.+)";

foreach(var mp3 in mp3s)
{
	try
	{	
		
		var item = new Mp3File(mp3);
		item.TagHandler.Artist = "Neerlands Hoop in Bange Dagen";
		item.TagHandler.Album = "Neerlands Hoop Liedjes";
		//item.TagHandler.Title = Path.GetFileNameWithoutExtension(mp3.Name);
		item.TagHandler.Title = Regex.Match(Path.GetFileNameWithoutExtension(mp3.Name), pattern).Groups["title"].Value;
		item.TagHandler.Track = "";
		//Regex.Match(Path.GetFileNameWithoutExtension(mp3.Name), pattern).Groups["title"].Value.Dump();
		//item.Update();
		//mp3.Dump("tag OK");
		//mp3.MoveTo(item.TagHandler.Title + ".mp3");
	}
	catch
	{	
		mp3.Dump("Id3V2 tag");
	}	
}

mp3s
	.Select (s => (new Mp3File(s)).TagHandler)
	.Select (th =>new {Track=th.Track, Title=th.Title, Artist=th.Artist, Composer=th.Composer, Album=th.Album })
	.Dump("Tags");

