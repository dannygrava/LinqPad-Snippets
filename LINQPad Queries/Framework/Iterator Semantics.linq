<Query Kind="Program" />

// Iterator semantics pp 132-133 C# in a Nutshell
void Main()
{
	foreach (int fib in Fibs(6))
		Console.Write(fib + " ");
		
	Console.Write("\n\n");
	foreach (string s in Foo(false))
		Console.Write(s + " ");
}

static IEnumerable<int> Fibs (int fibCount)
{
  	for (int i = 0, prevFib = 1, curFib = 1; i < fibCount; i++)
  	{
  		yield return prevFib;
		// NOOT DG: na eerste call stopt de code op bovenstaande yield
		// op de tweede call wordt de code hervat op de eerste regel waar de laatste yield plaats vondt
		// dus op i.c. de volgende regel.
		// Zie ook de Foo class
		int newFib = prevFib+curFib;
		prevFib = curFib;
		curFib = newFib;
	}
}

static IEnumerable<string> Foo(bool breakEarly)
{
	yield return "One";
	yield return "Two";
	if (breakEarly)
		yield break;
	yield return "Three";
}