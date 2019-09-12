<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

// Handige LinqPad functionaliteit uit de classes Util en Extensions

var dc = new DumpContainer().Dump ("DumpContainer: Watch me change");

var prog = new Util.ProgressBar ("Processing").Dump();
for (int i = 0; i <= 100; i++)
{
   	Thread.Sleep(10);
   	dc.Content = "I am now " + i;
	prog.Percent = i;
}

var values = new string[] {"Waarde 1", "Waarde 2", "Waarde 3"};
Util.WordRun(true, values).Dump("WordRun with gaps");
Util.WordRun(false, values).Dump("WordRun without gaps");

Util.Metatext("Wat is metatext?").Dump("Meta text");

"Dit is een test".Dump();
Util.Highlight("Dit is een test met highlight").Dump();
Util.HighlightIf<string>("Dit is een test met HighlightIf", (s) => s.Contains("HighlightIf")).Dump();

//Util.DisplayWebPage("This will be displayed in a web page");
Util.ToHtmlString("Naar html?").OnDemand("ToHtmlString").Dump("OnDemand");
var artikelen = new [] 
{
	new {Zoekcode="BD-R25GB", Omschrijving = "Fujifilm BD-R 25GB"}, 
	new {Zoekcode="BLUERAYSPELER", Omschrijving = "PHILIPS BLUE-RAY DISC PLAYER BDP7300"}, 
	new {Zoekcode="CDROM70080", Omschrijving = "CD-R, single-sided, 700 MB (80 min), 52x"}, 
};
Util.ToCsvString(artikelen).Dump("ToCsvString");
Util.VerticalRun(values).Dump("VerticalRun");

//new Hyperlinq (() => MessageBox.Show ("Hello, world!"), "Hyperlinq (click me)").Dump();
new Hyperlinq (() => 
	{
		var number = Util.ReadLine<int>("Enter a number"); 
		number.Dump("You entered:");
		string name = Util.ReadLine ("Your favorite color (Combobox with options)", "", new[] { "Red", "Yellow", "Green", "Blue" });		
	}, "Hyperlinq + ReadLine demo").Dump();



Util.Image(@"http://intranet.quadrant.local/wiki/skins/monobook/qdicons/LogoKING.jpg").Dump("Image");
Util.RawHtml (new XElement ("h1", "This is a big heading")).Dump("raw html 1");
Util.RawHtml ("<p>This is a paragraph</p>").Dump("raw html 2");

var doc = @"<data>
  <summary>
    <account curr_desc='USD' acct_nbr='123' net='1000.00' />
    <account curr_desc='USD' acct_nbr='456' net='2000.00' />
  </summary>
  <details>
    <accounts>
      <account acct_nbr=""123"" curr=""USD"">
        <activity color='False' settle_date='02 Jul 2010' amt='580.00' />
        <activity color='True' settle_date='09 Jul 2010' amt='420.00' />
      </account>
      <account acct_nbr=""456"" curr=""USD"">
        <activity color='True' settle_date='12 Dec 2010' amt='1500.00' />
        <activity color='True' settle_date='19 Dec 2010' amt='500.00' />
      </account>
    </accounts>
  </details>
</data>";

//doc.DumpFormatted<string>("DumpFormatted wit string");
XDocument.Parse(doc).DumpFormatted("DumpFormatted wit XDocument");
new XElement("message", "Hello world").DumpFormatted("DumpFormatted with XElement");
typeof(Util)
	.GetMethods()
	.Where(info => info.IsPublic)
	.GroupBy(info => info.Name)
	//.Select(g => new {g.Key, g})
	.Select(g => new {g.Key, details=g.Select(x => new {x, x.Name, parameters=x.GetParameters()})})
	.Dump("Members van class Util", 1);