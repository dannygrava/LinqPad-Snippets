<Query Kind="Statements" />

//string input = "1223334444555551";
//string pattern = @"(\d)\1{2,}";
string input = "1. 10-15 23-18 2. 11-16 18x11x22 8x15";
string pattern = @"(\d+[.])? ((\d\d?)(([-x])(\d\d?))+)+";
var matches = Regex.Matches (input, pattern);
// \1 is een terugverwijzing naar groep (\d) (\index means reference a previously captured group by index)

//matches.Dump();

//foreach (Match m in matches)
//  m.Groups.Dump();


foreach (Group g in matches)
  //g.Value.Dump();
  g.Dump();