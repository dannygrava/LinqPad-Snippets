<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IO.Compression.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IO.Compression.FileSystem.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.IO.Compression</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

const string NS_CAMT_O52 = "urn:iso:std:iso:20022:tech:xsd:camt.052.001.02";

foreach (var filename in Directory.GetFiles(@"C:\Users\dg\Documents\King\foute-camts\", "*.xml"))
{
	try
	{
		var doc = XDocument.Load(filename);
		if (doc.Root.GetDefaultNamespace() == NS_CAMT_O52)
		{
			filename.Dump("CAMT052");
		}		
	}
	catch(Exception e)
	{
		//e.Dump("Exception");				
	}		
}

foreach (var filename in Directory.GetFiles(@"C:\Users\dg\Documents\King\foute-camts\", "*.zip"))
{
	try
	{
		// Leest Camt bestanden uit een Camt-zip-container
		using (ZipArchive archive = ZipFile.OpenRead(filename))
		{		
			//archive.Entries.Dump();
			foreach(var entry in archive.Entries)
			{
				using (Stream stream = entry.Open())
				{
					// NOTE DG...StreamReader gebruikt, omdat de ABN af en toe bestanden genereerd met fouten in de encodering
					//using (XmlReader reader = XmlReader.Create(stream))
					using (StreamReader reader = new StreamReader(stream))
					{
						try
						{
							XDocument doc = XDocument.Load(reader, LoadOptions.None);
							if (doc.Root.GetDefaultNamespace() == NS_CAMT_O52)
							{
								filename.Dump("CAMT052");
							}
						}		
						catch (Exception e)
						{
//							"Fout bij inhoud zip".Dump();
//							e.Dump();
//							entry.Name.Dump();
//							filename.Dump();
							break;
						}
					}
				}
			}
		}
	}
	catch(Exception e)
	{	
//		filename.Dump("inlezen zip");
//		e.Dump();
	}
}