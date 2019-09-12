<Query Kind="Statements">
  <Namespace>System.Globalization</Namespace>
</Query>

string folder = @"Podcast".FromDownloadFolder();
var output = File.ReadAllLines(Path.Combine(folder, "ExtraOutput.txt"));
output.Dump(0);


string LOG_FILENAME = @"Podcast\LieveMarianne.xml".FromDownloadFolder();

var rss = XDocument.Load(LOG_FILENAME);
XElement channel = rss.Root.Element("channel");

foreach (var podcastUri in output)
{
	DateTime dateInTitle;
	string title = "Lieve Marianne";
	if (DateTime.TryParseExact(Path.GetFileNameWithoutExtension(Path.GetFileName(podcastUri)).Substring(0, 8), "yyyyMMdd", null, DateTimeStyles.None, out dateInTitle))
		title = $"{title}, {dateInTitle:D}";				 			
	
	XElement item  = new XElement("item");
	item.Add(new XElement("title", title));
	item.Add(new XElement("link", podcastUri));
	item.Add(new XElement("description"));
	channel.AddFirst(item);
}

rss.Dump();
//rss.Save(LOG_FILENAME);