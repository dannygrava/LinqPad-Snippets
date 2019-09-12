<Query Kind="Statements" />

// meer functional Round Robin scheme generator

int NUM_ENTRIES = 12;

var schedule = Enumerable
	.Range(0, NUM_ENTRIES)
	.Select (x =>  new {Name=x, Rounds=Enumerable
		.Range(0, NUM_ENTRIES)
		.Select(y => {
			if (y == x)
				return -1;
			int temp = y+x;
			if (y==NUM_ENTRIES-1)
				temp = x+x;
			if (temp < NUM_ENTRIES-1)
				return temp;
			return temp - (NUM_ENTRIES-1);			
			})})
	//.Dump()
	;
	
	// Checks
	schedule.Where(entry => entry.Rounds.GroupBy(e=>e).Count()!= NUM_ENTRIES).Dump("Dubbele indelingen");
	schedule.Where(entry => entry.Rounds.Where(r => r != -1).Min() != 0 || entry.Rounds.Max() != NUM_ENTRIES-2).Dump("Rondes van 0 t/m " + (NUM_ENTRIES-2));	
	schedule.Dump();
//	int [,] sched = new int [MAX_PARTICIPANTS+1, MAX_PARTICIPANTS+1];	
//
//	start = 0;
//	for (int i = 0; i<MAX_PARTICIPANTS + 1; i++)
//	{
//		for (int j = 0; j<MAX_PARTICIPANTS+1; j++)
//		{	
//		   int day = start + j;
//		   if (day >= MAX_PARTICIPANTS)
//		   {
//		   		day -= MAX_PARTICIPANTS;
//		   }		   
//		   // Als i==j dan is opponent MAX_PARTICIPANTS+1 oftewel de laatste
//		   if (i == j)
//		   {
//		   		sched[i, MAX_PARTICIPANTS] = day;
//		   		sched[i, j] = -1;								
//		   }
//		   else
//		   {
//		   		if (j < MAX_PARTICIPANTS)
//		   			sched[i, j] = day;
//			}	
//		}	
//		start++;
//	}