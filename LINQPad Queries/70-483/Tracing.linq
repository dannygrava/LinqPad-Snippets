<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

//LISTING 3-46 Using the TraceSource class
//LISTING 3-47 Configuring TraceListener.

// NOTE DG: Anders wordt er niets getraced
#define TRACE 
// Configure to output to the console
var consoleTracer = new ConsoleTraceListener();
consoleTracer.Name = "mainConsoleTracer";
consoleTracer.WriteLine("Dit is een test");

Stream outputFile = File.Create("tracefile.txt".FromLinqpadDataFolder());
TextWriterTraceListener textListener = new TextWriterTraceListener(outputFile);

TraceSource traceSource = new TraceSource("myTraceSource", SourceLevels.All);
traceSource.Listeners.Clear();
traceSource.Listeners.Add(consoleTracer);
traceSource.Listeners.Add(textListener);

traceSource.TraceInformation("Tracing application..");
traceSource.TraceEvent(TraceEventType.Critical, 0, "Critical trace");
traceSource.TraceData(TraceEventType.Information, 1, new object[] { "a", "b",  "c" });
traceSource.Flush();
traceSource.Close();