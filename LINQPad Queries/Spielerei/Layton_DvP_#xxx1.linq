<Query Kind="Statements" />

// Professor Layton - doos van Pandora
// Puzzel xxx
var result = (
  from x in Enumerable.Range(1, 500)
  where
    x % 2 == 1 &&
	x % 3 == 1 &&
	x % 4 > 1 &&
	x % 5 == 1 &&
	x % 6 == 1 &&
	x % 7 == 1
  select new {x, vierdeler = x % 4});

result.Dump();