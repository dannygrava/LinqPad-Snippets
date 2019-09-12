<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/no-plus/", "Problem of the day: No plus").Dump();

Func<uint, uint, uint> Add = (x, y) => 
{
//	Convert.ToString(x, 2).Dump("x");
//	Convert.ToString(y, 2).Dump("y");
	
	uint result = 0;	
	uint carry = 0;
	for (int i=0; i<32; i++)
	{
		uint mask = (1u << i); 				
		result |= (carry ^ x ^ y) & mask;				
		carry = ((carry&x)|(carry&y)|(x&y)) << 1;
//		string.Format("Carry {0} temp result {1}", Convert.ToString(carry, 2), Convert.ToString(result, 2)).Dump();
	}
	return result;
};

// Stack overflow implementation: eleganter en efficienter
Func<uint, uint, uint> SOAdd = (a, b) => 
{
    uint carry = a & b;
    uint result = a ^ b;
    while(carry != 0)
    {
        uint shiftedcarry = carry << 1;
        carry = result & shiftedcarry;
        result ^= shiftedcarry;
    }
    return result;
};

Random r = new Random();

const int maxValue = 12345678;
Enumerable
	.Repeat(0, 100)
	.Select(dummy => new {x=(uint)r.Next(maxValue),y=(uint)r.Next(maxValue)})
	.Select(values => new {values, myresult=Add(values.x, values.y), SOresult=SOAdd(values.x, values.y)})
	.Where(item => item.myresult != item.values.x+item.values.y || item.myresult != item.SOresult)
	.Dump("Verschillen met standaard +operator");