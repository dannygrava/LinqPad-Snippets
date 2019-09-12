<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

new Hyperlinq("http://stackoverflow.com/questions/206323/how-to-execute-command-line-in-c-get-std-out-results").Dump("Stackoverflow - How To: Execute command line in C#, get STD OUT results");
new Hyperlinq("https://msdn.microsoft.com/en-us/library/system.diagnostics.processstartinfo.redirectstandardoutput(v=vs.110).aspx").Dump("MSDN - ProcessStartInfo.RedirectStandardOutput Property");


var filename = @"D:\Users\dg\Documents\King\Bovk\King5\PAIN_TEST2903.xml";

Process process = new Process();
process.StartInfo.FileName = "certutil.exe";        
process.StartInfo.Arguments = $"-hashfile {filename} SHA256";
// How to output an cmd
//process.StartInfo.FileName = "cmd.exe";
//process.StartInfo.Arguments = @"/c DIR D:\Mail\ /s";

process.StartInfo.UseShellExecute = false;
process.StartInfo.RedirectStandardOutput = true;        
process.Start();

// Synchronously read the standard output of the spawned process. 
StreamReader reader = process.StandardOutput;
string output = reader.ReadToEnd();

// Write the redirected output to this application's window.
//Console.WriteLine(output);
output.Dump("Sha256 van een bestand met certutil");

process.WaitForExit();
process.Close();