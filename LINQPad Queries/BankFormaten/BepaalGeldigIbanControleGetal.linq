<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Numerics.dll</Reference>
  <Namespace>System.Numerics</Namespace>
</Query>

void Main()
{            
//
	Enumerable.Range(0, 100).Select(x => $"NL{x:D2}INGB0005744991").First(s => IsGeldigeIban(s)).Dump();	
	var iban = MaakGeldigeIban("NL", "RABO", "0371323193").Dump();		
	IsGeldigeIban(iban).Dump();
}

// Define other methods and classes here
public static bool IsGeldigeIban(string rekeningnummer)
{
    if (string.IsNullOrEmpty(rekeningnummer))
        return false;
    // Controle 10 of korter en niet langer dan 35
    if (rekeningnummer.Length <= 10 || rekeningnummer.Length > 35)
        return false;


    string temp = rekeningnummer.ToUpper();
    // 2. verplaats de eerste 4 karakters naar het einde
    temp = temp.Substring(4) + temp.Substring(0, 4);
    // 3. vervang elke letter door 2 cijfers, waarbij A = 10, B = 11, ..., Z = 35
    StringBuilder sb = new StringBuilder();
    foreach (char c in temp)
    {
        if (char.IsLetter(c))
        {
            sb.Append($"{c - 'A' + 10:D2}");
        }
        else
        {
            sb.Append(c);
        }
    }

    // 4. bereken dan het getal modulo 97
    var remainder = BigInteger.Parse(sb.ToString()) % 97;
    // 5. als de restwaarde 1 is, dan klopt het nummer op basis van het controlecijfer en kan het IBAN valide zijn
    return remainder == 1;
}

// Define other methods and classes here
public static string MaakGeldigeIban(string landCode, string bankCode, string rekeningnummer)
{
	string basis = $"{bankCode}{rekeningnummer}{landCode.ToUpper()}00";
    // 2. vervang elke letter door 2 cijfers, waarbij A = 10, B = 11, ..., Z = 35
    StringBuilder sb = new StringBuilder();
    foreach (char c in basis)
    {
        if (char.IsLetter(c))
        {
            sb.Append($"{c - 'A' + 10:D2}");
        }
        else
        {
            sb.Append(c);
        }
    }

    // 3. bereken dan het getal modulo 97
    var remainder = BigInteger.Parse(sb.ToString()) % 97;
    // 4. bereken controle getal
	var controlegetal = 98-remainder;
	// 5. Stel IBAN samen
	string iban = $"{landCode}{controlegetal:D2}{bankCode}{rekeningnummer}";
    return iban;
}