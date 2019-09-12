<Query Kind="Statements" />

	string source = "Ğiği"; 
	Encoding unicode = new UnicodeEncoding();	
	Encoding win1252 = Encoding.GetEncoding(1252);	
	byte[] output = Encoding.Convert(unicode, win1252, unicode.GetBytes(source));

	win1252.GetString(output).Dump();