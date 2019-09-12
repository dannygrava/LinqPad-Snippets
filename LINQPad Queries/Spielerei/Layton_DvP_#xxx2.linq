<Query Kind="Statements" />

// Professor Layton - doos van Pandora
// Puzzel xxx2

var result = (
  from a1 in Enumerable.Range(1, 8)
  from a2 in Enumerable.Range(1, 8)
  from a3 in Enumerable.Range(1, 8)
  from b in Enumerable.Range(1, 8)
  from c1 in Enumerable.Range(1, 8)
  from c2 in Enumerable.Range(1, 8)
  from c3 in Enumerable.Range(1, 8)
  from c4 in Enumerable.Range(1, 8)  
  let
    hs = new HashSet<int>() {a1, a2, a3, b, c1, c2, c3, c4} 
  where
    hs.Count ==8 &&
	(a1*100 + a2*10 + a3) * (b) == (c1 * 1000 + c2 * 100 + c3 * 10 + c4)
  select
    new {a1, a2, a3, b, c1, c2, c3, c4}
	);
	
	result.Dump();