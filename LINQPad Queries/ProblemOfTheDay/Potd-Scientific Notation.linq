<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/scientific-notation/", "Problem of the day: Scientific Notation").Dump();

Func<int, string> toScientific = (input) => {
	var exp = (int) Math.Log(input, 10);
	var base_ =  input / Math.Pow(10, exp);
	return string.Format("{0} 10^{1}", base_, exp); 
};

Random r = new Random();

Enumerable
	.Repeat(0, 20)
	.Select(x => r.Next(1000000))
	.Select(x => new {x, notation=toScientific(x)})
	.Dump(); 





