<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\SQL Anywhere 17\Assembly\V4.5\Sap.Data.SQLAnywhere.v4.5.dll</Reference>
  <Namespace>Sap.Data.SQLAnywhere</Namespace>
</Query>

// Laat zien hoe een connectie op te zetten met KingDatabase
// Laat zien hoe een query uittevoeren met SACommand en parameters (SAParameter)
// Laat zien hoe een Dataset te vullen en makkelijk te dumpen met de AsDynamic handigheid (nieuwe feature LINQPad file:///C:/Users/dg/AppData/Local/Temp/LINQPad5/WhatsNew.html)

string password = File.ReadAllLines(@"D:\King_Trunk\Sources\COMP\KingLib\StaticData.txt")
  .Reverse()  
  .Skip(1) // Grootste gedeelte van het jaar moeten we de een-na-laatste hebben, korte periodes van het jaar de laatste :-(  
  .First()
  .Split('=')
  .Last();
  
string kingServerNaam = "QuadProd";
string dbName = "Quadrant";
string procName = "KingMailChimpConnector";
string lijstnaam = "2017 King Klanten Nieuws";
//string lijstnaam = "Lijst_DemoArt";

//password.Dump("_");

Func<SAConnection> connect = () => {
	SAConnection conn = new SAConnection();
	conn.ConnectionString = $"uid=KingSystem;pwd={password};eng={kingServerNaam};dbn={dbName};ASTART=No;host=webs.quadrant.local:2638";	
	conn.Open();
	return conn;
};

Func<SAConnection, string, IEnumerable<dynamic>> query = (connection, statement) => {
	SADataAdapter adapter = new SADataAdapter(statement, connection);	
	ReturnDataSet dataset = new ReturnDataSet();
	adapter.Fill(dataset, "ResultSet");	
	return dataset.AsDynamic();
};

string stmt = $"call MAATWERK_KingMailChimpConnector('{lijstnaam}', '{procName}')";

// Apply caching: similar to what Cache() does
string dataName = procName+lijstnaam;
var mailChimpData = AppDomain.CurrentDomain.GetData (dataName);
(mailChimpData != null).Dump("Assigned?");
if (mailChimpData == null)
{
	using (var connection = connect())
	{
		mailChimpData = query (connection, stmt);		
		AppDomain.CurrentDomain.SetData (dataName, mailChimpData);
	}
}

IEnumerable<dynamic> data = mailChimpData as IEnumerable<dynamic>;
//mailChimpData.Dump();
//data.Where(x => x.PAKKETS != null && x.PAKKETS != "King" && x.PAKKETS != "Queen").Dump();
data
//	.Count()
//	.Where (x => x.PAKKETS == "Excel2King")
//   .Where(x => x.NAWBEST != "D")
//	.GroupBy (x => x.NAWBEST)
//	.Where( x=> x.GroupBy(y => new {y.SAB, y.NAWBEST, y.LICNR}).Count() != 1)
	//.Select (g => new {g.Key, items = g})
	//.OrderBy (x => x.Key)
//	.Where (x => x.Grp_Naam != null)
//	.OrderBy(x => x.Grp_Naam )
	//.GroupBy (x => x.Grp_Naam)
	//.GroupBy (x => x.EMAIL.ToUpper()[0])
	//.Select (g => new {g.Key, Count=g.Count()})	
  .Dump("Rauw")
	.Where(x => x.Grp_Inschakelen == true)  
	.Select(x => new {x.EMAIL, x.NawNr})
	.Distinct()
	.Dump()
	.Count()
	.Dump()
	;