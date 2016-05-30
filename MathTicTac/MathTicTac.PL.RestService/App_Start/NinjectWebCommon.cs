[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MathTicTac.PL.RestService.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MathTicTac.PL.RestService.App_Start.NinjectWebCommon), "Stop")]

namespace MathTicTac.PL.RestService.App_Start
{
	using BLL.Interfaces;
	using BLL.Logic;
	using DAL.Dao;
	using DAL.Interfaces;
	using Interfaces;
	using Microsoft.Web.Infrastructure.DynamicModuleHelper;
	using Models;
	using Ninject;
	using Ninject.Web.Common;
	using System;
	using System.Web;
	using System.Web.Http;

	public static class NinjectWebCommon
	{
		private static readonly Bootstrapper bootstrapper = new Bootstrapper();

		/// <summary>
		/// Starts the application
		/// </summary>
		public static void Start()
		{
			DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
			DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
			bootstrapper.Initialize(CreateKernel);
		}

		/// <summary>
		/// Stops the application.
		/// </summary>
		public static void Stop()
		{
			bootstrapper.ShutDown();
		}

		/// <summary>
		/// Creates the kernel that will manage your application.
		/// </summary>
		/// <returns>The created kernel.</returns>
		private static IKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			try
			{
				kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
				kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

				RegisterServices(kernel);
				return kernel;
			}
			catch
			{
				kernel.Dispose();
				throw;
			}
		}

		/// <summary>
		/// Load your modules or register your services here!
		/// </summary>
		/// <param name="kernel">The kernel.</param>
		private static void RegisterServices(IKernel kernel)
		{
			// due to Controller and ApiController use different Dependency Resolvers
			GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);

			kernel.Bind<IAccountService>().To<AccountService>().InSingletonScope();
			kernel.Bind<IAccountLogic>().To<AccountLogic>().InSingletonScope();
			kernel.Bind<IAccountDao>().To<AccountDao>().InSingletonScope();
			kernel.Bind<IGameService>().To<GameService>().InSingletonScope();
			kernel.Bind<IGameLogic>().To<GameLogic>().InSingletonScope();
			kernel.Bind<IGameDao>().To<GameDao>().InSingletonScope();
		}
	}
}