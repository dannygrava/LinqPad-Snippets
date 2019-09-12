<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	//LISTING 3-17 Use a symmetric encryption algorithm
	EncryptSomeText();
}

public static void EncryptSomeText()
{
	string original = "My secret data!";
	using (SymmetricAlgorithm symmetricAlgorithm = new AesManaged())
	{
		byte[] encrypted = Encrypt(symmetricAlgorithm, original);
		string roundtrip = Decrypt(symmetricAlgorithm, encrypted);
		// Displays: My secret data!
		$"Original: {original}".Dump();
		$"Round Trip: {roundtrip}".Dump();
	}
}

static byte[] Encrypt(SymmetricAlgorithm aesAlg, string plainText)
{
	ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
	using (MemoryStream msEncrypt = new MemoryStream())
	{
		using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
		{
			using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
			{
				swEncrypt.Write(plainText);
			}
			return msEncrypt.ToArray();
		}
	}
}

static string Decrypt(SymmetricAlgorithm aesAlg, byte[] cipherText)
{
	ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
	using (MemoryStream msDecrypt = new MemoryStream(cipherText))
	{
		using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
		{
			using (StreamReader srDecrypt = new StreamReader(csDecrypt))
			{
				return srDecrypt.ReadToEnd();
			}
		}
	}
}