<Query Kind="Program">
  <Reference Relative="..\..\..\..\Documents\Visual Studio 2010\Projects\Speeltuin\ChLib\bin\Debug\ChLib.dll">&lt;MyDocuments&gt;\Visual Studio 2010\Projects\Speeltuin\ChLib\bin\Debug\ChLib.dll</Reference>
  <Namespace>ChLib</Namespace>
  <Namespace>ChLib.Evaluators</Namespace>
</Query>

void Main()
{
	Search searcher = new Search();
	searcher.Evaluate = ICheckersEvaluator.Evaluate;
	
	int enginecolor = MoveGenerator.WHITE;
    int level = 19;
	
	 // first position
	Position current = new Position
                           {
                               BlackPieces = 0xFFF,
                               WhitePieces = 0xFFF00000.ToInt()
                           };
	
  PrintBoard(current);				
	var input = "";
	while (true)
	{
		input = Util.ReadLine<string>();		
		if (input == "q")
			break;			
		if (isValidInput(input, ref current, enginecolor))		
		{
			//current.ToString().Dump();
			var value = searcher.Start2(ref current, 1 << level, enginecolor);
			//Utils.GetSearchStatistics(searcher, current).Dump();		
			current = searcher.BestMove;
      PrintBoard(current);			
			searcher.StorePosition(current, enginecolor^1);
			value.Dump("Value");
		}
		else
			"Invalid move".Dump();
	}
}


// Define other methods and classes here

private void PrintBoard(Position p)
{
  string board = p.ToString().Replace(":::", ((char) 0x2B1B).ToString());
  board = board
    .Replace(" ", "")
//    .Replace('.', (char) 0x2B1C)
//    .Replace('w', (char) 0x25EF)
//    .Replace('W', (char) 0x235F)
//    .Replace('b', (char) 0x2B24)
//    .Replace('B', (char) 0x272A);    
    .Replace('w', (char) 0x26C0)
    .Replace('W', (char) 0x26C1)
    .Replace('b', (char) 0x26C2)
    .Replace('B', (char) 0x26C3);
  Util.WithStyle(board, "font-family:Courier;font-size:24px").Dump();
}

private bool isValidInput(string input, ref Position current, int enginecolor)
{
  string exp = @"(\d{1,2})[- x](\d{1,2})";
  var m = Regex.Match(input, exp);
  if (!m.Success)
      return false;

  int from;
  if (!int.TryParse(m.Groups[1].Value, out from))
      return false;
  int to;
  if (!int.TryParse(m.Groups[2].Value, out to))
      return false;

  return applyMove(ref current, enginecolor, from, to);
}

private bool applyMove(ref Position current, int enginecolor, int from, int to)
{
  Position[] moves = new Position[MoveGenerator.MAX_LEGAL_MOVES];
  var numMoves = MoveGenerator.GenerateMoves(ref current, moves, enginecolor ^ 1);
  for (int i = 0; i < numMoves; i++)
  {
      int a;
      if (enginecolor == MoveGenerator.WHITE)
      {
          a = (moves[i].BlackPieces ^ current.BlackPieces);
      }
      else
      {
          a = (moves[i].WhitePieces ^ current.WhitePieces);
      }

      int b = (1 << (from - 1)) | (1 << (to - 1));
      if (a == b)
      {
          //addToHistory(_current, moves[i], _enginecolor^1);
          current = moves[i];
          return true;
      }
  }
  return false;
}