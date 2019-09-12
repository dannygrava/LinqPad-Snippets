<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

string [] loglines; //= 

string logText = "";
//foreach(var f in Directory.GetFiles(@"C:\Users\dg\Downloads\MailChimp\Logs\", "Log_201*.txt"))
////foreach(var f in Directory.GetFiles(@"D:\SupportMaatwerk\0 Algemeen\Taak KingMailChimpConnector\Maatwerk\C# KingMailChimpConnector\KingMailChimpConnector\KingMailChimpConnector\bin\Debug\Logs\", "Log_201*.txt"))
//{
//	logText += File.ReadAllText(f); 
//}

logText = File.ReadAllText(Directory.GetFiles(@"\\support-server\KingMailChimpConnector_Logs\", "Log_201*.txt").OrderByDescending(f=>f).First().Dump("File parsed"));
//logText = File.ReadAllText(@"\\support-server\KingMailChimpConnector_Logs\Log_2017-09.txt");

// NOTE DG: p.767 Albahari&Albahari
// You can include the separators, however, by wrapping the expression in a positive lookahead.
loglines = Regex.Split(logText, @"(?m)^(?=\d{1,4}[./-]\d{1,2}[./-]\d{1,4})"); //.Dump();

loglines
	.Where(l => !string.IsNullOrEmpty(l) && Regex.IsMatch(l, @"(?i)fout|not found"))
	.Select(l => new {
		dateTime= Regex.Match (l, @"(.+?): ").Groups[1].Value, 
		mailChimpError=Regex.Match (l, "detail\":\"(.+?)\"").Groups[1].Value, 
		emailAddress=Regex.Match (l, @"member (.+)\.\s").Groups[1].Value, 		
		//overigefout= Regex.Match (l, "(?i)(?m)Fout opgetreden: (.+?)$").Groups[1].Value, 
		interestNotFound = Regex.Match (l, @"not found (.+)$").Groups[1].Value, 		
		sourceline = l,
		//source=l
		})
	//.Where(x => !string.IsNullOrEmpty(x.mailChimpError) || !string.IsNullOrEmpty(x.interestNotFound))
	.Distinct()
	//.Distinct(Comparer.Create<>((x, y) => x.mailChimpError==y.mailChimpError))
	.Dump();

//logText.Where(l => Regex.IsMatch(l, @"(?i)fout"))
//	.Dump();
//