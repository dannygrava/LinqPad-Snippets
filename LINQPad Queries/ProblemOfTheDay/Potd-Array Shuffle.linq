<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/Array-shuffle/", "Problem of the day: Array Shuffle").Dump();

int[] nums = Enumerable.Range(1, 20).ToArray();

Random r = new Random();

for (int i=0; i<nums.Length; i++)
{
	var newIndex = r.Next(nums.Length);
	var temp = nums[i];
	nums[i] = nums[newIndex];
	nums[newIndex] = temp;	
}

nums.Dump();