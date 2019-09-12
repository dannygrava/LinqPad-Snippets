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

new Hyperlinq(@"https://nl.wikipedia.org/wiki/Vermoeden_van_Collatz").Dump();
IEnumerable<BigInteger> f (BigInteger x)
{
  Debug.Assert(x > 0);  
  var steps = new List<BigInteger>();
  while (x != 1)
  {
    x =  x % 2 == 1? x * 3 + 1: x / 2;        
    steps.Add(x);
  }
  return steps;
}

f(871).Count().Dump();
f(27).Count().Dump();
f(837799).Count().Dump();
f(670617279).Count().Dump();
f(13131313131313).Count().Dump();
//try{
//  f(837799).Count().Dump();
//  f(670617279).Count().Dump();
//}