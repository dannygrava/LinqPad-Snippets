<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

//DriveInfo.GetDrives().Dump();

string path = @"test.dat".FromLinqpadDataFolder();
using (StreamWriter streamWriter = File.CreateText(path))
{
  string myValue = "MyValue";
  streamWriter.Write(myValue);
}

using (FileStream fileStream = File.OpenRead(path))
{
  byte[] data = new byte[fileStream.Length];
  for (int index = 0; index < fileStream.Length; index++)
  {
    data[index] = (byte)fileStream.ReadByte();
  }
  Encoding.UTF8.GetString(data).Dump("Displays: MyValue");
}

using (StreamReader streamWriter = File.OpenText(path))
{
  streamWriter.ReadLine().Dump("Displays: MyValue");
}