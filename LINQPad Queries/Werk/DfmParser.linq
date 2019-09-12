<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

void Main()
{
  var dc = new DumpContainer().Dump("Bezig met");
  foreach (string filename in Directory.GetFiles(@"D:\King_Trunk\Sources\App\div", "*.dfm", SearchOption.AllDirectories))
  { 
    //var fileName = @"D:\King_Trunk\Sources\RAP\Rdm\dmBackordersBinnenArtikelData.dfm"; 
    //var filename = @"D:\King_Trunk\Sources\App\Div\EigenRekening\fEigenRekeningBrowse.dfm";
    
    dc.Content = filename;    
    try
    {
      //filename.Dump(filename);
      var dfmParser = new DfmParser(filename); 
      dfmParser.ReadFile();   
//      //dfmParser.Nodes.Where (node => node.PropertyType == "unknown DG").Dump();
//      foreach(var n in dfmParser.Nodes.Where (node => node.PropertyName == "SQL"))
//      {
//        n.PropertyValue.Dump(n.Parent.PropertyName);      
//      }
      //dfmParser.Dump();
    }
    catch (Exception e)
    {
      filename.Dump("Raises exception");
      e.Dump("Exception");
    }
   
  }  
}

// Credits: RAFAEL ROMÃO, 15 AGOSTO 2009
// http://rafaelromao-archived.blogspot.nl/2009/08/parsing-delphi-dfm-file.html
// NOTE DG: Added parsing of multi-line strings and special characters (#13#10 etc).
// Not yet support added for serializing them into the dfm.
// Verschillende fixes
class DfmDataNode {
    public string PropertyName { get; set; }
    public string PropertyType { get; set; }
    public string PropertyClass { get; set; }
    public object PropertyValue { get; set; }
    public DfmDataNode Parent { get; set; }
    private string key = null;
    public string Key {
        get {
            if (key == null) {
                var s = PropertyName;
                var p = Parent;
                while (p != null) {
                    s = p.PropertyName + "." + s;
                    p = p.Parent;
                }
                key = s;
            }
            return key;
        }
    }
    private int? identLevel = null;
    public int IdentLevel {
        get {
            if (identLevel == null) {
                var r = 0;
                var p = Parent;
                while (p != null) {
                    r++;
                    p = p.Parent;
                }
                identLevel = r;
            }
            return identLevel.GetValueOrDefault();
        }
    }
    public override string ToString() {
        if (PropertyClass != null) {
            return PropertyType + "::" + PropertyClass;
        }
        return PropertyType;
    }
}

class DfmParser {
    public DfmParser(string fileName) {
        OnReadNode += delegate { };
        OnReadNodeCount += delegate { };
        this.FileName = fileName;
        this.Nodes = new List<DfmDataNode>();
    }
    public event EventHandler OnReadNode;
    public event EventHandler OnReadNodeCount;
    public string FileName { get; private set; }
    public List<DfmDataNode> Nodes { get; private set; }
    public long NodeCount { get; private set; }
    public void ReadFile() {
        if (!File.Exists(FileName)) return;
        using (var fileStream = new FileStream(FileName, FileMode.Open)) {
            using (var fileReader = new StreamReader(fileStream)) {
                var fileContent = new List<string>();
                while (!fileReader.EndOfStream) {
                    fileContent.Add(fileReader.ReadLine());
                }
                
                NodeCount = fileContent.Count(s => {
                    return s.Contains("object") || 
                           s.Contains("inherited") || 
                           s.Contains("item") || 
                           s.Contains(" = <");
                });
                NodeCount = fileContent.Count - NodeCount;
                OnReadNodeCount(this, EventArgs.Empty);

                var node = ReadObjectProperties(fileContent, null);
                ((List<DfmDataNode>)Nodes).Add(node);
            }
        }
    }
    private DfmDataNode ReadObjectProperties(IEnumerable<string> objectData, DfmDataNode parent) {
        var obj = "object";
        var inh = "inherited";
        var node = new DfmDataNode();
        var s = objectData.ElementAt(0);
        var k1 = s.IndexOf(obj) + obj.Length;
        node.PropertyType = obj;
        if (k1 == obj.Length-1) {
            k1 = s.IndexOf(inh) + inh.Length;
            node.PropertyType = inh;
        }
        var k2 = s.IndexOf(":");
        k2 = k2 > -1 ? k2 : k1+1;
        node.Parent = parent;
        node.PropertyName = s.Substring(k1+1, k2-k1-1).Trim();
        node.PropertyClass = s.Substring(k2+1, s.Length-k2-1).Trim();
        node.PropertyValue = node;

        var index = 0;
        var count = objectData.Count()-2;
        while (index < count) {
            index = ReadObjectProperty(index, objectData, node);
        }

        return node;
    }
    
