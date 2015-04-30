namespace Poc.Identity.MvcClient.Tenant
{
    public static class HomeRealmDiscoveryService
    {
        public static string DiscoverTenantOfUrlRealm(string tenantUriDistinctorToken)
        {
            if (tenantUriDistinctorToken == "2671")
            {
                return "Tenant1";
            }

            return "Tenant2";
        }
    }
}