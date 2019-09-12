<Query Kind="Statements">
  <Namespace>System.Globalization</Namespace>
</Query>

Func<string, DateTime> GetDate = (s)=> 
{
	DateTime dateInTitle;
	if (DateTime.TryParseExact(Path.GetFileNameWithoutExtension(Path.GetFileName(s)).Substring(0, 8), "yyyyMMdd", null, DateTimeStyles.None, out dateInTitle))
		return dateInTitle;
	return new DateTime(2000, 1, 1);
};

//const string LOG_FILENAME = @"D:\Users\dg\Downloads\Podcast\LieveMarianne\LieveMarianne.xml";
string LOG_FILENAME = @"\Podcast\LieveMarianne\LieveMarianne.xml".FromDownloadFolder();

var rss = XDocument.Load(LOG_FILENAME);
XElement channel = rss.Root.Element("channel");

var items = channel.Elements("item");
items.Count().Dump("Aantal podcasts");
foreach (var item in items.Where(it => GetDate(it.Element("link").Value).Year > 2000).Reverse())
{
	var title = item.Element("title").Value;
	if (Path.GetExtension(item.Element("link").Value).ToLower() == ".mp4")
		title = title + " (video)";	
	new Hyperlinq (item.Element("link").Value, title).Dump();
}
//rss.Dump("RSS");