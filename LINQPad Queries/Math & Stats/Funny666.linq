<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Numerics.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Numerics</Namespace>
</Query>

// funny math problem from YouTube
var nums = Enumerable.Range(1, 666)
  .Select (x => new string('6', x))
  .Select<string, BigInteger> (s => BigInteger.Parse(s))  
  .Dump(0);

nums.Aggregate<BigInteger>((x, y) => x + y).Dump("Sum");

BigInteger result = 0;
foreach(BigInteger bi in nums)
{
  result += bi;
}
result.Dump();

