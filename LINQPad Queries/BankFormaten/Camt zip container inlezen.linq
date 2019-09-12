<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.IO.Compression.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IO.Compression.FileSystem.dll</Reference>
  <Namespace>System.IO.Compression</Namespace>
</Query>

// Leest Camt bestanden uit een Camt-zip-container
using (ZipArchive archive = ZipFile.OpenRead(@"D:\users\dg\documents\king\camt\UitHetVeld\abnamro.zip"))
{
	
	archive.Entries.Dump();
	foreach(var entry in archive.Entries)
	{
		using (Stream stream = entry.Open())
		{
			using (XmlReader reader = XmlReader.Create(stream))
			{
				XDocument xdoc = XDocument.Load(reader, LoadOptions.None);
				xdoc.Dump("Reading camt from zip container");
			}
		}
	}
}