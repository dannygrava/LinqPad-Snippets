<Query Kind="Statements">
  <Connection>
    <ID>f2f6e2b4-0f40-48df-b2c7-82584a77c393</ID>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Nutshell.mdf</AttachFileName>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
  </Connection>
</Query>

//int[] hours = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
// Zoek alle tijden waarbij de tijd drie of meer dezelfde cijfers op een rij bevat.
// Bijv.: 02:22, maar ook 11:10 etc.
var q = (
  from h in Enumerable.Range(1, 12) //hours
  from m in Enumerable.Range(0, 60) //minutes
  let digit1 = h / 10
  let digit2 = h % 10
  let digit3 = m / 10
  let digit4 = m % 10
  where 
    (digit1 == digit2 && digit2 == digit3) ||
	(digit2==digit3 && digit3 == digit4)
  select (string.Format ("{0:D2}:{1:D2}", h, m))
  ).ToList();

q.Dump("De " + q.Count() + " gevonden oplossingen");