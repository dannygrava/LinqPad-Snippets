<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/number-box/", "Problem of the day: Number Box").Dump();
//43 | 48 | 38
//21 | 25 | 17
//30 | 33 | 27
//24 | 26 | 22
//40 | 41 | 39
//
//
//+5
//+4
//+3
//+2
//+1

int[][] numbers = new int [][] {new int[]{43, 48, 38}, new int[]{21, 25, 17}, new int[]{30, 33, 27}, new int[]{24, 26, 22}, new int[]{40, 41, 38}};

numbers.Select (x => new {dif12= x[1]-x[0], dif23=x[2]-x[0]}).Dump();

//int [] test = new int [] {32,23,1,2,2};


