<Query Kind="Statements" />

// BIC afleiden uit IBAN (Nederland)
string iban = "NL96RABO0157686280";

string bankcode = iban.Substring(4, 4);
string landcode = iban.Substring(0, 2);
string controleteken = "?";

string plaatscode = $"2{controleteken}"; // Voor Nederland
string filiaalnummer = ""; // Voor Nederland

string bic = $"{bankcode}{landcode}{plaatscode}{filiaalnummer}";

bic.Dump();





