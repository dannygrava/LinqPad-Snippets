<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

@"The callback method is invoked once after dueTime elapses, and thereafter each time the time interval specified by period elapses.

If dueTime is zero (0), the callback method is invoked immediately. If dueTime is Timeout.Infinite, the callback method is never invoked; the timer is disabled, but can be re-enabled by calling Change and specifying a positive value for dueTime.

If period is zero (0) or Timeout.Infinite, and dueTime is not Timeout.Infinite, the callback method is invoked once; the periodic behavior of the timer is disabled, but can be re-enabled by calling Change and specifying a positive value for period.

The Change method can be called from the TimerCallback delegate.".Dump("Remarks");
List<string> input = new List<string>();

Timer t = new Timer((o) => input.Dump($"{DateTime.Now}: Entered values"));
int dueTime = 2000; // 2 secs
int period = 0;

string value = "";
do 
{  
  value = Util.ReadLine();  
  input.Add(value);   
  t.Change(dueTime, period);  
}
while (!string.IsNullOrEmpty(value));


