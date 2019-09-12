<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

String direction = "";
WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
using (WebResponse response = request.GetResponse())
using (StreamReader stream = new StreamReader(response.GetResponseStream()))
{
    direction = stream.ReadToEnd();
}

//Search for the ip in the html
int first = direction.IndexOf("Address: ") + 9;
int last = direction.LastIndexOf("</body>");
direction = direction.Substring(first, last - first);

direction.Dump();