<Query Kind="Program" />

void Main()
{	
	new Hyperlinq("http://www.problemotd.com/problem/pin-hack/", "Problem of the day: Pin Hack").Dump();
	var results = Enumerable
		.Repeat(0, 1000)
		.Select (x => _random.Next(10000).ToString("D4"))		
		.Select (pin => new {
			random = checkPin (pin, GeneratePinDigitRandom),
			lookup =  checkPin (pin, GeneratePinDigitLookup),
			bruteforce =  checkPin (pin, GeneratePinDigitBruteForce),
			bruteforceopt =  checkPin (pin, GeneratePinDigitBruteForceOptimized)
		}).ToList();
	
	results.Average(x => x.random.GuessCount).Dump();
	results.Average(x => x.lookup.GuessCount).Dump();
	results.Average(x => x.bruteforce.GuessCount).Dump();
	results.Average(x => x.bruteforceopt.GuessCount).Dump();
	
	results.Average(x => x.random.Milliseconds).Dump();
	results.Average(x => x.lookup.Milliseconds).Dump();
	results.Average(x => x.bruteforce.Milliseconds).Dump();
	results.Average(x => x.bruteforceopt.Milliseconds).Dump();
}

class Result
{
	public bool Failure;
	public int Milliseconds;
	public int GuessCount;
}

private static Result checkPin (string pincode, Func<IEnumerable<char>> pinGenerator) 
{
	var guess = new StringBuilder();
	var guessCount = 0;
	var start = DateTime.Now;
	
	//foreach (char c in GeneratePinDigitRandom())
	foreach (char c in pinGenerator())	
	{
		guess.Append(c);
		if (guess.Length > 4)
		{
			guessCount++;
			guess.Remove(0,1);
		}	
		
		if (guess.ToString() == pincode)
		{	
			//string.Format("Pin found in {0} ms and {1} guesses", , guessCount).Dump();
			return new Result {Milliseconds = (DateTime.Now - start).Milliseconds, GuessCount = guessCount};			
		}		
	}	
	return new Result{Failure = true};
}

private static Random _random = new Random();

private static IEnumerable<char> GeneratePinDigitRandom()
{	
	while (true)
	{
		yield return _random.Next(10).ToString()[0];
	}	
}

// alternatief 1: Start met random nummer en hou een lijst bij van mogelijkheden
private static IEnumerable<char> GeneratePinDigitLookup()
{
	// Let op: Dit stukje buiten de lus wordt maar een keer aangeroepen!
	//string[] _allPins = Enumerable.Range(0, 10000).Select(x => x.ToString("D4")).ToArray();
	bool[] _allPins = Enumerable.Range(0, 10000).Select(x => true).ToArray();
	string _guess = _random.Next(10000).ToString("D4");
	
	while (true)
	{	
		int startIndex = Int32.Parse(_guess.Substring(1,3)) * 10;
		string nextguess = null;

		for(int i=0; i < 10; i++)
		{
			var pin = _allPins[startIndex + i];
			if (pin)
			{
				nextguess = (startIndex + i).ToString("D4");		
				_allPins[startIndex + i] = false;
				break;
			}
		}			
		
		if (string.IsNullOrEmpty (nextguess))
			_guess = _guess.Substring(1,3) + _random.Next(10).ToString();
		else
			_guess = nextguess;		
		yield return _guess[3];		
	}
}	

private static string _AllNumbers;

private static IEnumerable<char> GeneratePinDigitBruteForce()
{
	if (string.IsNullOrEmpty(_AllNumbers))
	{
		StringBuilder sb = new StringBuilder();
		Enumerable.Range(0, 10000).Select(x => x.ToString("D4")).Aggregate(sb, (builder, s)  => builder.Append(s));	
		_AllNumbers = sb.ToString();
	}

	for (int i=0; i < _AllNumbers.Length; i++)
	{
		yield return _AllNumbers[i];
	}	
}

private static string _AllNumbersOpt;

private static IEnumerable<char> GeneratePinDigitBruteForceOptimized()
{	
	if (string.IsNullOrEmpty(_AllNumbersOpt))
	{
		StringBuilder sb = new StringBuilder();
		Enumerable
			.Range(0, 10000)
			.Select(x => x.ToString("D4"))
			.Aggregate(sb, (builder, s)  => builder.ToString().Contains(s)? sb : builder.Append(s))
			;	
		_AllNumbersOpt = sb.ToString();
	}

	for (int i=0; i < _AllNumbersOpt.Length; i++)
	{
		yield return _AllNumbersOpt[i];			
	}	
}
