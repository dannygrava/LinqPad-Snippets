<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.Formatters.Soap.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Runtime.Serialization.Formatters.Binary</Namespace>
  <Namespace>System.Runtime.Serialization.Formatters.Soap</Namespace>
</Query>

void Main()
{
  MemoryStream ms = new MemoryStream();
  IFormatter formatter = new SoapFormatter(); 
  //IFormatter formatter = new BinaryFormatter();      
    
  try
  {
    throw new MyException ("Dit is een test", "<Response van de Webservice>");
  }
  catch(Exception e)
  {
    e.Dump();
    "Exception!".Dump();    
    formatter.Serialize(ms, e);    
  } 
  
  ms.Position = 0;
  Exception ex = (Exception) formatter.Deserialize(ms);
  ex.Dump("Gedeserialiseerd!");
  (ex as MyException).Response.Dump("Custom property value:");
  
  ms.Position = 0;
  XDocument.Load(ms).Dump();
}

// Define other methods and classes here

[Serializable]
public class MyException : Exception, ISerializable
{
  public string Response {get; set;}
  
  public MyException(string message, string response):base(message)
  {  
      Response = response;
  }
  
  protected MyException(SerializationInfo info, StreamingContext context)
  {
      // Reset the property value using the GetValue method.
      Response= (string) info.GetValue("Response", typeof(string));
  }
  
  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    // Use the AddValue method to specify serialized values.
    info.AddValue("Response", Response, typeof(string));  
  }
  
  
}