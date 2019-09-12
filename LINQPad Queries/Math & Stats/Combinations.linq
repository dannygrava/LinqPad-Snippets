<Query Kind="Statements">
  <Output>DataGrids</Output>
</Query>

var teams = new string[] {"Ajax", "Feijenoord", "PSV", "Fc Twente", "AZ", "Vitesse" };
teams
	.SelectMany((t, i) => teams.Skip(i+1).Select(x=> new {home=t, visitors=x}))		
	.Dump("Alle combinaties");
	