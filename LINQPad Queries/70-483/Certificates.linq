<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>System.Security.Cryptography.X509Certificates</Namespace>
</Query>

// LISTING 3-24 Signing and verifying data with a certificate
void Main()
{  
  SignAndVerify();
}

public static void SignAndVerify()
{
  string textToSign = "Test paragraph";
  byte[] signature = Sign(textToSign);
  signature.Dump("signature");
  
  Verify(textToSign, signature).Dump("Verification result of signature");
  // Make the verification step fail
  signature[0] = 0;
  Verify(textToSign, signature).Dump("Verification result after tampering with signature");
}
static byte[] Sign(string text)
{
  X509Certificate2 cert = GetCertificate();
  var csp = (RSACryptoServiceProvider)cert.PrivateKey;
  byte[] hash = HashData(text);
  return csp.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
}
static bool Verify(string text, byte[] signature)
{
  X509Certificate2 cert = GetCertificate();
  var csp = (RSACryptoServiceProvider)cert.PublicKey.Key;
  byte[] hash = HashData(text);
  return csp.VerifyHash(hash,
  CryptoConfig.MapNameToOID("SHA1"), signature);
}

private static byte[] HashData(string text)
{
  HashAlgorithm hashAlgorithm = new SHA1Managed();
  UnicodeEncoding encoding = new UnicodeEncoding();
  byte[] data = encoding.GetBytes(text);
  byte[] hash = hashAlgorithm .ComputeHash(data);
  return hash;
}

private static X509Certificate2 GetCertificate()
{
  /// Displays user interface dialogs that allow you to select and view X.509 certificates.  
  var store = new X509Store(StoreName.My, StoreLocation.CurrentUser); //StoreLocation.LocalMachine
  store.Open(OpenFlags.ReadOnly);   
  var certificates = store.Certificates;
  X509Certificate2Collection resultaat = X509Certificate2UI.SelectFromCollection(certificates, "Selecteer een certificaat",
    "Selecteer het certificaat voor ondertekening en verzending", X509SelectionFlag.SingleSelection);
  
  X509Certificate2 certificate = resultaat.Cast<X509Certificate2>().FirstOrDefault();
  return certificate;
// NOTE DG: Onderstaande is originele code uit het boek

//  X509Store my = new X509Store("testCertStore", StoreLocation.CurrentUser);
//  my.Open(OpenFlags.ReadOnly);
//  var certificate = my.Certificates[0];
//  return certificate;
}