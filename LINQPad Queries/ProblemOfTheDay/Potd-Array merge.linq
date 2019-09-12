<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/array-merge/", "Problem of the day: Array Merge").Dump();
Random r = new Random();

int[] array1 = Enumerable.Repeat(0, 5).Select(x => r.Next(100)).OrderBy(x=>x).ToArray();
int[] array2 = Enumerable.Repeat(0, 7).Select(x => r.Next(100)).OrderBy(x=>x).ToArray();

int[] myArray = new int[array1.Length + array2.Length];

int i = 0;
int j = 0;
while (i < array1.Length || j < array2.Length)
{
	if (j >= array2.Length || (i < array1.Length && array1[i] <= array2[j]))
	{
		myArray[i+j] = array1[i];				
		i++;
	}
	else
	{
		myArray[i+j] = array2[j];
		j++;	
	}
}

array1.OnDemand("Array1").Dump();
array2.OnDemand("Array2").Dump();
myArray.OnDemand("Merge result").Dump();

Enumerable.SequenceEqual(myArray, array1.Concat(array2).OrderBy(x=>x)).Dump("Is correct implementation?");
