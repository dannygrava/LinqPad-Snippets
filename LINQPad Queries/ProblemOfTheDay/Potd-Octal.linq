<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/octal/", "Problem of the day: Octal").Dump();

Func<string, string> fromOctalToDecimal = (octal) => 
{
	int result = 0;
	int factor = 1;
	foreach(char c in octal.Reverse())
	{
		if (c < '0' || c > '8')
			return "Not a valid octal"; 				
		result += (c - '0') * factor;
		factor *= 8;
	}	
	return result.ToString();
};

fromOctalToDecimal ("31").Dump();
fromOctalToDecimal ("92").Dump();

//('d' < 'c').Dump();

//System.Convert.ToString(15, 8).Dump();

Enumerable
	.Range(0, 1000)
	.Select(x => new {x, octalStr = Convert.ToString(x, 8), octal=fromOctalToDecimal (Convert.ToString(x, 8))})
	.Dump()
	.Where(item => item.x.ToString() != item.octal)
	.Dump()
	;