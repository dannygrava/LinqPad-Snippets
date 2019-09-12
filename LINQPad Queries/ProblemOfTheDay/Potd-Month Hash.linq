<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/month-hash/", "Problem of the day: Month hash").Dump();
//
var desc = @"
January - 7110  
February - 826   
March - 5313    
April - 541     
May - 3513      
June - 4610";   

//var months = new string[] {"January", "February", "March", "April", "May", "June"};
var months = System.Globalization.DateTimeFormatInfo.InvariantInfo.MonthNames.Take(12);
months.Select((s, i) => new {
	s, 
	hash=string.Format("{0}{1}{2}", s.Length, i+1, (s[0] - 'A' + 1))
	})
	.Dump("Hash: Length+MonthIndex+FirstLetterIndex");



