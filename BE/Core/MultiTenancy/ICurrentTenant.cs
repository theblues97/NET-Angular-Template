//using Core.Dependency;
//using Microsoft.AspNetCore.Http;

//namespace Core.MultiTenancy
//{
//    public interface ICurrentTenant
//    {
//        string Tenant { get; }
//    }

//	public class CurrentTenant : ICurrentTenant, IScopedDependency
//	{
//		private readonly IHttpContextAccessor _httpContextAccessor;
//		private string _tenantId;
//		public CurrentTenant() 
//		{
//			_tenantId = "446a5211-3d72-4339-9adc-845151f8ada0";
//		}
//		public string Tenant 
//		{ 
//			get { return _tenantId; }
//		}

//		public void SetCurrent(string tenant) => _tenantId = tenant;
//	}
//}
