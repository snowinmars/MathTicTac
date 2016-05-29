[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MathTicTac.PL.RestService.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MathTicTac.PL.RestService.App_Start.NinjectWebCommon), "Stop")]

namespace MathTicTac.PL.RestService.App_Start
{
	using Interfaces;
	using BLL.Interfaces;
	using Microsoft.Web.Infrastructure.DynamicModuleHelper;
	using Ninject;
	using Ninject.Web.Common;
	using System;
	using System.Web;
	using BLL.Logic;
	using DAL.Dao;
	using DAL.Interfaces;
	using Models;
	public static class NinjectWebCommon
	{
		private static readonly Bootstrapper bootstrapper = new Bootstrapper();
		public static IKernel Kernel;

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
				kernel.Bind<IAccountService>().To<AccountService>().InSingletonScope();
				kernel.Bind<IAccountLogic>().To<AccountLogic>().InSingletonScope();
				kernel.Bind<IAccountDao>().To<AccountDao>().InSingletonScope();
				kernel.Bind<IGameService>().To<GameService>().InSingletonScope();
				kernel.Bind<IGameLogic>().To<GameLogic>().InSingletonScope();
				kernel.Bind<IGameDao>().To<GameDao>().InSingletonScope();

				NinjectWebCommon.Kernel = kernel;
				return kernel;
			}
			catch
			{
				kernel.Dispose();
				throw;
			}
		}
	}
}