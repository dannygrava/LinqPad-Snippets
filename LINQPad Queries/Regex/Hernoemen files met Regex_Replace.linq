<Query Kind="Statements" />

// Mp3 files hernoemen met behulp van Regex

//var pattern = @"(\d{4}\ -\ )";
var pattern = @"(yamashita-[\ ]?)";
//var pattern = @"(Yamashita[\ ]-?[\ ]?)";
const string path = @"D:\Users\dg\Music\Guitarra clasica\Popular\Kazuhito Yamashita plays The Beatles";

//Regex.Matches(input, pattern).Dump();
//Regex.Replace(input, pattern, "").Dump();


var mp3s = new DirectoryInfo (path)
	.GetFiles("*.mp3", SearchOption.AllDirectories)
	.Select(fi => new {old=fi.FullName, _new=System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Regex.Replace(fi.FullName, pattern, "").ToLower())})	
	.Where(fi => fi.old != fi._new)
	.Dump();
	
return;
foreach (var mp3 in mp3s)
{
	new FileInfo(mp3.old).MoveTo(mp3._new);	
}
	
//foreach (var fi in new DirectoryInfo (path).GetFiles("*.mp3", SearchOption.AllDirectories))
//{
//	if (Regex.IsMatch(fi.Name, pattern))
//	{
//		var newName = Regex.Replace(fi.FullName, pattern, "");
//		
//		string.Format("Old: {0}\nNew:{1}", fi.FullName, newName).Dump();
//		//fi.MoveTo(newName);
//	}
//}