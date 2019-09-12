<Query Kind="Program" />

void Main()
{
	// Poging tot oplossing van volgende puzzel:
	//FORTY
	//+ TEN
	//+ TEN
	//------
	//SIXTY
	
	//Solution:
	//29786
	//850
	//850
	//31486
	
	var oplossing = (
		from F in Enumerable.Range(0, 10)
		from O in Enumerable.Range(0, 10) where F != O 
		from R in Enumerable.Range(0, 10) where F != R && O != R
		from T in Enumerable.Range(0, 10) where F != T && O != T && R != T
		from Y in Enumerable.Range(0, 10) where F != Y && O != Y && R != Y && T != Y
		from E in Enumerable.Range(0, 10) where F != E && O != E && R != E && T != E && Y != E
		from N in Enumerable.Range(0, 10) where F != N && O != N && R != N && T != N && Y != N && E != N
		from S in Enumerable.Range(0, 10) where F != S && O != S && R != S && T != S && Y != S && E != S && N != S
		from I in Enumerable.Range(0, 10) where F != I && O != I && R != I && T != I && Y != I && E != I && N != I && S != I
		from X in Enumerable.Range(0, 10) where F != X && O != X && R != X && T != X && Y != X && E != X && N != X && S != X && I != X
		//let list = new List <int>() {F, O, R, T, Y, E, N, S, I, X} 
		//let list = new HashSet<int>(){F, O, R, T, Y, E, N, S, I, X} 
		where 
		//	(list.Distinct().Count() == 10) && // Dit is niet snel genoeg
		//	(list.Count() == 10) && // ook niet snel genoeg
			(Satisfies (F, O, R, T, Y, E, N, S, I, X))
		select new {F, O, R, T, Y, E, N, S, I, X}
	);
	oplossing.Dump();	
}

bool Satisfies (int F, int O, int R, int T, int Y, int E, int N, int S, int I, int X)
{
	return (F*10000 + O*1000 + R*100 + T*10 + Y) + 2*(100*T + 10*E + N) == (S*10000 + I*1000 + X*100 + T*10 + Y);
}