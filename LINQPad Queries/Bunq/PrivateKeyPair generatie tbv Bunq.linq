<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

//Generate a public/private key pair.
RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
//Save the public key information to an RSAParameters structure.
RSAParameters rsaKeyInfo = rsa.ExportParameters(true);

//rsa.Dump();
//rsaKeyInfo.Dump();
var xmlstring = rsa.ToXmlString(true);
xmlstring.Dump("RSA.xml");

var xdoc = XDocument.Parse(xmlstring);
xdoc.Dump();
xdoc.Root.Element("P").Value.Dump();
Convert.ToBase64String(rsaKeyInfo.P).Dump();


byte[] data = {1, 2, 3, 4, 5, 6 };
byte[] encrypted = rsa.Encrypt(data, true);
byte[] decrypted = rsa.Decrypt(encrypted, true);

//encrypted.Dump();
decrypted.Dump();

// Nu gaan we een nieuwe rsa instantie aanmaken, vullen met de public key en dan encrypted onsleutelen