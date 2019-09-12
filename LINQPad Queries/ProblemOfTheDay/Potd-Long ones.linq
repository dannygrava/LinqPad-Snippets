<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/long-ones/", "Problem of the day: Long ones").Dump();

Func<char[], int> longest_one = (chars) =>
{
	var count = 0;
	var bestcount = 0;
	foreach (char c in chars)
	{
		if (c == '1')
			count +=1;
		else
		{
			if (count > bestcount)
				bestcount = count;
			count = 0;
		}		
	}
	return bestcount;
};

Func<char[], int> long_ones = (chars) =>
{
	var longest = 0;
	for (int i=0; i < chars.Length; i++)
	{
		if (chars[i] == '0')
		{
			chars[i] = '1';
			var series_length = longest_one(chars);
			if (series_length > longest)
				longest = series_length;
			string.Format("index {0} {1}: {2}", i, new string(chars), series_length);
			chars[i] = '0';
		}		
	}
	return longest;
};

// NOTE Dg: .NET Framework bevat de goed verborgen overload Convert.ToString(x, 2)
// Converts an int to the base of 2, 8, 10, 16 (second parameter)
Func<int, string> toBinaryString = (x) => 
{
	string result = "";
	while (x != 0)
	{
		result += (x & 1).ToString();
		x >>= 1;
	}
	return result;
};


string test = "0010111011101011100011101011000010010";


long_ones(test.ToCharArray()).Dump();

var rand = new Random();

Enumerable
	.Range(0, 100)
	.Select(x => rand.Next())
	.Select (x => new {s=toBinaryString(x), l=long_ones(toBinaryString(x).ToCharArray())})
	.Dump()
	.Max(item => item.l)
	.Dump()
	;