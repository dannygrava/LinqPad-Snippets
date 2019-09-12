<Query Kind="Statements" />

var UnixStartTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
var timeNumbers = new int [] {1302268216, 1303887009, 1303826307, 212182};
timeNumbers.Select (t => UnixStartTime.AddSeconds(t).ToLocalTime()).Dump();

(DateTime.Today - UnixStartTime).TotalSeconds.Dump("Unix Time Number now");
