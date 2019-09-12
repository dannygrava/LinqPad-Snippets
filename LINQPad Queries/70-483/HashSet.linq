<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

HashSet<int> oddSet = new HashSet<int>();
HashSet<int> evenSet = new HashSet<int>();
for (int x = 1; x <= 10; x++)
{
  if (x % 2 == 0)
    evenSet.Add(x);
  else
    oddSet.Add(x);
}

evenSet.Dump();
oddSet.Dump();

oddSet.UnionWith(evenSet);
oddSet.Dump();
oddSet.ExceptWith(evenSet);
oddSet.Dump();
oddSet.Add(4);
oddSet.IntersectWith(evenSet);
oddSet.Dump();