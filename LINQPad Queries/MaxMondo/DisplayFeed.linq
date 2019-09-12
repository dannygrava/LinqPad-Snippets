<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

string LOG_FILENAME = @"Podcast\MaxMondoPremium\MaxMondoPremium.xml".FromDownloadFolder();
XDocument rss = XDocument.Load(LOG_FILENAME);
XElement channel = rss.Root.Element("channel");

var items = channel.Elements("item");
if (true)
{
foreach (var item in items)
{
	var title = item.Element("title").Value;	
	Util.RawHtml (new XElement ("h2", title)).Dump();
	title.Dump();
	item.Element("description").Value.Dump();	
	new Hyperlinq (item.Element("link").Value, title).Dump();	
}
}

items.First().Element("description").Value.Replace("  ", "\n").Dump();