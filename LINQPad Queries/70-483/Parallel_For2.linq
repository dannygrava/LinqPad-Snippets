<Query Kind="Statements">
  <Namespace>System.Collections.Concurrent</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

new Hyperlinq("http://www.problemotd.com/problem/double-up/", "Problem of the day: Double Up").Dump();
//Func<int, int> shuffle = (num) => Int32.Parse(new string(num.ToString().Reverse().ToArray()));

Func<int, bool> isDoubleShuffle = (num) => num.ToString().OrderBy(s=>s).SequenceEqual((num*2).ToString().OrderBy(s=>s));
BlockingCollection<int> results = new BlockingCollection<int>();

const int MAX_VALUE = 12345678;
Stopwatch sw = new Stopwatch();

sw.Start();

Parallel.For(1, MAX_VALUE, x => {    
  if (isDoubleShuffle(x))
    results.Add(x);   
});

sw.Stop();

results.Dump( "Alle results", 0).Min().Dump("Alternatief 1 met BlockingCollection");
sw.Elapsed.Dump("Alternatief 1 execution time");

// Alternatief...Gegevens dat we alleen in de laagste geinteresseerd zijn

int result = int.MaxValue;
object l = new object();

sw.Reset();
sw.Start();
Parallel.For(1, MAX_VALUE, (x) => {    
  if (isDoubleShuffle(x))
  {
    lock(l) 
    {
      if (x < result)
      {
        result = x;
      }
    }
  }   
});
sw.Stop();
result.Dump("Alternatief 2: result and lock()");
sw.Elapsed.Dump("Alternatief 2 execution time");

sw.Reset();
sw.Start();
var loopResult = Parallel.For(1, MAX_VALUE, (x, state) => {    
  if (state.ShouldExitCurrentIteration) 
  {
    if (state.LowestBreakIteration < x)
      return;
  }  
  
  if (isDoubleShuffle(x))
  { 
    state.Break();
  }   
});
sw.Stop();

loopResult.Dump("ParallelLoopResult").LowestBreakIteration.Dump("Alternatief 3: Met early break");
sw.Elapsed.Dump("Execution time Alternatief 3");


