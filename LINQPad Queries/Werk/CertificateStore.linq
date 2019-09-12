<Query Kind="Statements">
  <Namespace>System.Security.Cryptography.X509Certificates</Namespace>
</Query>

X509Certificate clientCertificate;
X509Store store = null;
store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);

clientCertificate =
    store.Certificates.Find(X509FindType.FindByThumbprint, "601D244A1C15A5C5DF53504E754DCB011F515E75", false)
        .OfType<X509Certificate2>()
        .FirstOrDefault();

clientCertificate.Dump("KIB-Client-Develop ontwikkelcertificaat");         
store.Certificates.OfType<X509Certificate2>().Select(cert => new {cert.FriendlyName, cert.NotAfter, cert.Subject}).Dump("Alle certificaten");