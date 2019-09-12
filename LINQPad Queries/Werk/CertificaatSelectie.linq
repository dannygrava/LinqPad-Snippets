<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>System.Security.Cryptography.X509Certificates</Namespace>
</Query>

/// Displays user interface dialogs that allow you to select and view X.509 certificates. 

var store = new X509Store(StoreName.My, StoreLocation.CurrentUser); //StoreLocation.LocalMachine
store.Open(OpenFlags.ReadOnly);   
var certificates = store.Certificates.Find(X509FindType.FindByIssuerName, "Quadrant King Internet bankieren Clients", false);
//certificates.Dump();

X509Certificate2Collection resultaat = X509Certificate2UI.SelectFromCollection(certificates, "Selecteer een certificaat voor KIB",
                    "Selecteer het certificaat voor ondertekening en verzending", X509SelectionFlag.SingleSelection);

X509Certificate2 certificate = resultaat.Cast<X509Certificate2>().FirstOrDefault();
if (certificate != null)
{
//	certificate.GetNameInfo(X509NameType.SimpleName, false).Dump("CertificateName");
//	certificate.Thumbprint.Dump("Thumbprint");
	certificate.Dump();
}