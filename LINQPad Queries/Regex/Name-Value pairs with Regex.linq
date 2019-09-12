<Query Kind="Statements" />

var input = @"Pakketsoort = King
Licentienummer = 00025000
Release = 5.50a
Versie = Logistiek
Gebruikers = 5
Opties = Alle,ASP€
Checksum = 8F95D9870A77DFBA7E7AAAB7B7E84F69E438E78A";

var pattern = @"^(?'name'[^=]+) = (?'value'.+)$";

Regex
	.Matches(input, pattern, RegexOptions.Multiline)
	.Cast<Match>()
	.ToDictionary(m => m.Groups["name"].Value, m => m.Groups["value"].Value)
	.Dump()
	;