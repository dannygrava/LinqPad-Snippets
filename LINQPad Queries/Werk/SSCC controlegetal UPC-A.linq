<Query Kind="Statements" />

new Hyperlinq("http://www.morovia.com/education/utility/upc-ean.asp", "UPC-EAN Calculator").Dump();
// UPC-A http://www.morovia.com/education/utility/upc-ean.asp

// NOTE 1 Dit zou eenvoudiger kunnen input is altijd 11 lang
// NOTE 2 zou verder vereenvoudigd kunnen worden 
Func<long, int> UPC_EAN_CheckDigit = (input) =>{
	
	int odd = 0;
	int even = 0;
	int count = 1;
	while (input > 0)
	{
		int temp = (int) (input % 10);
		input /= 10;
		if (count % 2 == 0)
			even += temp;
		else
			odd += temp;		
		count++;
	}
	
	if (count % 2 == 1)
	{
		var temp = odd;
		odd = even;
		even = temp;		
	}	
	odd *=3;	
	
	return 10 - (odd + even) % 10;	
};


Func<long, int> UPC_EAN_CheckDigit2 = (input) =>{
	var str = input.ToString().Reverse().ToArray();
	int sumOdds = 0;
	int sumEvens = 0;
	
	for (int i = 0; i < str.Length; i++)
	{
		if ((i+1)%2==0)
			sumEvens += str[i]-'0';
		else
			sumOdds += str[i]-'0';
	}
	sumOdds *=3;
	return 10 - (sumOdds + sumEvens) % 10;
};


UPC_EAN_CheckDigit(72641017543).Dump();
UPC_EAN_CheckDigit2(72641017543).Dump();