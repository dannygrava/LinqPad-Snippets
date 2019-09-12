<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/red-white-blue/", "Problem of the day: Red, White, Blue").Dump();
//RWBR = 91
//BWWB = 50
//BBWR = 65
//RWWB = ?
var values = new int []{91, 50, 65};
//var strings = new string[] {};
values.Select(x=> new {x, bin=Convert.ToString(x, 2), hex=Convert.ToString(x, 16)}).Dump();

var strings = new string [] {"RWBR", "BWWB", "BBWR"};
strings
	.Select(value => new {value, nums=value.Select(c => c - 'A' + 1), sum=value.Select(c => c - 'A' + 1).Sum()})
	//.Dump()
	;
	
(
	from r in Enumerable.Range(0, 100) 
	from w in Enumerable.Range(0, 100)
	from b in Enumerable.Range(0, 100)
	let RWBR=r+w+b+r
	let BWWB=b+w+w+b
	let BBWR=b+b+w+r
	let RWWB=r+w+w+b
	where RWBR == 91 && BWWB==50 && BBWR == 65
	select new {r, w, b, RWBR, BWWB, BBWR, RWWB}
).Dump().First().RWWB.Dump("RWWB=");