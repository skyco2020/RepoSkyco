using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Resolver;
using Unity.Mvc4;

namespace SkyCoApi
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

        DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        return container;
    }

    public static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      // e.g. container.RegisterType<ITestService, TestService>();    
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
            //Component initialization via MEF
            ComponentLoader.LoadContainer(container, ".\\bin", "SkyCoApi.dll");
            ComponentLoader.LoadContainer(container, ".\\bin", "BusinessServices.dll");
        }
  }
}