<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

Task<double> x = Task.Run(() => Math.Pow(26 + 10, 5));
Task<double> y = Task.Run(() => Enumerable.Range(1, 100000000).Select (i => 1d /(i * (i+1))).Sum());
"Stap 1".Dump();
var results = await Task.WhenAll(x, y);
"Stap 2".Dump();
results.Dump();
"Stap 3".Dump();
