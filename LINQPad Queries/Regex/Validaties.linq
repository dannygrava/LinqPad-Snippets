<Query Kind="Statements" />

//var pattern = @"^([\d]+\.?[\d]+)$";
var pattern = @"^[\+]{0,1}[0-9]{1,13}[.][0-9]{2}$";
Regex.IsMatch("+1231.23", pattern).Dump();
Regex.IsMatch("123123123", pattern).Dump();
Regex.IsMatch("12.31.23123", pattern).Dump();


return;
//var pattern = @"^\d{1,3}$";
pattern = @"^[A-Z]{2}$";
//var pattern = @"^\p{L}{2}$";
//var pattern = @"\A[A-Z]{2}\z";
Regex.IsMatch("NL", pattern).Dump();
Regex.IsMatch("NLD", pattern).Dump();
Regex.IsMatch("N1", pattern).Dump();
Regex.IsMatch("ÑL", pattern).Dump();
Regex.IsMatch("nl", pattern).Dump();

pattern = @"^[a-zA-z0-9]{8}|[a-zA-z0-9]{11}$";

Regex.IsMatch("ABNANL2A", pattern).Dump();
Regex.IsMatch("ABNÄNL2A", pattern).Dump();
Regex.IsMatch("N1", pattern).Dump();
Regex.IsMatch("RABONL2U123", pattern).Dump();

pattern = @"^[a-zA-Z0-9/\-?: ().,'+]{1,35}$";
Regex.IsMatch("TEST.Betaalde incasso.TC-1.1", pattern).Dump();