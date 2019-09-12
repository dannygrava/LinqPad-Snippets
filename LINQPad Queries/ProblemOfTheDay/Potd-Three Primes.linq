<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/three-primes/", "Problem of the day: Three primes").Dump();
"Je kunt per definitie geen priemgetal krijgen door het vermenigvuldigen van drie losse priemen. Het resultaat dat ik vind is het gevolg van een integer overflow die toevallig priem is.".Dump("Let op!");

// Sieve of Eratosthenes
int maxvalue = 100000000;

BitArray isPrime = new BitArray(maxvalue);
for (int i=0; i< maxvalue; i++)
{
	isPrime.Set(i, true);
}

isPrime.Set(0, false);
isPrime.Set(1, false);
bool isprime = true;

for (int i = 2; i < maxvalue; i++)
{
	isprime = isPrime.Get(i);
	if (isprime)
	{
		for (int j = 2*i; j < maxvalue; j += i)
		{
			isPrime.Set(j, false);
		}
	}
}

//isPrime.Dump("Primes");

for (int i = 0; i < maxvalue; i++)
{
	if (isPrime.Get(i))
	{
		for (int j = i+1; j < maxvalue; j++)
		{
			if (isPrime.Get(j))
			{
				var thirdNumber = j+j-i;				
				var product = i*j*thirdNumber;
				if (thirdNumber < maxvalue && isPrime.Get(thirdNumber) && product < maxvalue && product > 0 && isPrime.Get(product))
				{
					string.Format("Solution found: {0}*{1}*{2}={3}", i, j, thirdNumber, product).Dump();
					string.Format("Diff 2,1: {0}", j-i).Dump();
					string.Format("Diff 3,2: {0}", thirdNumber-j).Dump();					
					return;
				}
			}				
		}
	}
}