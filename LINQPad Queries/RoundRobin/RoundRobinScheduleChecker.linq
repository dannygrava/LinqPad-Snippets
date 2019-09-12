<Query Kind="Statements" />

int MAX_PARTICIPANTS = 11;
	Random rand = new Random();
	int seed = rand.Next(MAX_PARTICIPANTS);		
	
	for (int k = 0; k < MAX_PARTICIPANTS; k++)
	{
		seed = k;
		// Build a schedule		
		int [,] sched = new int [MAX_PARTICIPANTS+1, MAX_PARTICIPANTS+1];	
		for (int i = 0; i<MAX_PARTICIPANTS + 1; i++)
		{
			for (int j = i; j<MAX_PARTICIPANTS+1; j++)
			{	
				int day = (i + j + seed) % MAX_PARTICIPANTS;		   		   
				sched[i, j] = day;							
				sched[j,i] = sched[i,j];
			}
			// Als i==j dan is opponent MAX_PARTICIPANTS+1 oftewel de laatste
			sched[i, MAX_PARTICIPANTS] = sched [i,i];
			sched[MAX_PARTICIPANTS, i] = sched [i,i];
		    sched[i, i] = -1;						
		}
		
		sched.Dump();
		// checks 
		
		// Check tussen 0 en MAX_PARTICIPANTS of -1
		var errors = false;
		for (int i = 0; i < MAX_PARTICIPANTS+1; i++)
		{			
			for (int j = 0; j < MAX_PARTICIPANTS+1; j++)
			{
				if (!(sched [i,j] == -1 || sched [i,j] >= 0 && sched[i,j] <= MAX_PARTICIPANTS))
				{
					errors = true;
					string.Format("Value out of range: ({0}, {1})={2}", i,j, sched [i,j]).Dump();
				}
			}
		}
		
		// Check uniqueness
		for (int i = 0; i < MAX_PARTICIPANTS+1; i++)
		{		
			var rounds = new List<int>();
			for (int j = 0; j < MAX_PARTICIPANTS+1; j++)
			{
				rounds.Add (sched[i,j]);
			}
			if (rounds.GroupBy(x => x).Count() != MAX_PARTICIPANTS+1)
			{
				errors = true;
				string.Format("Seed {1}; Row {0} does not contain unique values", i, seed).Dump();
			}
		}

		
		// Check spiegeling
		for (int i = 0; i < MAX_PARTICIPANTS+1; i++)
		{
			for (int j = 0; j < MAX_PARTICIPANTS+1; j++)
			{
				if (sched [i,j] != sched [j,i])
				{
					errors = true;
					string.Format("Afwijking: ({0}, {1})={2} != ({1}, {0})={3}", i,j, sched [i,j], sched [j,i]).Dump();
				}
			}
		}	
		if (errors)
		{
			sched.Dump("Errors");
		}
		else
		{
			"No errors found!".Dump();
		}			
	}