<Query Kind="Statements">
  <Reference Relative="..\..\..\..\Documents\Visual Studio 2010\Projects\Speeltuin\ChLib\bin\Debug\ChLib.dll">&lt;MyDocuments&gt;\Visual Studio 2010\Projects\Speeltuin\ChLib\bin\Debug\ChLib.dll</Reference>
  <Namespace>ChLib</Namespace>
  <Namespace>ChLib.Evaluators</Namespace>
</Query>

int colorToMove;
Position p = new Position();
int maxSearches = 1 << 20;

p.SetupBlack(5, 16, 23);
p.SetupWhite(13, 10, 32);
p.SetupKings(23);


p.ToString().Dump();
Reve64Evaluator.Evaluate(p, Search.WHITE, Search.LOSS, Search.WIN).Dump();
Reve64Evaluator.Evaluate(p, Search.BLACK, Search.LOSS, Search.WIN).Dump();
p.Flip();
p.ToString().Dump();
Reve64Evaluator.Evaluate(p, Search.WHITE, Search.LOSS, Search.WIN).Dump();

colorToMove = Search.WHITE;

return;

//p.SetupBlack(20, 16, 11, 10, 9, 6, 7, 5, 3, 2, 1);
//p.SetupWhite(21, 22, 23, 25, 26, 27, 29, 30, 31, 32, 28);
//p.Kings = 0;
//colorToMove = Search.BLACK;

//p.SetupBlack(20, 16, 11, 10, 9, 6, 7, 5, 2, 1);
//p.SetupWhite(21, 22, 23, 25, 26, 27, 30, 31, 32, 28);
//p.Kings = 0;
//colorToMove = Search.WHITE;

//p.SetupBlack(20, 16, 11, 10, 9, 6, 7, 5, 3);
//p.SetupWhite(21, 22, 23, 25, 26, 27, 29, 30, 31);
//p.Kings = 0;
//colorToMove = Search.BLACK;

//p.SetupBlack(2,7,9,10,11,4,13);
//p.SetupWhite(19,20,21,22,23,25,27);
//p.Kings = 0;
//colorToMove = Search.BLACK;

//p.SetupBlack(8, 10, 13, 19, 21);
//p.SetupWhite(16, 17, 22, 30, 32);
//p.SetupKings (13, 22);
//colorToMove = Search.BLACK;
//
//p.SetupBlack(8, 10, 13, 19, 21);
//p.SetupWhite(16, 17, 22, 30, 32);
//p.Kings = 0;
//colorToMove = Search.BLACK;

//p.SetupBlack(3,4,8);
//p.SetupWhite(16, 1);
//p.Kings = p.BlackPieces | p.WhitePieces;
//colorToMove = Search.BLACK;

//p.SetupBlack(3,4);
//p.SetupWhite(16, 1);
//p.Kings = p.BlackPieces | p.WhitePieces;
//colorToMove = Search.WHITE;

//p.SetupBlack(3,4);
//p.SetupWhite(1);
//p.Kings = p.BlackPieces | p.WhitePieces;
//colorToMove = Search.WHITE;


Search s = new Search();
var value = s.Start2(ref p, maxSearches, colorToMove);
//value.Dump("VAL");
//Utils.GetMove(p, s.BestMove).Dump("BMOV");
string.Format ("({0},{1},{2},{3})", p.BlackPieces.BitCount(), (p.BlackPieces & p.Kings).BitCount(), p.WhitePieces.BitCount(), (p.WhitePieces & p.Kings).BitCount()).Dump("COUNTS");
string.Format ("{0:N0}", s.TotalSearches).Dump("TOTS");
string.Format ("{0:N0}", s.NominalDepth).Dump("DEPTH");
string.Format ("{0}", s.SearchTime.TotalMilliseconds).Dump("TIME");

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