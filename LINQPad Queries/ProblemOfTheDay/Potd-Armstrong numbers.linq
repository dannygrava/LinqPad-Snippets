<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/armstrong-numbers/", "Problem of the day: Armstrong numbers").Dump();

Func<int, bool> isArmstrongNumber = (num) => {
	var result = 0;
	foreach (char c in num.ToString())
	{
		result += (int) Math.Pow(int.Parse(c.ToString()), num.ToString().Length);
	}
	return result == num;
};

//isArmstrongNumber (153).Dump();
Enumerable
	.Range(10, 10000-10)
	.Select(x => new {number=x, isArmstrong=isArmstrongNumber(x)})
	.Where(y => y.isArmstrong)
	.Dump("All Armstrong numbers between 10 and 10000")
	;