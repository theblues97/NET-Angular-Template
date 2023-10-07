//using Autofac;
//using Autofac.Extras.Moq;
//using Core.Context;
//using Core.Dependency;
//using Microsoft.EntityFrameworkCore;
//using 
//namespace Project.Test;

//public abstract class BaseTestService
//{

//    public BaseTestService()
//    {
//    }

//    private void ServiceRegistrar(ContainerBuilder cb)
//    {
//        var moduleType = AssemblyRegistrar.GetListTypeModule();
        
//        cb.RegisterGeneric(typeof(DbContextProvider<>))
//                .As(typeof(IDbContextProvider<>))
//                .InstancePerDependency();

//        var options = new DbContextOptionsBuilder<AppDbContext>()
//            .UseInMemoryDatabase(databaseName: "MovieListDatabase")
//            .Options;

//        moduleType.ForEach(module =>
//        {
            

//            cb.GenericRepositoryRegistrar(module);
//            var assignableToBaseService = module.Assembly
//            .GetTypes()
//            .Where(type => !type.IsAbstract && typeof(BaseService).IsAssignableFrom(type));

//            foreach (var service in assignableToBaseService)
//            {
//                cb.RegisterType(service).PropertiesAutowired();
//            }
//        });
//    }

//    protected AutoMock AutoMock => AutoMock.GetLoose(ServiceRegistrar);
//}