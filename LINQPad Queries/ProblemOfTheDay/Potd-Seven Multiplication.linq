<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/seven-multiplication/", "Problem of the day: Seven Multiplication").Dump();
Func<int, int> Mult7 = (num) =>	(num << 3) - num;

Random r = new Random();

var tests = Enumerable.Repeat(0, 20).Select (x => r.Next(1000)).Where(x => Mult7(x) != 7*x);
if (tests.Any())
	":-(! Implementatie incorrect!".Dump();
else
	"Jeej! Implementatie correct!".Dump();

Mult7(-7).Dump();
//Mult7(3).Dump();
//Mult7(4).Dump();
//Mult7(5).Dump();
//Mult7(6).Dump();

