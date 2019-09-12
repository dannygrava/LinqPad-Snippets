<Query Kind="Statements">
  <Reference Relative="..\..\..\..\Documents\Visual Studio 2010\Projects\Speeltuin\ChLib\bin\Debug\ChLib.dll">&lt;MyDocuments&gt;\Visual Studio 2010\Projects\Speeltuin\ChLib\bin\Debug\ChLib.dll</Reference>
  <Namespace>ChLib</Namespace>
  <Namespace>ChLib.Evaluators</Namespace>
</Query>

int colorToMove;
Position p = new Position();

// first position
p.SetupBlack(28, 12);
p.SetupWhite(27, 23);
p.SetupKings(28, 27, 23);

// bug position
//p.BlackPieces=0x80008000; p.WhitePieces=0x440000; p.Kings=0x80440000;                 
//unchecked {p.BlackPieces=(int) 0x80008000; p.WhitePieces=(int) 0x440000; p.Kings=(int) 0x80440000;}
//unchecked {p.BlackPieces=(int) 0x80080; p.WhitePieces= (int) 0x80040000;p.Kings= (int) 0x80040080;}
//unchecked {p.BlackPieces=0xAF1C; p.WhitePieces=(int)0xA5DA0000; p.Kings=0x0;}
//unchecked {p.BlackPieces=0xAF1C; p.WhitePieces=(int) 0xA5DA0000; p.Kings=0x0;}
unchecked {p.BlackPieces=0xAF1C; p.WhitePieces=(int)0xB4DA0000; p.Kings=0x0;}
//p.WhitePieces = p.WhitePieces & ~Position.Sq24 | Position.Sq19; 
//p.SetupBlack(32, 12);
//p.SetupWhite(19, 23);
//p.SetupKings(32, 19, 23);

unchecked {p.BlackPieces=0xFC88; p.WhitePieces=(int) 0x85F80000; p.Kings=0x0;}

colorToMove = Search.WHITE;
//int searchDepth = 32;
p.SetupBlack(5, 16, 23);
p.SetupWhite(13, 10, 32);
p.SetupKings(23);
colorToMove = Search.BLACK;

int numpositions = 1 << 23;

Search s = new Search();
s.Quiescense = Search.QMode.Full;
s.Evaluate = ChLib.Evaluators.Reve64Evaluator.Evaluate;
//var value = s.Start(ref p, searchDepth, colorToMove);
var value = s.Start2(ref p, numpositions, colorToMove);
value.Dump("VAL");
Utils.GetMove(p, s.BestMove).Dump("BMOV");
string.Format ("{0:N0}", s.TotalSearches).Dump("TOTS");
Util.OnDemand("STATS", () => Utils.GetSearchStatistics(s, p)).Dump("STATS");


//Search sr64 = new Search();
//sr64.Evaluate = Reve64Evaluator.Evaluate;
//var vsr64 = sr64.Start(ref p, 22, colorToMove);
//
//vsr64.Dump("VAL");
//Utils.GetMove(p, sr64.BestMove).Dump("BMOV");
//string.Format ("{0:N0}", sr64.TotalSearches).Dump("TOTS");

//FieldInfo fieldInfo = typeof(HashTable).GetField("_entries", BindingFlags.NonPublic | BindingFlags.Static);
//var entries = (HashEntry[]) fieldInfo.GetValue(null);
//entries.Where(q=>q.Depth ==4).Dump();
//s.Dump();
//Utils.GetSearchStatistics(s, p).Dump("STATS");
/////Search.WHITE.Dump();
//
//p.ToString().Dump();
//var fieldInfo2 = typeof (Search).GetField("_moves", BindingFlags.NonPublic|BindingFlags.Instance);
//var moves = (Position[][]) fieldInfo2.GetValue(s);
//for (int i =0; i<searchDepth; i++)
//{
//	moves[i].TakeWhile(x=> !x.IsEmpty())
//		.Select(x=>Utils.GetMove(p, x))
//		.Dump("Checking out");
//}
//moves.Select(m => m.Count(n=>!n.IsEmpty())).Max().Dump("MAX_MOVS");