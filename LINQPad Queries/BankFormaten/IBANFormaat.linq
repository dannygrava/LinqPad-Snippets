<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Numerics.dll</Reference>
  <Namespace>System.Numerics</Namespace>
</Query>

void Main()
{
	new Hyperlinq("https://nl.wikipedia.org/wiki/International_Bank_Account_Number", "Wiki: International Bank Account Number").Dump();	
	
	var testRekeningNrs = new string [] {
    "NL19TRIO052151234", 
    "NL69FVLB052151234", 
    "MT84MALT011000012345MTLCAST001S", 
    "NL02ABNA0123456789", 
    "NL13RABO0371323193",
    "NL32RABO0380579561", 
		"NL23MSPY0010317082", // Multi-safe pay van klant -> geen geldige IBAN volgens KING
		"NL20INGB0001234567",
		"NL57ABNA0975292048", 
		"NL55ABNA0540716855", 
		"NL54INGB0000000503", 
		"NL67ABNA0422361895", 
		"NL67RABO0115790659", 
		"NL03RABO0342918087", 
		"NL57INGB0006248704", 
		"NL92DEUT0524270929", 
		"DE19590501010067039677",
		"NL86INGB0002445588", 
		"DE88500700100175526303", 
		"NL65ABNA0514675764", 
		"NL50ABNA0471446335", 
		"NL90INGB0006080785", 
		"NL58RABO0161346871", 
		"NL98ABNA0618069992", 
		"NL09ABNA0823017850", 
		"NL31BOFA0266503853", 
		"NL21NWAB0636751678", 
		"NL97ABNA0433526734", 
		"NL63ABNA0516176390", 
		"NL20ABNA0607575123",
		"NL96RABO0157686280", 
		"NL49RABO0126519862", 
		"NL66RABO0331110598", 
		"NL62ABNA0459614835", 
		"NL17ABNA0419163735", 
		"NL14BNPA0227982975", 
		"NL48ABNA0412602679", 
		"NL10RABO0130475157", 
		"NL30INGB0695615580", 
		"NL84INGB0684765837", 
		"NL41INGB0672222086", 
		"NL71RABO0365948349", 
		"NL13INGB0652232248", 
		"DE79312512200000442442",
		"NL23INGB0005415019", 
		"NL40ABNA0472109219", 
		"NL34RABO0321027442", 
		"NL33INGB0004854082", 
		"DE65700800000358093300", 
		"NL49RABO0144449730", 
		"NL40RABO0136479693", 
		"NL68RABO0300711344", 
		"NL17RABO0162353693", 
		"NL10RABO0155199196", 
		"NL97INGB0008243662", 
		"BE66230030955043", 
		"NL03RABO0384128149", 
		"NL10FVLB0227386655", 		
		"NL78YIDZ1104515687",
		"NL27OGKO7871109286",
		"NL22WGNJ0723958467",
		"NL02OLJI1127384732",
		"NL36PMWK1610539966",
		"NL87HKOV5113553085",
		"NL50JSDI0638834198",
		"NL22PZCX2705649328",
		"NL74CQOI9971289407",
		"NL88XEMO2940962065",
		"NL71STKD4935733187",
		"NL06YLSG0624904288",
		"NL70SWRE4727795493",
		"NL53EEZP7684218263",
		"NL08OQVU8556800866",
		"NL66OSUT7586383588",
		"NL88POAA0708722687",
		"NL67XMAV5271331598",
		"NL30WOPL2132142009",		
	}
		.Select(s => new {iban = s, Landcode = s.Landcode(), Bankcode = s.Bankcode(), Controlegetal_extracted = s.Controlegetal(), Controlegetal_berekend=berekenControleGetal (s.RekeningIdentificatie(), s.Landcode())})		
		//.Where (x => x.Landcode != "NL")
		.Dump("Controle Getal")
		.Where(x => x.Controlegetal_extracted != x.Controlegetal_berekend)
		.Dump("Afwijkingen in controle getal t.o.v. berekening")
		;	
}

public static string berekenControleGetal (string rekeningIdentificatie, string landCode) 
{
	// 1. Rekeningidentificatie nemen
	var temp = rekeningIdentificatie;
	// 2. Landcode erachter plaatsen
	temp += landCode; 
	// 3. Alle letters te vervangen door twee cijfers gebaseerd op de volgorde in het Latijns alfabet beginnend met A=10, B=11, ..., Y=34, Z=35
	temp = toDigits(temp);
	// 4. twee nullen toe te voegen aan het einde
	temp += "00";
	
	BigInteger value = BigInteger.Parse(temp);
	// 5. Dan de rest bij delen door 97 nemen (mod 97)
	// 6. het controlegetal is 98 min deze rest
	// 7. als het controlegetal kleiner dan 10 is, een voorloopnul toevoegen (controlegetal 1 wordt 01).
	int ivalue = (int) (98 - (value % 97));
	return ivalue.ToString("D2");
	
}

public static string toDigits(string rekeningNr)
{
	return rekeningNr
		.ToUpper()
		.Select(c => c - (c <= '9' ? '0': ('A' - 10)))		
		.Aggregate<int, string> ("", (i, s)  => i.ToString() + s);
}

public static class IbanHelper 
{
	public static string Landcode (this string iban)
	{
		return iban.Substring(0, 2);
	}
	
	public static string Controlegetal (this string iban)
	{
		return iban.Substring(2, 2);
	}
	
	public static string Bankcode (this string iban)
	{
		return iban.Substring(4, 4);
	}
	
	public static string RekeningIdentificatie (this string iban)
	{
		return iban.Substring(4, iban.Length-4);
	}
	
	public static string Rekeningnr (this string iban)
	{
		return iban.Substring(8, iban.Length-8);
	}
}