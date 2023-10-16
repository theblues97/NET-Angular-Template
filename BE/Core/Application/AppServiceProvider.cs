//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Core.Application
//{
//	public interface IAppServiceProvider
//	{

//	}
//	public class AppServiceProvder: IAppServiceProvider
//	{
//		private readonly IServiceProvider _serviceProvider;	
//		public AppServiceProvder(IServiceProvider serviceProvider) 
//		{
//			_serviceProvider = serviceProvider;
//		}

//		public T? GetService<T>() where T : class
//		{
//			return _serviceProvider.GetService<T>();
//		}

//		public T GetRequireService<T>() where T : class
//		{
//			return _serviceProvider.GetRequiredService<T>();
//		}
//	}
//}
