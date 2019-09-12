<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\SQL Anywhere 17\Assembly\V4.5\Sap.Data.SQLAnywhere.v4.5.dll</Reference>
  <Namespace>Sap.Data.SQLAnywhere</Namespace>
  <Namespace>System.Collections.Specialized</Namespace>
  <Namespace>System.ComponentModel</Namespace>
</Query>

// Laat zien hoe een connectie op te zetten met KingDatabase
// Laat zien hoe een query uittevoeren met SACommand en parameters (SAParameter)
// Laat zien hoe een Dataset te vullen en makkelijk te dumpen met de AsDynamic handigheid

string password = File.ReadAllLines(@"D:\King_Trunk\Sources\COMP\KingLib\StaticData.txt").Last().Split('=').Last();
//password.Dump("_");

Func<string, SAConnection> connect = (dbFilename) => {
	SAConnection conn = new SAConnection();
	conn.ConnectionString = $"uid=KingSystem;pwd={password};eng=KingDanny;dbf={dbFilename}";
	conn.Open();
	return conn;
};

//string stmt = "select ArtCode, ArtZoekCode, ArtOms from tabArtikel where ArtCode = :ArtCode";
//using (SACommand command = new SACommand(stmt, connect(@"Demoart")) { CommandType = CommandType.Text })
//{
//    SAParameter param = new SAParameter(":ArtCode", SqlDbType.VarChar);
//	param.Value = "Basilicum";
//	command.Parameters.Add(param); 
//	command.Prepare();	
//	var reader = command.ExecuteReader();			
//	while (reader.Read())
//	{
//		reader["ArtCode"].Dump("ArtCode");
//		reader[1].Dump("ArtZoekCode");
//		reader[2].Dump("ArtOms");
//	}
//}	

var stmt = "select BASE64_ENCODE(DarThumbNail) as Thumbnail from tabDigitaalArchief where DarThumbNail is not null";
using (SACommand command = new SACommand(stmt, connect(@"Demoart")) { CommandType = CommandType.Text })
{

	command.Prepare();	
	var reader = command.ExecuteReader();			
	while (reader.Read())
	{
    var thumbnail = reader["ThumbNail"].Dump("Thumbnail");    
    string image = $"<img src=\"data:image/png;base64,{thumbnail}\"/>";
    Util.RawHtml(image).Dump("Thumbnail");
    Console.WriteLine(thumbnail);
		//Convert.ToBase64String(reader["DarThumbNail"] as Byte[]).Dump("DarThumbNail");
//		reader[1].Dump("ArtZoekCode");
//		reader[2].Dump("ArtOms");
	}
}	


//Action<SAConnection, string> sqlOpen = (connection, statement) => {
//	using (SACommand command = new SACommand(statement, connection) { CommandType = CommandType.Text })
//	{
//		command.Prepare();	
//		var reader = command.ExecuteReader().Dump();			
//	}	
//};

Func<SAConnection, string, IEnumerable<dynamic>> query = (connection, statement) => {
	SADataAdapter adapter = new SADataAdapter(statement, connection);	
	ReturnDataSet dataset = new ReturnDataSet();
	adapter.Fill(dataset, "ResultSet");	
	return dataset.AsDynamic();
};

using(var con = connect(@"D:\king_trunk\databases\TestMag3.db"))
{
	var statements = new string [] {
	@"
	select BepaalTeBetalenBedragInBetalingsVoorstellen(309, '34', @@BasisValutaNummer, 8, 8) as InVoorstel",
	@"select OspGid, @@BasisValutaNummer, OspValNummer, OspFactuurnummer, OspRksGid from tabOpenstaandePost where OspFactuurNummer = '34'",	
	@"
select
  9 as @@BjGid,	
  OspRksGid,
  OspValNummer BetRglValNummer,
  (OspFactuurBedragDefinitief + OspFactuurBedragVoorlopig) FactuurBedrag, // Altijd voorlopig erbij...
  (OspBetaaldBedragDefinitief + OspBetaaldBedragVoorlopig) BetaaldBedrag,
    FactuurBedrag -                                                                                      
    BetaaldBedrag -                                                                                      
    OspBedragOnderwegInValuta -                                                                           
	if DefaultSoort = 7 then
    	BepaalTeBetalenBedragInBetalingsVoorstellen(OspRksGid, OspFactuurNummer, OspValNummer, @@BjGid, @@PerNr)
	else
		- BepaalTeBetalenBedragInBetalingsVoorstellen(OspRksGid, OspFactuurNummer, OspValNummer, @@BjGid, @@PerNr)
	endif
  TeBetalenBedragInValutaVanOsp,
  if OspValNummer <> BetRglValNummer then
    ValutaOmrekenen(OspValNummer, TeBetalenBedragInValutaVanOsp, BetRglValNummer, @@BjGid, @@PerNr, 2)
  else
    TeBetalenBedragInValutaVanOsp
  endif TeBetalenBedragInValutaVanBetRgl,
  (select NawRekBankRekening from tabNawRekening inner join tabNawFile on (nawRekNawGid = NawFilNawGid) where NawFilRksGid = OspRksGid and NawFilFonId = 'C' and NawRekSoort like '%b%') BankRekening,
  (select DefaultSoort from tabRekeningschema where RksGid = OspRksGid) Defaultsoort
 
from
 tabOpenstaandePost
where
 OspFactuurNummer = '34'
	"
	};
	
	//sql.DumpFormatted();
	
	foreach (string sql in statements)
	{
	query (con, sql)
//		.Where (a => a.ArtSoort == 2)
//		.OrderBy(a => a.ArtCode)
		.Dump()
		;	
	}		
	con.Close();
}