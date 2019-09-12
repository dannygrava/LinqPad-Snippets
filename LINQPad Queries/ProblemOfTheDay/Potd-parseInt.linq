<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/parseint/", "Problem of the day: parseInt").Dump();
Func<string, double> parseInt = (value) => 
{
	double temp = 0;
	int indexLowDigit = value.Length;
	for (int i = indexLowDigit-1 ; i >= 0; i--)
	{
		if (value[i] < '0' || value[i] > '9')
			break;
		temp += (value[i] - '0') * Math.Pow(10, (indexLowDigit-1 - i));
	}
	
	if (value.IndexOf('-') != -1)
		return -temp;
	return temp;
};

var testvalues = new string[] {"123", "456", "0456", "456", "-3", "-123902", "897-", "-982"};

testvalues.Select(v => parseInt(v)).Dump();
//value.IndexOf('.').Dump();
