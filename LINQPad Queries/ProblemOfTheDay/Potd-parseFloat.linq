<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/parsefloat/", "Problem of the day: parseFloat").Dump();
Func<string, double> parseFloat = (value) => 
{
	double temp = 0;
	int indexLowDigit = value.IndexOf('.') >= 0 ?  value.IndexOf('.') : value.Length;
	for (int i = indexLowDigit-1 ; i >= 0; i--)
	{
		if (value[i] < '0' || value[i] > '9')
			break;
		temp += (value[i] - '0') * Math.Pow(10, (indexLowDigit-1 - i));
	}
	
	for (int i = indexLowDigit+1 ; i < value.Length; i++)
	{
		if (value[i] < '0' && value[i] > '9')
			break;
		temp += (value[i] - '0') * Math.Pow(10, -(i-indexLowDigit));
	}
	
	if (value.IndexOf('-') != -1)
		return -temp;
	return temp;
};

var testvalues = new string[] {"123", "123.456", "0.456", ".456", "-3", "-1239.02", "897-", "-.982"};

testvalues.Select(v => parseFloat(v)).Dump();
//value.IndexOf('.').Dump();
