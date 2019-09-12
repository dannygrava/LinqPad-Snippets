<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

using (WebClient client = new WebClient ())
{
	int key = 627;	
	int track = 2;
	//string title = "Una emoción - Guitarras de Roberto Grela, Osvaldo Ribó";	
	string title = "La cumparsita - Sexteto Mayor";

	client.DownloadFile(
		$"http://souther4.wwwss10.a2hosted.com/media/mp3/{key}.mp3", 
		$@"TodoTango\{track:D2} {title}.mp3".FromDownloadFolder()
	);
	"!".Dump();
}

// Voor de volgende versie:
var zoekUrl = "http://www.todotango.com/buscar/?kwd=milonga+de+mis+amores";

// todo regex for this string:
// <h3><a id="main_buscar1_RP_Temas_hl_Tema_0" href="http://www.todotango.com/musica/tema/145/Milonga-de-mis-amores/">Milonga de mis amores</a></h3>

// todo regex for this
//{id:"1947",idtema:"145",titulo:"Milonga de mis amores",canta:"Instrumental",detalles:"Ene 14 1944 Buenos Aires Odeon 7635 13523",duracion:"02&#39;26&quot;",formacion:"Orquesta Pedro Laurenz",oga:"http://souther4.wwwss10.a2hosted.com/media/ogg/1947.ogg",mp3:"http://souther4.wwwss10.a2hosted.com/media/mp3/1947.mp3",existeenlistasusuario:"0",idusuario:"0"}