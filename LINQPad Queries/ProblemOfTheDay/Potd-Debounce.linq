<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/debounce/", "Problem of the day: Debounce").Dump();

DateTime last = DateTime.Now;
Action<Action, int> debounce = (func, timelapse) => 
{
	if (DateTime.Now > last.AddMilliseconds(timelapse))
	{
		func();
		last = DateTime.Now;
	}
};


int count = 0;
while(true) 
{
	debounce(() => {count++; count.Dump();}, 250);
	if (count > 10)
		break;
}
