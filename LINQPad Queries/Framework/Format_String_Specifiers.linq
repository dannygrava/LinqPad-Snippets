<Query Kind="Statements" />

// Format strings

string.Format("{0:G3}", 1.234).Dump("G3");
string.Format("{0:F2}", 1234.567).Dump("F2: Fixed number of decimals");
string.Format("{0:N2}", 1234.567).Dump("N2: Fixed number of decimals, with group seperator");
string.Format("{0:D5}", 12).Dump("D: Padding with zeroes");

string.Format("{0:.##}", 12.345).Dump("##: Custom formatting");
string.Format("{0:.00000}", 12.345).Dump("00: Custom formatting");

new int [] {15, -15, 0}
	.Select (i => string.Format("{0:+#;-#; 0}", i))
	.Dump("Section separator ;");

string.Format("Name={0, -20} CreditLimit={1, 15:C}", "Mary", 2000).Dump("Padding with string Format!!!");
string.Format("Name={0, -20} CreditLimit={1, 15:C}", "Anne", 50).Dump("Padding with string Format!!!");

//=====================================================================================================
//=======================================DateTime======================================================
//=====================================================================================================
#region DateTime
DateTime.Now.Dump();
DateTime.Now.ToString("yyyyMMddhhmmss").Dump("Test");
//string.Format ("Test {yyyyMMddhhmm}", DateTime.Now).Dump("Werkt ook met string.Format!");
string.Format ("Test {0:yyyyMMddhhmm}", DateTime.Now).Dump("Werkt ook met string.Format!");
var s = DateTime.Now.ToString("yyyyMMddhhmm");
var s2 = string.Format("{0:s}", DateTime.Now).Dump("Sortable date");
var s3 = string.Format("{0:s}", DateTime.Now, new System.Globalization.CultureInfo("en-GB")).Dump("Sortable date; Culture en-GB");
var s4 = DateTime.Now.ToString("MMMM yyyy").Dump("Long date notation");
var s5 = DateTime.Now.ToString("dddd dd MMMM yyyy").Dump("Long date notation with weekdays");
DateTime.Now.ToString("d").Dump("Short date");
DateTime.ParseExact(s, "yyyyMMddhhmm", System.Globalization.CultureInfo.CurrentCulture).Dump();
DateTime.ParseExact(s, "yyyyMMddhhmm", null).Dump();
DateTime.ParseExact(s2, "s", null).Dump("Parse sortable ISO 8601 date");
#endregion