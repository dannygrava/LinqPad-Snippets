<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/int-overflow/", "Problem of the day: Int Overflow").Dump();
var num = 1;
try
{
	checked {
		while(true)
		{
			num *= 2;	
		}
	}	
}

catch (OverflowException)
{
	num.Dump("CHECKED and Caught");
	Convert.ToString(num, 2).Dump();
}

// alternatief gebruik van checked
while(true)
{
	num = checked(num*2);	
}






