<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

using (var fs = new FileStream(@"King\Bovk\King5\PAIN_TEST2903.xml".FromDocumentsFolder(), FileMode.Open))
{	
	byte[] bytes = new byte[fs.Length];
	int numBytesToRead = (int)fs.Length;
	fs.Read(bytes, 0, numBytesToRead);

	SHA256 sha = SHA256.Create(); // This is one implementation of the abstract class SHA1.
	var result = sha.ComputeHash(bytes);
		
	StringBuilder sb = new StringBuilder();
	foreach (byte b in result)
		sb.Append(b.ToString("X2"));
	
	sb.ToString().Dump("SHA256 hash");
	sha.Dump("sha");
  CryptoConfig.MapNameToOID("SHA256").Dump();
	//Convert.ToBase64String(result).Dump("Base64");
	
//	D:\Users\dg\Documents\King\Bovk\King5>certutil -hashfile .\PAIN_TEST2903.xml SHA256
	//BitConverter.ToString(result).Dump("SHA1 hash");		
}