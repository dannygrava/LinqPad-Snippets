<Query Kind="Statements" />

var files = new DirectoryInfo (@"D:\Users\dg\Documents\King\CAMT\UitHetVeld").GetFiles("*", SearchOption.TopDirectoryOnly);

//foreach(var file in files)
{
	using (var fs = new FileStream (@"D:\Users\dg\Documents\King\CAMT\Grote Camt.xml", FileMode.Open))//file.FullName
	{
		byte[] bytes = new byte[5]; 
		fs.Read(bytes, 0, 5);
		Encoding.UTF8.GetString(bytes).Dump();
		bytes.Dump();		
		//fs.Position.Dump();
	}
}

"PK".Select(c => (byte) c).Dump();