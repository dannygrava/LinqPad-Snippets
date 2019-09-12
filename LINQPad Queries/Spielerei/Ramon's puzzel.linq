<Query Kind="Statements" />

Func<int, int> factorial = null; 
	factorial = x => x==0?1:x*factorial(x-1);
Func<int, int> sqrt = x => (int) Math.Sqrt(x);

factorial(factorial(0) + factorial(0) + factorial(0)).Dump();
factorial(factorial(1) + factorial(1) + factorial(1)).Dump();
(2 + 2 + 2).Dump();
(3*3-3).Dump();
(sqrt(4)+sqrt(4)+sqrt(4)).Dump();
(5+5/5).Dump();
(6+6-6).Dump();
(7-7/7).Dump();
(8-sqrt(sqrt(8+8))).Dump();  
factorial(sqrt(8/8+8)).Dump();
(sqrt(9)*sqrt(9)-sqrt(9)).Dump();

