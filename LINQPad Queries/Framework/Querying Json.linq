<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

new Hyperlinq ("http://www.newtonsoft.com/json/help/html/QueryJson.htm").Dump("Read more here (Json.NET Documentation)");
string response = File.ReadAllText(@"C:\Users\dg\Documents\King\Bovk\Arco_BOVK.JSON");

JObject x = JObject.Parse(response);

x["Bovk"]["Bankomgeving"].Dump();

var y = x["Bovk"]["Test"];
y.Dump();

x.SelectToken("Bovk.Bankomgeving").Dump();

JObject o = JObject.Parse(@"{
  'Stores': [
    'Lambton Quay',
    'Willis Street'
  ],
  'Manufacturers': [
    {
      'Name': 'Acme Co',
      'Products': [
        {
          'Name': 'Anvil',
          'Price': 50,
		  'Description' : 'Short desc here'
        }
      ]
    },
    {
      'Name': 'Contoso',
      'Products': [
        {
          'Name': 'Elbow Grease',
          'Price': 99.95		  
        },
        {
          'Name': 'Headlight Fluid',
          'Price': 4
        }
      ]
    }
  ]
}");

// manufacturer with the name 'Acme Co'
JToken acme = o.SelectToken("$.Manufacturers[?(@.Name == 'Acme Co')]");
JToken desc = acme.SelectToken("$.Products[0].Description"); 

acme.Dump();
desc.Dump();
// { "Name": "Acme Co", Products: [{ "Name": "Anvil", "Price": 50 }] }

// name of all products priced 50 and above
IEnumerable<JToken> pricyProducts = o.SelectTokens("$..Products[?(@.Price >= 50)].Name").Dump();

acme = o.SelectToken("$.Manufacturers[?(@.Name == 'Acme Co')]");
acme.Dump("acme");

// Anvil
// Elbow Grease

string json = @"{
  'channel': {
    'title': 'James Newton-King',
    'link': 'http://james.newtonking.com',
    'description': 'James Newton-King\'s blog.',
    'item': [
      {
        'title': 'Json.NET 1.3 + New license + Now on CodePlex',
        'description': 'Annoucing the release of Json.NET 1.3, the MIT license and the source on CodePlex',
        'link': 'http://james.newtonking.com/projects/json-net.aspx',
        'category': [
          'Json.NET',
          'CodePlex'
        ]
      },
      {
        'title': 'LINQ to JSON beta',
        'description': 'Annoucing LINQ to JSON',
        'link': 'http://james.newtonking.com/projects/json-net.aspx',
        'category': [
          'Json.NET',
          'LINQ'
        ]
      },
      {
        'title': null,
        'description': 'Annoucing LINQ to JSON',
        'link': 'http://james.newtonking.com/projects/json-net.aspx',
        'category': [
          'Json.NET',
          'LINQ'
        ]
      }
	  
    ]
  }
}";

JObject rss = JObject.Parse(json);

var postTitles =
    from p in rss["channel"]["item"]
    select (string)p["title"];

postTitles.Dump("Post titles");
//LINQ to JSON beta
//Json.NET 1.3 + New license + Now on CodePlex

var categories =
    from c in rss["channel"]["item"].Children()["category"].Values<string>()
    group c by c
    into g
    orderby g.Count() descending
    select new { Category = g.Key, Count = g.Count() };

foreach (var c in categories)
{
    (c.Category + " - Count: " + c.Count).Dump("Categories");
}
//Json.NET - Count: 2
//LINQ - Count: 1
//CodePlex - Count: 1