<Query Kind="Statements">
  <Reference>&lt;MyDocuments&gt;\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\ICSharpCode.SharpZipLib.dll</Reference>
  <Reference>&lt;MyDocuments&gt;\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\Id3Lib.dll</Reference>
  <Reference>&lt;MyDocuments&gt;\Visual Studio 2010\Projects\id3lib\Mp3Lib\bin\Release\Mp3Lib.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Namespace>Id3Lib</Namespace>
  <Namespace>Mp3Lib</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

string folder = @"Podcast\MaxMondoPremium".FromDownloadFolder();
const string LOG_FILENAME = @"MaxMondoPremium.xml";

const string searchPattern = @"*.mp3";

var mp3s = Directory
	.EnumerateFiles(folder, searchPattern)
	.OrderBy(s => s)	
	.Select (s => new {FileName = s, th= new TagHandler(new Mp3File(s).TagModel)})		
	.Select (m => new {FileName = m.FileName, m.th.Track, Title=m.th.Title, Artist=m.th.Artist, Album=m.th.Album, Comment = m.th.Comment, Year = m.th.Year})
	.Dump(0);

var rss = new XDocument(new XElement ("rss", new XAttribute("version", "2.0")));
XElement channel = new XElement("channel");
rss.Root.Add(channel);
	
foreach (var mp3 in mp3s)
{
	//if (mp3.Comment.Contains("Trieste"))
//		mp3.Comment.Dump($"{mp3.Title} - {mp3.Year}");
	XElement item  = new XElement("item");
	item.Add(new XElement("title", mp3.Title));
	item.Add(new XElement("link", new Uri(new Uri(@"http://traffic.libsyn.com/maxmondo1it/"), Path.GetFileName(mp3.FileName))));
	item.Add(new XElement("description", mp3.Comment.Replace("  ", "\r\n")));
	//channel.AddFirst(item);		
	channel.Add(item);	
}

rss.Dump();
rss.Save($@"{folder}\{LOG_FILENAME}");