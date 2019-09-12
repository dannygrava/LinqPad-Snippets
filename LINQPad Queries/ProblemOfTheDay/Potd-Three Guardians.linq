<Query Kind="Statements" />

Func<int, int, bool> isSolution = (coins, donation) =>
{
	coins *= 2; // cross first river
	coins -= donation; // first guardian
	coins *= 2; // cross second river
	coins -= donation; // second guardian
	coins *= 2; // cross third river
	coins -= donation; // third gaurdian
	// Are we broke now?
	return coins == 0;
};

isSolution(14, 16).Dump("Is 14, 16 a solution?");
isSolution(7, 8).Dump("Is 7, 8 a solution?");

Enumerable
	.Range(1, 1000)
	.SelectMany(x => Enumerable.Range(1, 1000), (coins, donation) => new {coins, donation})		
	.Where (item => isSolution(item.coins, item.donation))
	.Dump("Solutions")
	;