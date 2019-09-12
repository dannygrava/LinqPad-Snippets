<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.VisualBasic.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Microsoft.VisualBasic.FileIO</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

var myReader = new Microsoft.VisualBasic.FileIO.TextFieldParser(@"C:\Users\dg\Downloads\google.csv");
myReader.TextFieldType = FieldType.Delimited;
myReader.Delimiters = new  string[] {","};
//myReader.Dump();

// Loop through all of the fields in the file. 
// If any lines are corrupt, report an error and continue parsing. 
if (myReader.EndOfData)
  return;
string [] headers = myReader.ReadFields();  

while (!myReader.EndOfData)
{
  string[] currentRow = myReader.ReadFields();  
  currentRow.Select((x, i) => x != "" ? $"{i} {headers[i]}: {x}":"").Where(s => s != "").Dump();  
}

