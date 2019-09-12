<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.IO.Compression.dll</Reference>
  <Namespace>System.IO.Compression</Namespace>
</Query>

//var fs = new MemoryStream();
using (var fs = new FileStream(@"D:\Test.zip", FileMode.Create))
{
	// Close op de archive is essentieel
	using(ZipArchive archive = new ZipArchive(fs, ZipArchiveMode.Create))
	{
		ZipArchiveEntry entry = archive.CreateEntry("Test.txt");
		using (StreamWriter sw = new StreamWriter (entry.Open()))	
		{		
			sw.WriteLine("Hello Archive");
			sw.WriteLine("=============");
		}
	}

	//fs.Flush();
}
