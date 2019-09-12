<Query Kind="Statements" />

Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Dump();
Environment.GetFolderPath(Environment.SpecialFolder.MyMusic).Dump();
Environment.GetFolderPath(Environment.SpecialFolder.MyVideos).Dump();
Environment.GetFolderPath(Environment.SpecialFolder.Personal).Dump();

//typeof(Environment.SpecialFolder).GetValues().Dump();

Enum.GetValues(typeof(Environment.SpecialFolder))
	.Cast<Environment.SpecialFolder>()
	.Select(v => new{folderEnum=v,folderPath=Environment.GetFolderPath(v)})
	.Dump()
	.Where(x => x.folderPath.StartsWith(@"D:\Users\dg"))
	.Dump()
	;

