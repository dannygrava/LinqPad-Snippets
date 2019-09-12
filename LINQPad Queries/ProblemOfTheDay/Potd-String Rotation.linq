<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/string-rotation/", "Problem of the day: String Rotation").Dump();

Func<string, string, bool> is_rotation = (str1, str2) =>
{
	if (str1.Length == str2.Length)
	{
		for(int i = 0; i<str1.Length; i++)
		{
			if (str1[i] != str2[str2.Length-i-1])
				return false;
		}
		return true;
	}
	return false;
};


is_rotation("123", "332").Dump();
is_rotation("123", "3325").Dump();
is_rotation("4123", "332").Dump();
is_rotation("1234567890", "0987654321").Dump();
is_rotation("1234", "321").Dump();
is_rotation("1234", "54321").Dump();
is_rotation("Fèvrier", "reirvèF").Dump();