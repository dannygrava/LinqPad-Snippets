<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/thanksgiving/", "Problem of the day: Thanksgiving").Dump();
 
var phoneData = new string [] {
	"mom 4078239999",
	"dad 4079923456", 
	"sis 4078961324",
	"danny 06-42801623",
	"Fred 06-29502894", 
	"Beef 06-28765324",
	"Martin 06-22904376",
	"Vincent 06-10976829",
	"Ted 06-24200415",
};

string input = Util.ReadLine("Search in phonebook");

bool isNameSearch = false;
foreach (var c in input)
{
	isNameSearch = !Char.IsDigit(c);
	if (isNameSearch)
		break;
};

phoneData
	.Select(s => s.Split(' '))	
	.Select(a => new {name=a[0], phone=a[1]})
	.Where (x => isNameSearch ? string.Equals(input, x.name, StringComparison.OrdinalIgnoreCase): x.phone.Contains(input)) 
	.Dump();


