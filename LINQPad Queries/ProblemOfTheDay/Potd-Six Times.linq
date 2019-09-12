<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/six-times/", "Problem of the day: Six Times").Dump();

Func<int, bool> isSixTimes = (num) => {
	int sumOfDigits = 0;
	foreach (char c in num.ToString())
	{
		sumOfDigits += int.Parse(c.ToString());
	}
	//sumOfDigits.Dump();
	return num == 6 * sumOfDigits;
};

isSixTimes(666);

Enumerable
	.Range(0, 10000000)
	.Where(x => isSixTimes(x))
	.Dump("Six times the sum of the digits")
	;