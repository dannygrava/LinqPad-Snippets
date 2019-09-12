<Query Kind="Program" />

// http://projecteuler.net/index.php?section=problems&id=49

void Main()
{	
	var primes = Enumerable.Range(1, 10000).AsParallel()
		.WithDegreeOfParallelism(8)
	  	.Where(getal=>isPrime(getal)).ToList();	  	
	primes.Count().Dump();
	
	var differentPrimes = primes.Where(p => string.Format("{0:D4}", p).GroupBy(c=>c).Count() ==4).ToList();
	
	var resultset = from a in differentPrimes.AsParallel()
		from b in differentPrimes
		from c in differentPrimes
		where a != b && b != c && a != c &&
			(b - c) == 3330 && 
			(a - b) == 3330
//		    (b - a) > 0 &&
//	   		(b - a) == (c - b)
			
		let strA = string.Format("{0:D4}", a)		
		let strB = string.Format("{0:D4}", b)		
		let strC = string.Format("{0:D4}", c)		
		
		where (strA.Union(strB).Union(strC)).Count() == 4	    
	 	select new {Oplossing=strA+strB+strC, strA, strB, strC, verschil=b-a, verschil2=c-b};		
	resultset.Dump();
}

private bool isPrime(int getal)
{
  for (int i=2; i < getal; i++)
  {
	 if (getal % i == 0)
	   return false;
  }
  return true;
}