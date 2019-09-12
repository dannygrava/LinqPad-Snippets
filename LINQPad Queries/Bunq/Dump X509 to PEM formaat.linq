<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Security.Cryptography.X509Certificates</Namespace>
</Query>

var store = new X509Store(StoreName.My, StoreLocation.LocalMachine); //StoreLocation.LocalMachine
store.Open(OpenFlags.ReadOnly);   
//var certificates = store.Certificates.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);
//var certificates = store.Certificates.Find(X509FindType.FindBySubjectName, "KIB-Client-Develop", false);
//var certificates = store.Certificates.Find(X509FindType.FindBySubjectName, "KIB-Client-Develop", false);
var certificates = store.Certificates;//.Find(X509FindType.FindByIssuerName, "Quadrant King Internet bankieren Clients", false);
//var certificates = store.Certificates.Find(X509FindType.FindByIssuerDistinguishedName, "Quadrant King Internet bankieren Clients", false);
//certificates.Dump();

X509Certificate2Collection resultaat = X509Certificate2UI.SelectFromCollection(certificates, "Selecteer een certificaat voor KIB", "Selecteer het certificaat voor ondertekening en verzending", X509SelectionFlag.SingleSelection);

X509Certificate2 certificate = resultaat.Cast<X509Certificate2>().FirstOrDefault();
if (certificate != null)
{
	certificate.GetNameInfo(X509NameType.SimpleName, false).Dump("CertificateName");
	certificate.Thumbprint.Dump("Thumbprint");
//	certificate.Dump();
	
	StringBuilder builder = new StringBuilder();            
	
	builder.AppendLine("-----BEGIN CERTIFICATE-----");
	builder.AppendLine(Convert.ToBase64String(certificate.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks));
	builder.AppendLine("-----END CERTIFICATE-----");
	
	builder.ToString().Dump();	
}


