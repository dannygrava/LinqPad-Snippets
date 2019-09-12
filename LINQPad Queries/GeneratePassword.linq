<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.dll</Reference>
  <Namespace>System.Web.Security</Namespace>
</Query>

/// NOTE DG: Veel gebruikt, maar nu obsolete sinds browsers zelf de mogelijkheid bieden om wachtwoorden te genereren.
int passwordLength = 10;
int numberOfNonAlphanumericCharacters = 1;

Enumerable.Repeat(0, 100)
	.Select (x => Membership.GeneratePassword(passwordLength, numberOfNonAlphanumericCharacters))
	.Last()
	.Dump();