    private static class ItemCounter {
        private static int current = 0;
        internal static int NextValue {
            get { return ++current; }
        }
        internal static void Reset() {
            current = 0;
        }
    }

    private DfmDataNode ReadItemsProperties(IEnumerable<string> objectData, DfmDataNode parent) {
        var node = new DfmDataNode();
        var s = objectData.ElementAt(0);
        var k = s.IndexOf("=");
        if (k > -1) {
            node.PropertyName = s.Substring(0, k-1).Trim();
            node.PropertyType = "items";
            ItemCounter.Reset();
        } else {
            node.PropertyName = "item" + ItemCounter.NextValue;
            node.PropertyType = "item";
        }
        node.PropertyValue = node;
        node.Parent = parent;

        var index = 0;
        var count = objectData.Count()-2;
        while (index < count) {
            index = ReadObjectProperty(index, objectData, node);
        }

        return node;
    }
    private int ReadObjectProperty(int startIndex, IEnumerable<string> objectData, DfmDataNode parent) {
        objectData = objectData.Skip(1).Take(objectData.Count()-2);
        var s = objectData.ElementAt(startIndex);
        var endIndex = startIndex;
        DfmDataNode node = null;
        if (s.EndsWith(" item")) {
            var ident = s.Substring(0, s.IndexOf("item"));
            endIndex = objectData.Skip(startIndex).ToList().IndexOf(ident+"end")+startIndex;
            if (endIndex == startIndex-1) {
                endIndex = objectData.Skip(startIndex).ToList().IndexOf(ident+"end>")+startIndex;
            }
            node = ReadItemsProperties(
                       objectData.Skip(startIndex)
                                 .Take(endIndex-startIndex+1),
                       parent
                   );
        } else if (s.IndexOf("inherited") > -1) {
            var ident = s.Substring(0, s.IndexOf("inherited"));
            endIndex = objectData.Skip(startIndex).ToList().IndexOf(ident+"end")+startIndex;
            node = ReadObjectProperties(
                       objectData.Skip(startIndex)
                                 .Take(endIndex-startIndex+1),
                       parent
                   );
        } else if (s.IndexOf("object") > -1) {
            var ident = s.Substring(0, s.IndexOf("object"));
            endIndex = objectData.Skip(startIndex).ToList().IndexOf(ident+"end")+startIndex;
            node = ReadObjectProperties(
                       objectData.Skip(startIndex)
                                 .Take(endIndex-startIndex+1),
                       parent
                   );
        } else {
            var k = s.IndexOf("=");
            node = new DfmDataNode();
            if (k-1 > 0)
              node.PropertyName = s.Substring(0, k-1).Trim();
            node.Parent = parent;
            var b0 = (!s.StartsWith("'")) && (!s.StartsWith("#"));
            var b1 = s.EndsWith("= (");
            var b2 = s.EndsWith("= {");
            var b3 = s.EndsWith("= <");

            if ((b0) && (b1 || b2 || b3)) {
                var endTerm1 = ")";
                var endTerm2 = "}";
                var endTerm3 = ">";
                string endTerm = endTerm1;
                if (b2) endTerm = endTerm2;
                if (b3) endTerm = endTerm3;
                for (int i = startIndex; i < objectData.Count(); i++)
                {
                    if (objectData.ElementAt(i).EndsWith(endTerm) && !objectData.ElementAt(i).EndsWith("<>"))
                    {
                        endIndex = i;
                        break;
                    }
                }
//                
//                endIndex = objectData.ToList().IndexOf(
//                               objectData.Skip(startIndex).First(o => {
//                                   return o.EndsWith(endTerm) && !o.EndsWith("<>");
//                               })
//                           );
                if (b1 || b2) {
                    node.PropertyValue = new List<string>();
                    if (b1) {
                        node.PropertyType = "record";
                        ((List<string>)node.PropertyValue).Add("(");
                    } else {
                        node.PropertyType = "binary";
                        ((List<string>)node.PropertyValue).Add("{");
                    }
                    ((List<string>)node.PropertyValue).AddRange(
                        objectData.Skip(startIndex+1).Take(endIndex-startIndex)
                    );
                } else {
                    node = ReadItemsProperties(
                        objectData.Skip(startIndex).Take(endIndex-startIndex+2),
                        parent
                    );
                }
            } else {    
                s = s.Substring(k+1, s.Length-k-1).Trim();
                node.PropertyValue = s;
                if (node.PropertyName == "SQL")
                {
                 //(propValue == "").Dump("Debug");
                  //propValue.ToCharArray().Dump("Debug");                  
                }                
                if (s.Contains("'") || s.Contains("#") || (s == "")) {
                    node.PropertyType = "string";                    
                    while (s.Trim() == "" || s.EndsWith("+"))
                    {                      
                      endIndex++;
                      s = objectData.ElementAt(endIndex).Trim();
                      node.PropertyValue += s;                          
                    }
                    node.PropertyValue = normalizeString ((string) node.PropertyValue);
                } else {                    
                    var numbers = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                    var v = node.PropertyValue.ToString();
                    if (v.Length > 0) 
                    {
                      var c = v.Substring(v.Length-1, 1);
                      if (numbers.Contains(c)) {
                          node.PropertyType = "number";
                      } else if ((node.PropertyType == "True") || (node.PropertyType == "False")) {
                          node.PropertyType = "boolean";
                      } else {
                          node.PropertyType = "unknown";
                      }                      
                    }
                }
            }
        }
        ((List<DfmDataNode>)Nodes).Add(node);
        OnReadNode(this, EventArgs.Empty);
        return endIndex+1;
    }
    public void SaveFile() {
        var fileContent = new List<string>();
        WriteNodes(fileContent, 0, null);
        File.WriteAllLines(FileName, fileContent.ToArray(), Encoding.Unicode);
    }
    private void WriteNodes(List<string> fileContent, int identLevel, DfmDataNode parentNode) {
        foreach (var node in Nodes) {
            if ((node.Parent == parentNode) && (node.IdentLevel == identLevel)) {
                WriteNode(node, fileContent, identLevel);
            }
        }
    }
    private void WriteNode(DfmDataNode node, List<string> fileContent, int identLevel) {
        var iLevel = identLevel * 2;
        var space = " ";
        var equal = " = ";
        var colon = ": ";
        var less = "<";
        var more = ">";
        var end = "end";
        var ident = String.Empty;
        for (int i = 0; i < iLevel; i++) {
            ident += space;
        }
        switch (node.PropertyType) {
            case "boolean":
            case "string": 
            case "number":
            case "unknown":
                fileContent.Add(ident + node.PropertyName + equal + node.PropertyValue);
                break;
            case "record":
            case "binary":
                var list = node.PropertyValue as List<string>;
                fileContent.Add(ident + node.PropertyName + equal + list[0]);
                list.Skip(1).All(
                    s => {
                        fileContent.Add(s);
                        return true;
                    }
                );
                break;
            case "inherited":
            case "object":
                fileContent.Add(ident + node.PropertyType + space + node.PropertyName + colon + node.PropertyClass);
                WriteNodes(fileContent, ++identLevel, node);
                fileContent.Add(ident + end);
                break;
            case "items":
                fileContent.Add(ident + node.PropertyName + equal + less);
                WriteNodes(fileContent, ++identLevel, node);
                fileContent[fileContent.Count-1] = fileContent[fileContent.Count-1] + more;
                break;
            case "item":
                fileContent.Add(ident + node.PropertyType);
                WriteNodes(fileContent, ++identLevel, node);
                fileContent.Add(ident + end);
                break;
        }
    }
    
