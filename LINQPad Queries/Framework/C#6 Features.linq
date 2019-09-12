<Query Kind="Program">
  <Namespace>static System.Console</Namespace>
</Query>

void Main()
{
	var test = "world";
	
	$"Hello {test}!".Dump("C#6 feature: string interpolation");
	
	var doc = XDocument.Parse(@"<ElvisOperator>
			<Operator>?.</Operator>
			<Oms>x?.y – null conditional member access. Returns null if the left hand operand is null.</Oms>
		</ElvisOperator>");
		
	//doc.Dump();
	doc.Element("ElvisOperator").Element("Oms").Dump("What is the elvis operator?");
	doc?.Element("NietBestaandeTag")?.Element("DezeEvenmin").Dump("Test");
	
	nameof(doc).Dump("Sample use of the nameof-operator");
	
	AutoPropertyInitializer test1 = new AutoPropertyInitializer();
	test1.First.Dump("Auto Property Initializer");
	test1.Name.Dump("Expression Bodied Function");
	
	typeof(AutoPropertyInitializer).GetProperty(nameof(test1.Name)).Dump("GetProperty simplified with nameof");	
	Console.WriteLine("Static Import (Note: the call without namespace Console) Note the static key word");
}

// Define other methods and classes here

class AutoPropertyInitializer
{
    public string First {get;set;} = "John";
	public string Last {get;set;} = "Doe";	
    public string Name => First + " " + Last; 	
}