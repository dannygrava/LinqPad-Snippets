<Query Kind="Statements" />

var str = @"%6D%70%33";
System.Uri.UnescapeDataString(str).Dump();
System.Uri.EscapeDataString("WMA").Dump();

var bytes = Encoding.Default.GetBytes ("King.pdf");
var result = System.Net.WebUtility.UrlEncodeToBytes(bytes, 0, bytes.Length).Dump();

StringBuilder sb = new StringBuilder();
result.Aggregate(sb, (builder, b) => builder.Append(string.Format("%{0:X2}", b)));
//result.Aggregate(sb, (builder, b) => builder.Append("%" + b.ToString("X2")));

//	foreach (byte b in result)
//		sb.Append("%" + b.ToString("X2"));
		
System.Net.WebUtility.UrlDecode(sb.ToString().Dump("Gecodeerd")).Dump("Gedecodeerd");