    private string normalizeString (string dfmInput)
    {
      bool inQuotes = false; 
      bool inEscapeChar = false;
      string asciicode = "";
      StringBuilder sb = new StringBuilder();
      
      Func<string, char> CreateAscii = (s) => {
        try
        {
          return (char) int.Parse(s);
        }
        catch (Exception)
        {
          asciicode.Dump("ERROR");
          return '?';
        }
          
      };
      
      foreach (char c in dfmInput)
      {
        if (c == '\'')
        {   
          inQuotes = !inQuotes;
          if (inEscapeChar)
          {
            sb.Append(CreateAscii(asciicode));
            asciicode = "";
            inEscapeChar = false;
          }    
          
        }
        else if (c == '#')
        {
          if (inQuotes)
          {
            sb.Append(c);
          }
          else
          {          
            if (inEscapeChar)
            {
              sb.Append(CreateAscii(asciicode));
              asciicode = "";
              inEscapeChar = false;    
            }
            inEscapeChar = true;   
          }
        }
        else
        {
          if (inEscapeChar)
          {
            if (Char.IsDigit(c))      
              asciicode += c;  
            else
            {
              sb.Append(CreateAscii(asciicode));
              asciicode = "";
              inEscapeChar = false;    
            }      
          }
          if (inQuotes)    
            sb.Append(c);
        }
      }
      return sb.ToString();    
    }
}