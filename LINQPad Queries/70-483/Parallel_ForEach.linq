<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Extensions.dll</Reference>
  <Namespace>System.Collections.Concurrent</Namespace>
  <Namespace>System.Diagnostics</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
  var sw = new Stopwatch();
   
  var values = getAllValues ();
  values.Count.Dump();
  
  hits = 0;
  episode = 23;
  sw.Start();
  var result = Parallel.ForEach(values, DoWork);
  sw.Stop();
  result.Dump("Result");  
//  values.ElementAt((int) result.LowestBreakIteration).Dump();
  searchresult.Dump(); 
  sw.Elapsed.Dump();
  hits.Dump();
  
  hits = 0;
  sw.Reset();
  sw.Start();
  getAllValues().AsParallel().AsUnordered().FirstOrDefault(x => FindValue(x) != null).Dump("As Parallel");
  sw.Stop();
  sw.Elapsed.Dump();
  hits.Dump();
}

private List<string> getAllValues ()
{
	StringBuilder sb = new StringBuilder();
	for (char c = '0'; c <= 'z'; c++)
	{
		if (Char.IsDigit(c) || (char.IsLetter(c) && char.IsLower(c)))
	 		sb.Append(c);	
	}
	var values = sb.ToString();


	Random r = new Random();
	return (
		from a in values
		from b in values
		from c in values
		select ""+a+b+c
		)
    //.OrderByDescending(s => s.Contains("8a"))
		.ToList();
}

string searchresult;
int hits;
int episode;

private void DoWork(string value, ParallelLoopState state)
{
  if (state.ShouldExitCurrentIteration)
    return;
  var filename = FindValue(value);
  if (filename != null)
  {    
    //searchresult = filename;
    Interlocked.Exchange(ref searchresult, filename);
    //state.Stop();
    state.Break();
  }  
}

private string FindValue(string value)
{
  Interlocked.Increment(ref hits);
  var filename = $@"Podcast\MaxmondoPremium\mm-ii-prem-{episode:D3}-{value}.mp3".FromDownloadFolder();
  if (File.Exists(filename))
  {    
    return filename;
  }    
  return null;
}
