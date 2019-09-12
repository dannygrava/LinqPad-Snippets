<Query Kind="Statements">
  <Connection>
    <ID>f2f6e2b4-0f40-48df-b2c7-82584a77c393</ID>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Nutshell.mdf</AttachFileName>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
  </Connection>
</Query>

//Customers.Select(c => c).Dump("Alle customers!");
//MedicalArticles.Select(ma => ma).Dump("Alle medische artikelen");
//Products.Dump("Alle producten"); 
//Purchases.Dump("Alle aankopen");
//PurchaseItems.Dump("Alle aangekochte artikelen");
var joinedPurchases = Purchases.Join(
	Customers,
	purchase => purchase.CustomerID,
	customer => customer.ID,
	(p, c) => new {Purchase = p, Customer = c}
);

joinedPurchases.Dump();

joinedPurchases.
  GroupBy(x => x.Customer).
  Select (g => new {Customer = g.Key, SomVanAankopen = g.Sum(p => p.Purchase.Price)}).
  OrderBy(x => x.Customer.Name).
  Dump();
  
