<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/binary-basics/", "Problem of the day: Binary basics").Dump();

Func<int, int[], int> binary_basic = (num, nums) => 
{
	var bottom = 0;
	var top = nums.Length-1;
	
	if (top == bottom && num == nums[bottom])
		return bottom;
		
	while (bottom < top)
	{
		if (top - bottom == 1)
		{
			if (nums[bottom] == num)
				return bottom;
			if (nums[top] == num)
				return top;
			break;		
		}		
		var middle = (top+bottom)/2;
		//new {num, bottom, top, middle}.Dump("Debug: locals watch");
		if (num == nums[middle])
			return middle;
		if (num > nums[middle])
			bottom = middle;
		if (num < nums[middle])
			top = middle;
	}	
	return -1;
};

// Elegantere implementatie uit Delphi-tijd; merk op de +1 en -1 waardoor kans op hangen er niet meer is met elegantere code als resultaat.
Func<int, int[], int> binary_search = (num, nums) => 
{
	var lowerbound = 0;
	var upperbound = nums.Length-1;
	
	while (lowerbound <= upperbound)
	{
		var idx = (lowerbound + upperbound) / 2;
		if (nums[idx] == num)
			return idx;
		if (num > nums[idx])
			lowerbound = idx + 1;
		if (num < nums[idx])
			upperbound = idx - 1;
		
	}	
	return -1;
};

Random rand = new Random();

var numbers =  new int[] {1,2,4,6,7,8,9,10,11};
Enumerable.Range(0, 15).Select(x => new {x, idx1=binary_basic(x, numbers), idx2=binary_search(x, numbers)}).Dump("Show results");

//binary_basic(2, numbers).Dump();
//binary_basic(4, numbers).Dump();
//binary_basic(1, numbers).Dump();
//binary_basic(10, numbers).Dump();
//binary_basic(6, numbers).Dump();

var one_item_array =  new int[] {2};
binary_basic(2, one_item_array).Dump("Show results: single item array");
binary_search(2, one_item_array).Dump("Show results: single item array");

var empty_array =  new int[] {};
Enumerable.Range(0, 2).Select(x => new {x, idx1=binary_basic(x, empty_array), idx2=binary_search(x, empty_array)}).Dump("Show results: empty array");

var randomSample = Enumerable
	.Repeat(0, 10)
	.Select(x => rand.Next(50))
	.OrderBy(y=> y)
	.ToArray()
	.Dump()
	;
	
Enumerable
	.Range(-10, 70)
	.Select(x => new {IndexBB= binary_basic(x, randomSample), IndexBS=binary_search(x, randomSample)})
	.Dump()
	.Where(item => item.IndexBB != item.IndexBS)
	.Dump()
	;



