<Query Kind="Program">
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	/// BTL91 parser
	/// Code gebaseerd op de implementatie van D:\King_Trunk\Sources\APP\naw\BetalingsOpdrachten\AanmakenOpdracht\dBTL91.pas
	/// M.a.w. als die afwijkt van de standaard, zal deze ook afwijken.	
	//const string BTL91FileName = @"D:\Users\dg\Documents\King\Bovk\BTL91_20151215_001.TXT";
	//const string BTL91FileName = @"D:\Users\dg\Documents\King\BTL91DefectAKA.txt";
	const string BTL91FileName = @"D:\Users\dg\Documents\My Received Files\BTL91 bestand.txt";
	//const string BTL91FileName = @"D:\Users\dg\Documents\King\BTL91_20160104_001.TXT";
	//const string BTL91FileName = @"D:\Users\dg\Documents\King\BTL91 CL-1929611.txt";
	var lines = File.ReadAllLines(BTL91FileName);
	
	//lines.Dump();		
	lines
		.Where(line => line.Substring(0,2) == "11")
		.Select(line => new {
					RecordCode = line.Substring(0,2), 					
					DeviezenBank = line.Substring(2, 4), 
					MediumCode = line.Substring(6,1),
					Versienummer = line.Substring(7,2),
					Aanmaakdatum = ParseDateTime(line.Substring(9,8)),
					Batchnummer = line.Substring(17, 3),
					NaamOpdrachtgever = line.Substring(20, 35).Trim(),
					AdresOpdrachtgever = line.Substring(55, 35).Trim(),
					PlaatsOpdrachtgever = line.Substring(90, 35).Trim(),
					LandOpdrachtgever = line.Substring(125, 35).Trim(),
					BedrijfstakOpdrachtgever = line.Substring(160, 4),
					Uitvoeringsdatum = ParseDateTime(line.Substring(164, 8)),				
					})
		.Dump("Voorloop record");

	lines
		.Where(line => line.Substring(0,2) == "21")
		.Select(line => new {
					RecordCode = line.Substring(0,2), 					
					Opdrachtnummer = line.Substring(2,4), 
					ValutaCodeRekening = line.Substring(6,3), 
					Rekeningnummer = line.Substring(9,10), 
					ValutaCodeBetaling = line.Substring(19,3), 
					TeBetalenBedrag = ParseDecimal(line.Substring(22,15)), // let geen decimalen punt, laatste drie zijn decimalen
					VerwerkingsDatum = ParseDateTime(line.Substring(37,8)), 
					CodeKostenBuitenLand = line.Substring(45,1), 
					CodeKostenBinnenCorrespondent = line.Substring(46,1), 
					CodeSoortOpdracht = line.Substring(47,1), 
					CodeSoortUitvoering = line.Substring(48,1), 
					CodeChequesCrossen = line.Substring(49,1), 
					CodeBetaalInstructie1 = line.Substring(50,2), 
					CodeBetaalInstructie2 = line.Substring(52,2), 
					CodeBetaalInstructie3 = line.Substring(54,2), 
					CodeBetaalInstructie4 = line.Substring(56,2),
					//NooitIngevuldRestant = line.Substring(58, 192-58),				
					//raw=line.Trim(),
					})
		.Dump("Betaal record 1 'Opdrachtgever'");

	lines
		.Where(line => line.Substring(0,2) == "22")
		.Select(line => new {
					line.Length,
					RecordCode = line.Substring(0,2), 					
					Opdrachtnummer = line.Substring(2, 4), 
					Rekeningnummer = line.Substring(6,34).Trim(),
					Naam = line.Substring(40,35).Trim(),
					Adres = line.Substring(75,35).Trim(),
					Woonplaats = line.Substring(110, 35).Trim(),
					HeeftSpatie = line.Substring(110, 35).StartsWith(" "),
					LandCode = line.Substring(145, 2),
					LandNaam = line.Substring(147, 35).Trim(),					
					})
		.Dump("Betaal record 2 'Begunstigde'");

	lines
		.Where(line => line.Substring(0,2) == "23")
		.Select(line => new {
					line.Length,
					RecordCode = line.Substring(0,2), 					
					Opdrachtnummer = line.Substring(2, 4),
					Swift = line.Substring(6, 11),
					Naam =  line.Substring(17, 35).Trim(),
					Adres =  line.Substring(52, 35).Trim(),
					Woonplaats =  line.Substring(87, 35).Trim(),
					LandCode =  line.Substring(122, 2),
					LandNaam =  line.Substring(124, 35).Trim(),
					raw=line.Trim(),
					})
		.Dump("Betaal record 3 'Bank begunstigde'");

	lines
		.Where(line => line.Substring(0,2) == "24")
		.Select(line => new {
					RecordCode = line.Substring(0,2), 					
					Opdrachtnummer = line.Substring(2, 4),
					Reden1 = line.Substring(6, 35).Trim(),
					Reden2 = line.Substring(41, 35).Trim(),
					Reden3 = line.Substring(76, 35).Trim(),
					Reden4 = line.Substring(111, 35).Trim(),			
					})
		.Dump("Betaal record 4 'Redenen'");

		lines
			.Where(line => line.Substring(0,2) == "31")
			.Select(line=> new {
					RecordCode = line.Substring(0,2), 					
					Muntsoort = line.Substring(2, 3), 
					TotaalBedragTeBetalen = ParseDecimal(line.Substring(5, 15)), 
					TotaalAantalOpdrachten = line.Substring(20, 4), 
					}
					)
			.Dump("ValutaRecord");

		lines
			.Where(line => line.Substring(0,2) == "41")
			.Select(line=> new {
					RecordCode = line.Substring(0,2), 					
					AantalRecords = line.Substring(2, 6), 
					AantalTransacties = line.Substring(8, 4),
					Hash = line.Length >= 36 ? line.Substring(12, 24): ""
					}
					)
			.Dump("Sluit record");			

	"Overzicht van Incidenten".Dump();
	new Hyperlinq("http://ontime-server.quadrant.local/OnTimeWeb/viewitem.aspx?id=I08885&type=incidents&force_use_number=true", "Incident I08885 Problemen met betaling BTL 91 bestand.").Dump();	

}

public static string ParseDecimal(string value)
{
	decimal temp;
	if (decimal.TryParse(value, out temp))
		return string.Format("{0:F3}", temp / 1000m);
	else
		return string.Format ("*{0}", value);
}

public static string ParseDateTime(string value)
{
	DateTime temp;
	if (DateTime.TryParseExact(value, "yyyyMMdd", null, DateTimeStyles.AssumeLocal, out temp))
	{
		return temp.ToString("dd-MM-yyyy");
	}
	else
	{
		return string.Format("*{0}", value);
	}
	
}