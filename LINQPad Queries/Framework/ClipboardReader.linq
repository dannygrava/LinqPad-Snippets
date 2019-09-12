<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\System.Windows.Presentation.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsFormsIntegration.dll</Reference>
  <Namespace>System.Windows</Namespace>
</Query>

 var data = Clipboard.GetDataObject();
 var datos = (string)data.GetData(DataFormats.Text);
 
 string[] stringRows = datos.Split(new Char[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
 string.Join(",", stringRows.Select(s => $"'{s}'")).Dump();
 //stringRows.Select (s => $"'{s}'")..Dump();
return;
var text = System.Windows.Clipboard.GetText().Dump("Clipboard contents");	
text.ToCharArray().Dump("CharArray");
Encoding.Unicode.GetBytes(text).Dump("Bytes");
byte[] bytes = new byte[text.Length * sizeof(char)];
System.Buffer.BlockCopy(text.ToCharArray(), 0, bytes, 0, bytes.Length);
bytes.Dump("Bytes 2");

return;
  
foreach (var v in Enum.GetValues(typeof(TextDataFormat)))
{
  "========".Dump();
  v.Dump();
  "========".Dump();
  Clipboard.GetText((TextDataFormat) v).Dump(); 
  
//  byte[] bytes = new byte[text.Length * sizeof(char)];
//  System.Buffer.BlockCopy(text.ToCharArray(), 0, bytes, 0, bytes.Length);
//  bytes.Dump("Bytes 2");
}