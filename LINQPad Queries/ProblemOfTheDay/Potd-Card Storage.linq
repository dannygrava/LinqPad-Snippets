<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/card-storage/", "Problem of the day: Card Storage").Dump();

Func<string, int> toBase10 = (bitstring) =>
{
	int result = 0;
	for (int i = 0; i < bitstring.Length; i++)
	{
		if (bitstring[i] == '1')
			result |= 1 << (bitstring.Length-i-1);			
	}	
	return result;	
};

Func<int, string> toCardHolding = (num) => 
{
	string [] cards = new string [] {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
	const int NUM_FACES = 13;
	string result = "";
	for (int i = NUM_FACES-1; i >= 0; i--)
	{
		if ((num & (1<<i)) != 0)
		{
			result += cards[i];				
		}
	}
	return result;
};

Convert.ToString(toBase10("0011000100001"), 2).Dump();
toBase10("0011000100001").Dump("QJ72 (Expected=1569)");
Convert.ToString(1569, 2).Dump();
toCardHolding(1569).Dump();

//Convert.ToString(0x1FFF, 2).Dump();

Enumerable
	.Range(0, 0x1FFF)
	.Select(x => new {x , base10=toBase10(Convert.ToString(x, 2)), hand = toCardHolding(x)})
	.Where (item => item.x != item.base10)
	.Dump()
	;