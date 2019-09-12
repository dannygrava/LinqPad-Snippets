<Query Kind="Program" />

void Main()
{
	/// <Summary>
	/// Doel is om een minimale Camt bestand te bouwen dat ge-importeerd kan worden door de KIB-service en King5
	/// </Summary>
	
	XElement statement = createStatement();
	statement.Add(createAccount ("NL20INGB0001234567"));
	statement.Add(createBalans(BalansSoort.Begin, "2015-10-12T09:10:52", BijAfIndicator.Bij, 65m));			
	statement.Add(createBalans(BalansSoort.Eind, "2015-10-12", BijAfIndicator.Bij, 65m));			
	
	Random r = new Random();	
	var entries = Enumerable.Range(1, 10000).Select (x => createEntry(BijAfIndicator.Bij, (r.Next() * 10000m) / 100, boekDatum: "2015-12-11", oms: new String('a', 500)));
	statement.Add(entries);
//	statement.Add(createEntry(BijAfIndicator.Bij, 100m, "18239-90123-20140-26500"));			
//	statement.Add(createEntry(BijAfIndicator.Af, 80.00m, "18239-90123-20140-26500", "2015-10-16T09:14:00"));			
	
	//XmlConvert.ToDateTime("2015-10-16T09:14:00").Dump();
	XDocument camt = createCamtDocument(statement);		
	camt.Dump();
	camt.Save(@"D:\Users\dg\Documents\King\Camt\Grote Camt.xml");
}

private static readonly XNamespace _ns = "urn:iso:std:iso:20022:tech:xsd:camt.053.001.02";
private enum BalansSoort {Begin, Eind};
private enum BijAfIndicator {Bij, Af};

private static XElement createEntry(BijAfIndicator bijAf, decimal amount, string betalingskenmerk = "", string boekDatum = "", string oms = "")
{
    var entry =  new XElement(_ns + "Ntry", 
        new XElement(_ns + "Sts", "BOOK"), 
        new XElement(_ns + "CdtDbtInd", getDebetCreditValue(bijAf)), 
        new XElement(_ns + "Amt", new XAttribute("Ccy", "EUR"), amount)
        );

    if (!string.IsNullOrEmpty(betalingskenmerk))
        entry.Add(new XElement(_ns + "NtryDtls", new XElement(_ns + "TxDtls", new XElement(_ns + "RmtInf", new XElement(_ns + "Strd", new XElement(_ns + "CdtrRefInf", new XElement(_ns + "Ref", betalingskenmerk)))))));
    if(!string.IsNullOrEmpty(boekDatum))
        entry.Add(new XElement(_ns + "BookgDt", new XElement(_ns + (boekDatum.Contains("T") ? "DtTm" : "Dt"), boekDatum)));		
	if (!string.IsNullOrEmpty(oms))
		entry.Add(new XElement(_ns + "AddtlNtryInf", oms));
		
    return entry;
}

private static XElement createBalans(BalansSoort balansSoort, string datumTijd, BijAfIndicator? bijAf, decimal? amount)
{
    string balansCode = balansSoort == BalansSoort.Begin ? "OPBD" : "CLBD";

    XElement beginbalans = new XElement(_ns + "Bal", new XElement(_ns + "Tp", new XElement(_ns + "CdOrPrtry", new XElement(_ns + "Cd", balansCode))));

	if (!string.IsNullOrEmpty(datumTijd))
	{
        beginbalans.Add(new XElement(_ns + "Dt", new XElement(_ns + (datumTijd.Contains("T") ? "DtTm" : "Dt"), datumTijd)));		
	}

    if (bijAf != null)
        beginbalans.Add(new XElement(_ns + "CdtDbtInd", getDebetCreditValue(bijAf.Value)));
    if (amount != null)
        beginbalans.Add(new XElement(_ns + "Amt", new XAttribute("Ccy", "EUR"), amount));

    return beginbalans;
}

private static XElement createAccount(string iban)
{
	return new XElement(_ns + "Acct", new XElement(_ns + "Id", new XElement(_ns + "IBAN", iban)), new XElement(_ns + "Ccy", "EUR"));
}

private static XElement createStatement()
{
	return new XElement(_ns + "Stmt");
}

private static XDocument createCamtDocument(XElement statement)
{
	return new XDocument(
				new XElement(_ns + "Document",
		            new XElement(_ns + "BkToCstmrStmt", statement)
		        )
		    );
}

private static string getDebetCreditValue(BijAfIndicator bijAf)
{
    return bijAf == BijAfIndicator.Bij ? "CRDT" : "DBIT";
}