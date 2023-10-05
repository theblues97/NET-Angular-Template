﻿//using Dal.MultiTenancy;
//using Microsoft.Extensions.Configuration;

//namespace Core.MultiTenancy
//{

//    public interface ITenantRegistry
//    {
//        public Tenant[] GetTenants();
//    }


//    public class TenantRegistry : ITenantRegistry
//    {
//        private readonly TenantOptions _tenantOptions;
//        public TenantRegistry(IConfiguration configuration)
//        {
//            _tenantOptions = configuration.GetSection("TenantOptions").Get<TenantOptions>();
//            foreach (var tenant in _tenantOptions.Tenants.Where(e => string.IsNullOrEmpty(e.ConnectionString)))
//            {
//                tenant.ConnectionString = _tenantOptions.DefaultConnection;
//            }
//        }
//        public Tenant[] GetTenants() => _tenantOptions.Tenants;
//    }

//    public class TenantOptions
//    {
//        public string? DefaultConnection { get; set; }
//        public Tenant[] Tenants { get; set; } = Array.Empty<Tenant>();
//    }
//}
