<Query Kind="Statements">
  <Output>DataGrids</Output>
</Query>

// Three guardians different approach
var coins = 0;

for (int donation = 1; donation < 1000; donation++)
//int donation = 16;
{
	coins = 0; //16
	var found = true;
	for (int i=0; i<3; i++)
	{
		coins += donation;
		//coins.Dump("Stap 1");
		if (coins % 2 == 0)
		{
			coins /= 2;			
			//coins.Dump("Stap 2");
		}	
		else
		{
			found = false;
			break;
			//coins.Dump("End point");
		}
		
		//coins.Dump("Stap 3");
	}
	
	if (found)
	{
		string.Format("coins {0}, donation {1}", coins, donation).Dump();
	}
}



"DONE".Dump();