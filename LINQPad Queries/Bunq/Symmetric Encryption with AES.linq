<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

// Symmetric encryption
// Symmetric encryption uses the same key for encryption en decryption.
// The .NET framework provides 4 symmetric algorithms

//
// NOTE DG: Niet genoeg entropie...beter om Rfc2898DeriveBytes te gebruiken
//byte [] key = Encoding.UTF8.GetBytes("MijnGeheimeWachtwoord").Take(16).ToArray();

//The default iteration count is 1000 so the two methods use the same iteration count.

//new Hyperlinq("https://msdn.microsoft.com/en-us/library/system.security.cryptography.symmetricalgorithm(v=vs.110).aspx", "SymmetricalAlgorithm Class").Dump();
//new Hyperlinq("https://msdn.microsoft.com/en-us/library/system.security.cryptography.aes(v=vs.110).aspx", "AES Class").Dump();
//new Hyperlinq("https://msdn.microsoft.com/en-us/library/system.security.cryptography.rfc2898derivebytes(v=vs.110).aspx", "Rfc2898DeriveBytes Class (aanbevolen methode om van een wachtwoord naar een sleutel met voldoende entropie te gaan)").Dump();

new [] {
	new Hyperlinq("https://msdn.microsoft.com/en-us/library/system.security.cryptography.symmetricalgorithm(v=vs.110).aspx", "SymmetricalAlgorithm Class"),
	new Hyperlinq("https://msdn.microsoft.com/en-us/library/system.security.cryptography.aes(v=vs.110).aspx", "AES Class"),
	new Hyperlinq("https://msdn.microsoft.com/en-us/library/system.security.cryptography.rfc2898derivebytes(v=vs.110).aspx", "Rfc2898DeriveBytes Class (aanbevolen methode om van een wachtwoord naar een sleutel met voldoende entropie te gaan)")
}.Dump("Read more");

byte [] key = {145, 12, 32, 245, 98, 132, 98, 214, 6, 77, 131, 44, 221, 3, 9, 50}; // Sleutel
string iv = null;
//byte [] iv  = {15, 122, 132, 5, 93, 198, 44, 31, 9, 39, 241, 49, 250, 188, 80, 7}; // IV = Intialization factor (niet geheim, als seed (zijn er verschillen?))
//string text = @"invite_id=302
//account_id=500";

var ticket = new {invite_id=302, created=DateTime.UtcNow, response_id=1, account_id =221, iban="NL62BUNQ010252063", test = iv};
string text = JsonConvert.SerializeObject(ticket).Dump();

byte [] data = Encoding.UTF8.GetBytes(text);

byte[] encryptedData; 

// Encrypting the stream
using (SymmetricAlgorithm algorithm = Aes.Create())
{
	algorithm.LegalKeySizes.Dump("Legal key-sizes");
	algorithm.GenerateIV();	
	algorithm.IV.Length.Dump("IV Size");
	(algorithm.BlockSize / 8).Dump("BlockSize div 8");
	Convert.ToBase64String(algorithm.Key.Dump("Generated Key byte array")).Dump("Generated key");
	string.Join(",", algorithm.Key).Dump();
	iv = Convert.ToBase64String(algorithm.IV);
	key.Dump();
	algorithm.Key = key;	
	using (ICryptoTransform encryptor = algorithm.CreateEncryptor ())
	using (MemoryStream f = new MemoryStream())
	{				
		Convert.ToBase64String(algorithm.IV).Dump("Generated IV (base64)");
		//algorithm.IV.Dump("Generated IV");
		using (Stream c = new CryptoStream(f, encryptor, CryptoStreamMode.Write))
			c.Write(data, 0, data.Length);
		encryptedData = f.ToArray();
		encryptedData.Length.Dump("Lengte encrypted data");
		string encryptedData64 = Convert.ToBase64String(encryptedData).Dump("Encrypted data");
		encryptedData64.Length.Dump("Length");
	}
}	
	
// Decrypting the stream
byte[] decryptedData;

using (SymmetricAlgorithm algorithm = Aes.Create())
{
	using (ICryptoTransform decryptor = algorithm.CreateDecryptor (key, Convert.FromBase64String(iv)))
	using (MemoryStream f = new MemoryStream(encryptedData))
	using (MemoryStream ms = new MemoryStream())
	{
		using (Stream c = new CryptoStream(f, decryptor, CryptoStreamMode.Read))
			c.CopyTo(ms);
		decryptedData = ms.ToArray();
	}
}

//decryptedData.Dump("Decrypted result");
string djson = Encoding.UTF8.GetString(decryptedData).Dump("Decrypted result");
var obj = JsonConvert.DeserializeObject(djson);
obj.Dump();