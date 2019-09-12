<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Runtime.ExceptionServices</Namespace>
</Query>

// p.77 ExceptionDispatchInfo
ExceptionDispatchInfo possibleException = null;
try
{
string s = "rr";
int.Parse(s);
}
catch (FormatException ex)
{
possibleException = ExceptionDispatchInfo.Capture(ex);
}
if (possibleException != null)
{
possibleException.Throw();
}