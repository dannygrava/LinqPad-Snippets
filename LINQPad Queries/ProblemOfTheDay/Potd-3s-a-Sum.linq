<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/3s-sum/", "Problem of the day: 3s A Sum").Dump();
var numbers = new int [] {1,5,8,3,2,7,4};
int [] highNumbers = new int[3] {Int32.MinValue, Int32.MinValue, Int32.MinValue};
int highestSum = Int32.MinValue;

for (int i = 2; i < numbers.Length; i++)
{
	int sum = numbers[i] + numbers[i-1] + numbers[i-2];
	if (sum > highestSum)
	{
		highestSum = sum;
		for (int j = 0; j < 3; j++)
			highNumbers[j] = numbers[i-j];
		
	}
}

highNumbers.Dump("Solution: Consecutive numbers");
highestSum.Dump("Solution: Consecutive numbers");


// Variation: 3s a Sum non-consecutive order

highNumbers = new int[3] {Int32.MinValue, Int32.MinValue, Int32.MinValue};

foreach (int num in numbers)
{
	if (num > highNumbers[0])
	{
		highNumbers[2] = highNumbers[1];
		highNumbers[1] = highNumbers[0];
		highNumbers[0] = num;		
	}
	else if (num > highNumbers[1])
	{		
		highNumbers[2] = highNumbers[1];
		highNumbers[1] = num;		
	}
	else if (num > highNumbers[2])
	{
		highNumbers[2] = num;				
	}	
// Alternatief!	

//	if (num > highNumbers[2])
//	{		
//		highNumbers[2] = num;				
//	}
//	
//	if (num > highNumbers[1])
//	{		
//		highNumbers[2] = highNumbers[1];
//		highNumbers[1] = num;		
//	}
//	
//	if (num > highNumbers[0])
//	{		
//		highNumbers[1] = highNumbers[0];
//		highNumbers[0] = num;		
//	}	
}

highNumbers.Dump("Solution: Non-consecutive numbers");
highNumbers.Sum().Dump("Solution: Non-consecutive numbers");