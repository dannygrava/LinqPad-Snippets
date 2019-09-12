<Query Kind="Statements" />

var fizz = 3;
var buzz = 5;
Enumerable.Range(1, 100).
	OrderByDescending(x => x).
	Select (x => {
		if (x % (fizz*buzz)==0)
			return "fizzbuzz";
		if (x % fizz == 0)
			return "fizz";
		if (x % buzz == 0)
			return "buzz";
		return x.ToString();
	})
	.Dump();