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

//LISTING 3-18 Exporting a public key

RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
string publicKeyXML = rsa.ToXmlString(false);
string privateKeyXML = rsa.ToXmlString(true);
publicKeyXML.Dump("Public key");
privateKeyXML.Dump("Private key");

//LISTING 3-19 Using a public and private key to encrypt and decrypt data
UnicodeEncoding ByteConverter = new UnicodeEncoding();
byte[] dataToEncrypt = ByteConverter.GetBytes("My Secret Data!");
byte[] encryptedData;
using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
{
	RSA.FromXmlString(publicKeyXML);
	encryptedData = RSA.Encrypt(dataToEncrypt, false);
}
byte[] decryptedData;
using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
{
	RSA.FromXmlString(privateKeyXML);
	decryptedData = RSA.Decrypt(encryptedData, false);
}
string decryptedString = ByteConverter.GetString(decryptedData);
decryptedString.Dump("Decrypted string"); // Displays: My Secret Data!