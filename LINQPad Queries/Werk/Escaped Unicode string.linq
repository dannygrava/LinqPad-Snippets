<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

string value = "123.БДЖФУЛП7";

StringBuilder sb = new StringBuilder();
foreach( char c in value ) {
    if( c > 127 ) {
        // This character is too big for ASCII
        string encodedValue = "\\u" + ((int) c).ToString( "x4" );
        sb.Append( encodedValue );
    }
    else {
        sb.Append( c );
    }
}
sb.ToString().Dump();