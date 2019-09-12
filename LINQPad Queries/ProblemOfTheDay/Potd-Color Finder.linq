<Query Kind="Statements">
  <Namespace>System.Drawing</Namespace>
</Query>

new Hyperlinq("http://www.problemotd.com/problem/color-finder/", "Problem of the day: Color Finder").Dump();
var colors = new Dictionary<Color, int>();

//Bitmap bm = new Bitmap(@"D:\Users\dg\Pictures\SlapYourShit.jpg");
Bitmap bm = new Bitmap(@"D:\King_Trunk\Sources\GRAPHICS\King\King power on.ico");

for (int x = 0; x < bm.Width; x++)
{
	for (int y = 0; y < bm.Height; y++)
	{
		Color color = bm.GetPixel(x, y);
		if (colors.ContainsKey(color))
		{
			colors[color]++;
		}
		else
		{
			colors.Add(color, 1);
		}
	}
}

colors
	//.Where(c => c.Key.IsSystemColor)
	.OrderByDescending(x => x.Value)
	//.Take(10)
	.Dump()
	;