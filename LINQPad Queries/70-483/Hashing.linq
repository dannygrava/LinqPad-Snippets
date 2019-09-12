<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

//LISTING 3-23 Using SHA256Managed to calculate a hash code
UnicodeEncoding byteConverter = new UnicodeEncoding();
SHA256 sha256 = SHA256.Create();
string data = "A paragraph of text";
byte[] hashA = sha256.ComputeHash(byteConverter.GetBytes(data));

data = "A paragraph of changed text";
byte[] hashB = sha256.ComputeHash(byteConverter.GetBytes(data));
data = "A paragraph of text";
byte[] hashC = sha256.ComputeHash(byteConverter.GetBytes(data));
hashA.SequenceEqual(hashB).Dump("Is hash A equal to B"); // Displays: false
hashA.SequenceEqual(hashC).Dump("Is hash A equal to B"); // Displays: true

sha256.Dump();

// => Hashing prevents tampering
// => Encryption prevents reading
// Certificates => where Hashing and Encryption come together (+ Id?)
//A digital certificate is part of a Public Key Infrastructure (PKI). A PKI is a system of digital
//certificates, certificate authorities, and other registration authorities that authenticate and
//verify the validity of each involved party.
//A Certificate Authority (CA) is a third-party issuer of certificates that is considered trustworthy
//by all parties. The CA issues certificates, or certs, that contain a public key, a subject to
//which the certificate is issued, and the details of the CA.