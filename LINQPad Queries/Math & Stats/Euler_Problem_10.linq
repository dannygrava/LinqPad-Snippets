<Query Kind="Program" />

// http://projecteuler.net/index.php?section=problems&id=10
void Main()
{
	// parallel duur ca. 3m17s, niet parallel ca. 12min.
	int target = 2000000;
	//int target = 1000;
	var primes = Enumerable.Range(2, target).AsParallel()
	  .Where(getal=>isPrime(getal)).ToList();

	  //primes.Count().Dump();
	Int64 som = 0;
	//primes.ToList().ForEach(getal => som += getal);
	//primes.ForAll(getal => som += getal);
	primes.Sum(x => (Int64)x).Dump();
	primes.Count().Dump();	
	som.Dump();  
}

// Define other methods and classes here

private bool isPrime(int getal)
{
  for (int i=2; i < getal; i++)
  {
     if (getal % i == 0)
	   return false;
  }
  return true;
}