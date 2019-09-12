<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/large-primes/", "Problem of the day: Large primes").Dump();
// Todo to make it work for range Int32.MaxValue ...UInt32.MaxValue

// Sieve of Eratosthenes
//var maxvalue = 100000000;
uint maxvalue = UInt32.MaxValue;

BitArray isPrime = new BitArray(Int32.MaxValue);
BitArray isPrimeHigh = new BitArray(Int32.MaxValue);
for (int i=0; i< Int32.MaxValue; i++)
{
	isPrime.Set(i, true);
}

for (int i=0; i< Int32.MaxValue; i++)
{	
	isPrimeHigh.Set(i, true);	
}

isPrime.Set(0, false);
isPrime.Set(1, false);
bool isprime = true;

for (uint i = 2; i < Math.Sqrt(maxvalue); i++)
{
	try
	{
		isprime = maxvalue < (uint) Int32.MaxValue ? isPrime.Get((int) i) : isPrimeHigh.Get((int)(i-Int32.MaxValue));
	}
	catch(Exception)
	{
		i.Dump("Exceptie");
		((int)(i-Int32.MaxValue)).Dump("Exceptie");
		throw;
	}
	if (isprime)
	{
		for (uint j = 2*i; j < Math.Sqrt(maxvalue); j += i)
		{
			if (j < (uint) Int32.MaxValue)
				isPrime.Set((int) j, false);
			else
				isPrimeHigh.Set((int)(j-Int32.MaxValue), false);
		}
	}
}

// gaat ff om principe
for (int i=2; i<10000; i++)
{
	if (isPrime.Get(i))
		Console.WriteLine(i);
}

for (uint i=((uint) (Int32.MaxValue)) + 1; i < UInt32.MaxValue; i++)
{
	if (isPrimeHigh.Get((int)(i-Int32.MaxValue)))
		Console.WriteLine(i-Int32.MaxValue);
	return;
}

//var theprimes = new List<int>();
//for (int i=2; i<primes.Length; i++)
//{
//	if (primes[i])
//		theprimes.Add(i);
//}
//
//theprimes.Dump().Count().Dump();