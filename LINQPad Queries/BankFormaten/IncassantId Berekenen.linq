<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Numerics.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Numerics</Namespace>
</Query>

new Hyperlinq("https://www.rabobank.com/nl/images/formaatbeschrijving_xml_sepa_direct_debit.pdf", "Bijlage B: Het bepalen van de Creditor Identifier (Incassant ID)").Dump("IncassantID berekenen");

//Bijvoorbeeld
//De NVB heeft KVK-nummer 40536533 met locatiecode 0000.
//De basis van de Creditor Identifier wordt dan: NL00ZZZ405365330000
//Voor de berekening van het controlegetal dienen de volgende stappen doorlopen te worden
//1. Plaats NL00 achteraan de basis en verwijder ZZZ 405365330000NL00
//2. Vervang NL door de numerieke waarde, waar A=10 en Z = 35, dus N=23 en L=21 405365330000232100
//3. Bereken de modulus 97 van het in stap 2 gemaakte getal 47
//4. Trek het bij stap 3 berekende getal af van 98, waardoor het controlegetal ontstaat: 51
//De Creditor Identifier van NVB wordt dus NL51ZZZ405365330000

string kvkNummer = "40536533";
const string locatieCode = "0000";
const string controleGetal = "00";
//string creditorId = $"NL00ZZZ{kvkNummer}{locatieCode}";
// stap 1 LET OP ZZZ wordt niet meegenomen zoals bij de Iban berekening
string basis = $"{kvkNummer}{locatieCode}NL{controleGetal}";
basis.Dump("Stap 1: Basis");
// stap 2 vervang NL door numeriekwaarde
basis = basis.Replace("NL", "2321");
basis.Dump("Stap 2: Vervang NL met numeriek");
// stap 3 Bereken mod97
var remainder = BigInteger.Parse(basis) % 97;
remainder.Dump("Stap 3: Mod 97");
// Stap 4 Bereken controle getal
var controlegetal = 98-remainder; 
controlegetal.Dump("Stap 4: controlegetal");
// Stap 5 Creditor Identifier
string creditorIdentifier = $"NL{controlegetal:D2}ZZZ{kvkNummer}{locatieCode}";
creditorIdentifier.Dump("Stap 5: Creditor Identifier");
