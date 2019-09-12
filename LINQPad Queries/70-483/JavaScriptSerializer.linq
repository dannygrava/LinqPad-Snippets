<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Extensions.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Web.Script.Serialization</Namespace>
</Query>

// LISTING 3-13 Deserializing an object with the JavaScriptSerializer

string json = @"{""Bovk"":{""Bankomgeving"":""2"",""AlleOpdrachten"":[{""Bankrek"":{""RekNr"":""NL20INGB0001234567"",""Naam"":""Pizza Bakker Triestina"",""StraatHuisnr"":""Capelseweg 1"",""Plaats"":""Capelle ad IJssel"",""LandCode"":"""",""LandNaam"":""Nederland"",""Valuta"":""EUR"",""Bank"":{""BIC"":""INGBNL2A"",""Naam"":""ING Bank"",""StraatHuisnr"":"""",""Plaats"":"""",""LandCode"":""""}},""BTLcodes"":[{""Rubriek"":""KB"",""Waarde"":""1""},{""Rubriek"":""KC"",""Waarde"":""3""},{""Rubriek"":""SO"",""Waarde"":""0""},{""Rubriek"":""BI"",""Waarde"":""0""},{""Rubriek"":""VC"",""Waarde"":""2""}],""Opdrachten"":[{""E2E"":""F1547A9D7663674F8FA287D86548895E"",""Bankrek"":{""RekNr"":""NL48THSX6548463234"",""Naam"":""&Albert &  Heijn && co"",""StraatHuisnr"":"""",""Plaats"":""Testerdam"",""LandCode"":""NL"",""LandNaam"":""NEDERLAND"",""Valuta"":"""",""Bank"":{""BIC"":""THSXNL2ABBB"",""Naam"":""THSX bank"",""StraatHuisnr"":""Sjhdfjs"",""Plaats"":""Rotterdam"",""LandCode"":""NL""}},""OpdrachtDatum"":""2016-01-25"",""BetKenmerk"":"""",""Oms"":"""",""Valuta"":""PTO"",""Bedrag"":""9.00""}]}]}}";

var serializer = new JavaScriptSerializer();
var result = serializer.Deserialize<Dictionary<string, object>>(json);

result.Dump();