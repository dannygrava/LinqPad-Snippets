<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async void Main()
{
  (string text, int value) result = await getIntValue();
  result.Dump("Result (Note that it is just syntactic sugar)");
  result.value.Dump("Enhanced syntax! (result.value)");
}

// Define other methods and classes here
async Task<(string, int)> getIntValue ()
{
  await Task.Delay(1000);
  int amountInCents = 50_000_99;
  return("Demo: decimal separator in literals and value tuples", amountInCents);
}

