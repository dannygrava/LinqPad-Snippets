<Query Kind="Statements" />

new Hyperlinq("http://www.problemotd.com/problem/coin-flip-guessing/", "Problem of the day: Coin Flip Guessing").Dump();
/// Alice and Bob are playing a game. They are teammates, so they will win or lose together. 
/// Before the game starts, they can talk to each other and agree on a strategy.
/// When the game starts, Alice and Bob go into separate soundproof rooms – they cannot communicate with each other in any way. 
/// They each flip a coin and note whether it came up Heads or Tails. 
/// (No funny business allowed – it has to be an honest coin flip and they have to tell the truth later about how it came out.) 
/// Now Alice writes down a guess as to the result of Bob’s coin flip; and Bob likewise writes down a guess as to Alice’s flip.
/// If either or both of the written-down guesses turns out to be correct, then Alice and Bob both win as a team. 
/// But if both written-down guesses are wrong, then they both lose.
/// Can you think of a strategy Alice and Bob can use that is guaranteed to win every time?
Random r = new Random();
var names = new string [] {"O", "X"};

Enumerable
	.Repeat(0, 20)
	.Select (x => new {RollA=r.Next(2), RollB=r.Next(2)})
	.Select(x => new {Alice = names[x.RollA], Bob = names[x.RollB], guessA = names[x.RollA], guessB = names[x.RollB==0?1:0]})	
	.Dump()
	.Where(x => !(x.Bob==x.guessA || x.Alice == x.guessB))
	.Dump()
	;
	
	
// (X, X) => (X, O)
// (X, O) => (X, X)
// (O, X) => (O, O)
// (O, O) => (O, X)