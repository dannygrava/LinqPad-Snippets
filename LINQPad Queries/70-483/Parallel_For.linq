<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

CancellationTokenSource cancellationSource = new CancellationTokenSource();
ParallelOptions options = new ParallelOptions();
options.CancellationToken = cancellationSource.Token;

ParallelLoopResult loopResult = Parallel.ForEach(
  Enumerable.Range(0, 10), 
  options,
  (i, loopState) =>
  {
      Console.WriteLine("Start Thread={0}, i={1}", Thread.CurrentThread.ManagedThreadId, i);

      // Simulate a cancellation of the loop when i=2
      if (i == 2)
      {
        //cancellationSource.Cancel();
        //throw new Exception("Dit is een test");        
        loopState.Stop();
      }
    
      // Simulates a long execution
      for (int j = 0; j < 10; j++)
      {
          Thread.Sleep(1 * 200);

          // check to see whether or not to continue
          if (loopState.ShouldExitCurrentIteration) return;
          if (loopState.IsStopped) return;
      }

      Console.WriteLine("Finish Thread={0}, i={1}", Thread.CurrentThread.ManagedThreadId, i);
  }
);
Console.WriteLine("DONE?");
if (loopResult.IsCompleted)
{
    Console.WriteLine("All iterations completed successfully. THIS WAS NOT EXPECTED.");
}

Console.WriteLine("DONE2?");
                
                