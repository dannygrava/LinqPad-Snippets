<Query Kind="Statements" />

string input = "0123456789.123";
string pattern = @"^\d{1,10}\.\d{1,3}$";

Regex.IsMatch (input, pattern).Dump();