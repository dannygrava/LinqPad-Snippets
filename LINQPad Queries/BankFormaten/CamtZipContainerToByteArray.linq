<Query Kind="Statements">
  <GACReference>System.IO.Compression, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</GACReference>
  <GACReference>System.IO.Compression.FileSystem, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</GACReference>
  <Namespace>System.IO.Compression</Namespace>
</Query>

// Lees een zip stream
// Bewaar deze als byte array (voor transport)
// Lees deze byte array uit en deflate de resulting stream


// Optie 1: Met Reader
using (BinaryReader binReader = new BinaryReader(File.Open(@"D:\users\dg\documents\king\camt\UitHetVeld\abnamro.zip", FileMode.Open)))
//using (BinaryReader binReader = new BinaryReader(File.Open(@"D:\users\dg\documents\king\camt\UitHetVeld\CAMT053.xml", FileMode.Open)))
//using (BinaryReader binReader = new BinaryReader(File.Open(@"D:\users\dg\documents\king\camt\EenPdf.zip", FileMode.Open)))
{
	if (binReader.BaseStream.Length > Int32.MaxValue)
		throw new Exception("Stream too large");
		
	// Read the source file into a byte array. 
	byte[] bytes = binReader.ReadBytes((int) binReader.BaseStream.Length);	
	bytes.Dump();		
	
	using (ZipArchive archive = new ZipArchive(new MemoryStream(bytes)))
	{
		
		archive.Entries.Dump();
		foreach(var entry in archive.Entries)
		{
			using (Stream s = entry.Open())
			{
				using (XmlReader reader = XmlReader.Create(s))
				{
					XDocument xdoc = XDocument.Load(reader, LoadOptions.None);
					xdoc.Dump("Reading camt from zip container");
				}
			}
		}
	}	
}

return;
// Optie 2: Zonder reader
using (FileStream stream = File.Open(@"D:\users\dg\documents\king\camt\UitHetVeld\abnamro.zip", FileMode.Open))
{
	// Read the source file into a byte array. 
	byte[] bytes = new byte[stream.Length];
	int numBytesToRead = (int)stream.Length;
	int numBytesRead = 0;
	while (numBytesToRead > 0)
	{
		// Read may return anything from 0 to numBytesToRead. 
		int n = stream.Read(bytes, numBytesRead, numBytesToRead);
	
		// Break when the end of the file is reached. 
		if (n == 0)
			break;
	
		numBytesRead += n;
		numBytesToRead -= n;
	}
	bytes.Dump();		
	
	using (ZipArchive archive = new ZipArchive(new MemoryStream(bytes)))
	{
		
		archive.Entries.Dump();
		foreach(var entry in archive.Entries)
		{
			using (Stream s = entry.Open())
			{
				using (XmlReader reader = XmlReader.Create(s))
				{
					XDocument xdoc = XDocument.Load(reader, LoadOptions.None);
					xdoc.Dump("Reading camt from zip container");
				}
			}
		}
	}	
}