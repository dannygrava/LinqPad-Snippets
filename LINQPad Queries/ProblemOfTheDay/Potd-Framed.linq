<Query Kind="Statements" />

 new Hyperlinq("http://www.problemotd.com/problem/framed/", "Problem of the day: Framed").Dump();
 var input = new string [] {"Hello", "World", "in", "a", "frame"};
 
 var maxLength = input.Max(s => s.Length);
 
 const int FRAME_WIDTH = 2;

 var sb = new StringBuilder();
 sb.AppendLine(new string ('*', FRAME_WIDTH + maxLength + FRAME_WIDTH));
 foreach (var s in input)
 {
 	//sb.AppendFormat("{0,2}{1,}{0,2}\n", "*", s);
 	sb.Append("* ");
 	sb.Append(s.PadRight(maxLength));
	sb.AppendLine(" *");
 }
 sb.AppendLine(new string ('*', maxLength + 4));
 
 //Util.RawHtml (string.Format("<pre style=\"font-family:Lucida Console\">{0}</pre>", sb.ToString())).Dump();
 sb.ToString().DumpMonospaced();