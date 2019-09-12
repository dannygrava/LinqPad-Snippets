<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
  <Namespace>System.Security</Namespace>
</Query>

// LISTING 3-27 Initializing a SecureString
// LISTING 3-28 Getting the value of a SecureString
using (SecureString securePwd = new SecureString())
{  
  "Enter password: ".Dump();
  string password = Util.ReadLine<string>();
  foreach(var c in password)
  {
    securePwd.AppendChar(c);        
  }
  securePwd.Dump("Contents securePwd");
  
  // LISTING 3-28 Getting the value of a SecureString
  // Convert back to something readable
  IntPtr unmanagedString = IntPtr.Zero;
  try
  {
    unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePwd);
    Marshal.PtrToStringUni(unmanagedString).Dump("SecureString teruggelezen");
  }
  finally
  {
    Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
  }
}