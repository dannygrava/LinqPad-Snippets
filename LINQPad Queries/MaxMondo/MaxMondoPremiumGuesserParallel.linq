<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var allValues = getAllValues();
	
	int totalValues = allValues.Count();
	var dc = new DumpContainer ("Processing").Dump();
	step = 0;
	
	IProgress<int> progress = new Progress<int> (i => dc.Content = $"Stap {step} van {totalValues} ({step * 100d / totalValues:F2}%)");
	
	allValues
		.Select(s => $"http://traffic.libsyn.com/maxmondo1it/mm-ii-prem-200-{s}.mp3")
		.AsParallel()
		// Forceert het opgegeven aantal tasks simultaan
		// Als dit niet opgegeven wordt dan worden 8 tasks gebruikt op mijn huidige 8 core 
		.WithDegreeOfParallelism(10)
		// NOTE DG
		// First() stopt niet in pre 4.5 werd deze sequentieel uitgevoerd in 4.5 niet, maar dat houdt niet in dat gestopt wordt na de eerste vondst
		.Any(x => downloadFile(x, progress))
		.Dump();	
}

string DOWNLOAD_FOLDER = @"Podcast\MaxmondoPremium\".FromDownloadFolder();

//object stepLocker = new object();
int step = 0;

private bool downloadFile(string podcastUri, IProgress<int> progress)
{
	using (WebClient client = new WebClient ())
	{
		try
		{
			// p. 665 C# 3.0 In a Nutshell 
			// One way to address the issues is to wrap the nonatomic operations in a lock statement.
			// Locking simulates atomicity if consistently applied.
			// The Interlocked class however provides an easier and faster way for such simple operations:
			
			Interlocked.Increment(ref step);
//			lock(stepLocker)
//			{
//				step++;
//			}
			progress.Report(step);			
			client.DownloadFile(podcastUri, Path.Combine(DOWNLOAD_FOLDER, Path.GetFileName(podcastUri)));
			return true;	
		}
		catch (Exception)
		{
			return false;
		}
	}
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
		.OrderBy(x => r.Next()) // Random order
		.OrderByDescending(s => s.Count(c => Char.IsLetter(c)) ==2) // Geef voorrang aan twee letters
		.OrderByDescending(s => s.Contains("5")) // Geef prio aan de 5 (gebaseerd op ervaring)
		.ToList();
}