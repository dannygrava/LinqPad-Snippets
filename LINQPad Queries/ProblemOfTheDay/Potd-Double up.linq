<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/double-up/", "Problem of the day: Double Up").Dump();
//Func<int, int> shuffle = (num) => Int32.Parse(new string(num.ToString().Reverse().ToArray()));

Func<int, bool> isDoubleShuffle = (num) => num.ToString().OrderBy(s=>s).SequenceEqual((num*2).ToString().OrderBy(s=>s));

Enumerable
	.Range(1, 1234567)
	.AsParallel()
	.Where(x => isDoubleShuffle(x))
//	.First()
	.OrderBy(x=>x)
	.Select(x=> new {x, d=2*x})
	.Dump()
	;