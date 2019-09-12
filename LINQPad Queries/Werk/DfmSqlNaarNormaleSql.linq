<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

"Paste de query uit de Delphi source code met Alt+Shift+V (Paste as Escaped string)".Dump("Tip");

//string dfmInput = @"'SELECT OrdRelFunDocGID, DocSoort, DocOms, OrdRelOrkGID, ' +
//                            				 'OrdRelRelGID,OmsFunctie, DocContactPersoonDefault '+
//                                     'FROM tabDocument '+
//                                     'left outer join tabOrderRelatieVoorDocument on ( OrdRelDocSoort = DocSoort and OrdRelOrkGID=:OrkGID ) '+
//                                     'left outer join tabFunctieContact on (FnCode=DocFnCodeDefault)'"
//									 ;
string dfmInput = @"'select first 1/*#*/ from'#13#10'  tabInkoopfactuurKop'#13#10'inner join tabBoekja' +
      'ar on BjGId = (select BjGid from tabBoekjaar where :FactuurDatum' +
      ' between BjBeginDatum and BjEindDatum) and IfkFactuurDatum betwe' +
      'en BjBeginDatum and BjEindDatum'#13#10'where'#13#10'  IfkGid <> IsNull(:IfkG' +
      'id, -1) and'#13#10'  IfkFactuurNummer_lengte = Len(:FactuurNr) and'#13#10'  ' +
      'IfkFactuurNummer = :FactuurNr and'#13#10'  IfkNawGid = :NawGid'#13#10#13#10#13#10#13#10 +
      #13#10";

bool inQuotes = false; 
bool inEscapeChar = false;
string asciicode = "";
StringBuilder sb = new StringBuilder();

Func<string, char> CreateAscii = (s) => (char) int.Parse(s);

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

sb.ToString().Dump();