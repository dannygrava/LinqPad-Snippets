<Query Kind="Program" />

void Main()
{
	int[] a = new int[]{3,2,5,1,9,4};
	SelectionSort(a);
	//a.Dump();
		Tuple<string, int> [] tuples = new Tuple<string, int>[] {	    
	    new Tuple<string, int>("1015", 0),
		new Tuple<string, int>("1216", -2),
		new Tuple<string, int>("1116", 1),		
		new Tuple<string, int>("1115", 3),		
		new Tuple<string, int>("0913", -3),
		new Tuple<string, int>("1014", -2),
		new Tuple<string, int>("0914", 2),
		
	};	
	tuples.Dump("Origineel");	
	Tuple<string, int> [] nTuples = (Tuple<string, int> []) tuples.Clone();
  // Sorteert de lijst volledig
  //SelectionSort(nTuples, nTuples.Length); 
  // Sorteert de lijst deels, bijvoorbeeld wanneer je alleen geinteresseerd bent in de n-hoogste waardes
	SelectionSort(nTuples, 2);
	nTuples.Dump("Na afloop");		
}

static void SelectionSort(Tuple<string, int>[] a, int n)
{
	/* a[0] to a[n-1] is the array to sort */
	int i,j;
	int iMax;
//	int n = a.Length-1; /*   (could do j < n-1 because single element is also min element) */
	
	/* advance the position through the entire array */	
	for (j = 0; j < n; j++) {
		/* find the min element in the unsorted a[j .. n-1] */
	
		/* assume the max is the first element */
		iMax = j;
		/* test against elements after j to find the smallest */
		for ( i = j+1; i < a.Length; i++) 
		{
			/* if this element is more, then it is the new maximum */  
			if (a[i].Item2 > a[iMax].Item2) {
				/* found new maximum; remember its index */
				iMax = i;
			}
		}
	
		/* iMax is the index of the maximum element. Swap it with the current position */
		if ( iMax != j ) 
		{
			Tuple<string, int> temp = a[j];
			a[j] = a[iMax];
			a[iMax] = temp;		
		}
	}
}

static void SelectionSort(int[] a){
/* a[0] to a[n-1] is the array to sort */
int i,j;
int iMax;
int n = a.Length;

/* advance the position through the entire array */
/*   (could do j < n-1 because single element is also min element) */
for (j = 0; j < n-1; j++) {
	/* find the min element in the unsorted a[j .. n-1] */
 
	/* assume the max is the first element */
	iMax = j;
	/* test against elements after j to find the smallest */
	for ( i = j+1; i < n; i++) {
		/* if this element is less, then it is the new minimum */  
		if (a[i] > a[iMax]) {
			/* found new maximum; remember its index */
			iMax = i;
		}
	}
 
	/* iMax is the index of the minimum element. Swap it with the current position */
	if ( iMax != j ) {
		int temp = a[j];
		a[j] = a[iMax];
		a[iMax] = temp;		
	}
}
}