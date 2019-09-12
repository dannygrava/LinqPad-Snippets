<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

Queue<string> myQueue = new Queue<string>();
myQueue.Enqueue("Hello");
myQueue.Enqueue("World");
myQueue.Enqueue("From");
myQueue.Enqueue("A");
myQueue.Enqueue("Queue");
myQueue.Dump();
myQueue.Dequeue().Dump();
myQueue.Dump();