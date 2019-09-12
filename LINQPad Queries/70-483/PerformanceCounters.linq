<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

//LISTING 3-54 Reading data from a performance counter
// NOTE Restart LinqPad met Admin rechten

void Main()
{

  if (CreatePerformanceCounters())
  {
    Console.WriteLine("Created performance counters");
    Console.WriteLine("Please restart application");
    //Console.ReadKey();
    return;
  }
  
  var totalOperationsCounter = new PerformanceCounter("MyCategory", "# operations executed", "", false);
  var operationsPerSecondCounter = new PerformanceCounter("MyCategory", "# operations / sec", "", false);
  totalOperationsCounter.Increment();
  operationsPerSecondCounter.Increment();
}

static bool CreatePerformanceCounters()
{
  if (!PerformanceCounterCategory.Exists("MyCategory"))
  {
    CounterCreationDataCollection counters = new CounterCreationDataCollection
    {
      new CounterCreationData("# operations executed", "Total number of operations executed", PerformanceCounterType.NumberOfItems32),
      new CounterCreationData("# operations / sec", "Number of operations executed per second", PerformanceCounterType.RateOfCountsPerSecond32)    
    };
    
    PerformanceCounterCategory.Create("MyCategory", "Sample category for Codeproject", PerformanceCounterCategoryType.SingleInstance, counters);
    return true;
  }
  return true;
}

// Define other methods and classes here