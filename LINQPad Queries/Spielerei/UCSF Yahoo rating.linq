<Query Kind="Statements" />

// USCF system used this for calculating ratings of individual games of correspondence chess, 
// Very likely it is being applied by Yahoo too:
// http://en.wikipedia.org/wiki/Chess_rating_systems#Linear_approximation

// Note: strange behaviour in system if difference too large, then negative points even after wins. 
// f.i wins = 1, losses=0, myRating=1550, opponentRating=1100 => newRating = -2
// Dit treedt op bij een verschil van groter dan 400.
const decimal K = 32; // bepaalt de mate van aanpassing
const decimal C =200; // verschil met 75% score
//K×(W-L)/2 plus K/(4C) 

int myRating = 1602;
int opponentRating = 1335;

decimal resultaat = .62m;

decimal winFactor = (resultaat * 2) - 1;
decimal factor1 = K*(winFactor)/2;
decimal factor2 = K/(4*C);
decimal factor3 = (opponentRating-myRating);
decimal newRating =  myRating + (factor1 + (factor2 * factor3));
decimal newOppRating = opponentRating + (-factor1 + (factor2 * -factor3));

factor1.Dump();
factor2.Dump();
factor3.Dump();
Math.Round(newRating).Dump();
Math.Round(newOppRating).Dump();

//20221