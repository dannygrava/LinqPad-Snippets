<Query Kind="Statements" />

//TODO : \x27\x22 are single/double quotes
string line = @"'Between quotes with separator;';'Between quotes without separator';No will follow two empty matches;;;Without quotes;Without quotes 2;'With ;several ;separators';'Value without closing separator(at end of line)'";
var r3 = Regex.Matches (line + ";", @"'(?'value'[^']*)(';?)|(')|(?'value'[^';]*);");
//r3.Dump();
foreach (Match m in r3)
{
  Console.WriteLine (m.Groups["value"].Value);  
}

Console.WriteLine ("==============================================================");  
Console.WriteLine ("Now with the quotes replaced by double quotes and separator =,");  
Console.WriteLine ("==============================================================");  
// Nu de versie voor delimited tekst met "" en ,
string line2 = @"""Between quotes with separator,"",""Between quotes without separator"",Without quotes,""With ,several ,separators"",Value without closing separator(at end of line)";
var r4 = Regex.Matches (line2 + ",", @"\x22(?'value'[^\x22]*)\x22,*|(?'value'[^\x22,]*),");
foreach (Match m in r4)
{
  Console.WriteLine (m.Groups["value"].Value);  
}

// Wat niet werkt:
// 1. een reeks van seperators (;;;;Test;), leidt niet tot matches (inmiddels wel)
// 2. een CR of LF tussen quotes gaat waarschijnlijk evenmin goed (niet getest).