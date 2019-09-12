<Query Kind="Statements" />

new Hyperlinq("https://msdn.microsoft.com/en-us/library/windows/desktop/aa384106(v=vs.85).aspx", "WinHttpRequest object").Dump();
dynamic WinHttpReq  = Activator.CreateInstance(Type.GetTypeFromProgID("WinHttp.WinHttpRequest.5.1"));

// ClientCertificate [in] Specifies the location, certificate store, and subject of a client certificate.
new Hyperlinq("https://msdn.microsoft.com/en-us/library/windows/desktop/aa384055(v=vs.85).aspx", "IWinHttpRequest::SetClientCertificate method").Dump();
WinHttpReq.SetClientCertificate(@"CURRENT_USER\MY\KIB-King5");

// Boundary die de verschillende delen van het multi-part content gedeelte scheidt
string STR_BOUNDARY = "gc0p4Jq0M2Yt08jU534c0p";

//  Create an HTTP request.
WinHttpReq.Open("GET", "https://kibserviceacceptatie.cloudapp.net/api/rekeningafschriften", false);

//  Send the HTTP request.
WinHttpReq.Send();
// Post data to the HTTP server.
//WinHttpReq.Send("Post data");  
string strResult = WinHttpReq.ResponseText;
strResult.Dump("GET");

WinHttpReq.Open("POST", "https://kibserviceacceptatie.cloudapp.net:443/api/rekeningafschriften/", false);
WinHttpReq.SetRequestHeader("Accept", "application/json");
WinHttpReq.SetRequestHeader("Content-Type", "multipart/form-data; boundary=" + STR_BOUNDARY);
WinHttpReq.SetRequestHeader("Content-Length", "4000");

new Hyperlinq("http://www.w3.org/Protocols/rfc1341/7_2_Multipart.html", "The Multipart Content-Type").Dump();
// NOTE DG
// Het multipart protocol luistert nauw qua CRLF's. Het moeten echt CRLF's en geen LF's, want ander krijg je foutmeldingen als 400 en 415
//string multipartContent = "--"  + STR_BOUNDARY + "\r\n" + 		
//	"Content-Disposition: form-data; name=Camt; filename=CAMT053_TestMag3_01.zip\r\n"  +	
//	"Content-Type: application/octet-stream\r\n" + 	
//	"\r\n" +
//	File.ReadAllText(@"D:\Users\dg\Documents\King\CAMT\CAMT053_TestMag3_01.zip") + 
//	"\r\n"+ 
//	//Convert.ToBase64String(File.ReadAllBytes(@"D:\Users\dg\Documents\King\CAMT\CAMT053_TestMag3_01.zip"), Base64FormattingOptions.InsertLineBreaks) + ";\r\n" + 
//	"--" + STR_BOUNDARY + "--"; 
string multipartContent = "--"  + STR_BOUNDARY + "\r\n" + 	
	"Content-Disposition: form-data; name=Camt; filename=CAMT053_TestMag3_AllesIngevuld.zip; filename*=utf-8''TestCamt.zip\r\n\r\n"  +	
	File.ReadAllText(@"D:\Users\dg\Documents\King\CAMT\CAMT053_TestMag3_01.xml") + "\r\n" + 
	"--" + STR_BOUNDARY + "--"; 
//WinHttpReq.Send(multipartContent);         
multipartContent = "--"  + STR_BOUNDARY + "\r\n" + 	
	"Content-Disposition: form-data; name=Camt; filename=CAMT053_TestMag3_AllesIngevuld.zip; filename*=utf-8''TestCamt.zip\r\n\r\n"; 
byte[] bytes = Encoding.UTF8.GetBytes(multipartContent)
	.Concat(File.ReadAllBytes(@"D:\Users\dg\Documents\King\CAMT\CAMT053_TestMag3_01.zip"))
	.Concat(Encoding.UTF8.GetBytes("\r\n--" + STR_BOUNDARY + "--"))
	.ToArray();

WinHttpReq.Send(bytes);         	

//  Retrieve the response text.
string resp  = WinHttpReq.ResponseText;
resp.Dump("Response from KIB");
int status = WinHttpReq.Status;
status.Dump("POST Status"); 
Encoding.Default.GetString(bytes).Dump("Bytes");
//multipartContent.Dump();