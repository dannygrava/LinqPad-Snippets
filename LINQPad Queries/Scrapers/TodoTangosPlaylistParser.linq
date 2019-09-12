<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

//string jsonString = File.ReadAllText("Playlist_Instrumentales.json".FromLinqpadDataFolder());
string patternJaartal = @"\b((19|20)\d{2})\b";

string jsonString = File.ReadAllText("Playlist_Multiple.json".FromLinqpadDataFolder());
dynamic[] items = JsonConvert.DeserializeObject<dynamic[]>(jsonString);
var tangos = items
	.Select (x => new {id= int.Parse((string) x.id), titulo=WebUtility.HtmlDecode((string) x.titulo), canta= WebUtility.HtmlDecode((string) x.canta), formacion=WebUtility.HtmlDecode((string) x.formacion), url=x.mp3, detalles=x.detalles, year=Regex.Match((string) x.detalles, patternJaartal).Groups[0].Value})
	//.OrderBy(x => x.titulo)	
	.OrderBy(x => x.id)	
	.Dump(0);
	
//var selected = new int[] {1115, 726, 861, 1879, 6290, 2878, 1460, 1745, 2341, 3669, 7297, 813, 7238, 7103, 668, 1290, 1880, 2652, 646};	

var selected = new int[] {811, 889};

var toDownload = tangos.Join(
	selected,
	x => x.id,
	y => y,
	(x,y) => x
	).Dump(0);
	
using (WebClient client = new WebClient ())
{
	foreach (var tango in toDownload)
	{
		string uitvoerende = tango.canta != "Instrumental"?  tango.canta + " y " + tango.formacion: tango.formacion;
		string destination = $@"TodoTango\{tango.titulo} - {uitvoerende} - {tango.year}.mp3".FromDownloadFolder();
		
		if (!File.Exists(destination))
		{
			client.DownloadFile(
				$"http://souther4.wwwss10.a2hosted.com/media/mp3/{tango.id}.mp3", 
				destination
			);
			$"{destination}".Dump();	
		}
		else
		{
			$"SKIPPED: {destination}".Dump();	
		}
	}
}