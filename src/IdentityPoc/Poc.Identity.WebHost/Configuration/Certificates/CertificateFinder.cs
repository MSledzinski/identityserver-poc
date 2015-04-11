namespace Poc.Identity.WebHost.Configuration.Certificates
{
    using System.Security.Cryptography.X509Certificates;

    using Poc.Identity.WebHost.Utilities;

    public static class CertificateFinder
    {
        private const string CertResourceName = "Poc.Identity.WebHost.Configuration.Certificates.idsrv3test.pfx";

        public static X509Certificate2 GetDefault()
        {
            using (var resourceStream = typeof(CertificateFinder).Assembly.GetManifestResourceStream(CertResourceName))
            {
                return new X509Certificate2(resourceStream.ToArray(), "idsrv3test");
            }
        }
    }
}