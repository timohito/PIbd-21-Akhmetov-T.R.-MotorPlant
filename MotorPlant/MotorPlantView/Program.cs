using Microsoft.IdentityModel.Protocols;
using MotorPlantBusinessLogic.BusinessLogics;
using MotorPlantBusinessLogic.HelperModels;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantDatabaseImplement.Implements;
using System;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace MotorPlantView
{
	static class Program
	{
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main()
		{
			var container = BuildUnityContainer();
			MailLogic.MailConfig(new MailConfig
			{
				SmtpClientHost = ConfigurationManager.AppSettings["SmtpClientHost"],
				SmtpClientPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpClientPort"]),
				MailLogin = ConfigurationManager.AppSettings["MailLogin"],
				MailPassword = ConfigurationManager.AppSettings["MailPassword"],
			});

			// создаем таймер
			var mailLogic = container.Resolve<MailLogic>();
			var timer = new System.Threading.Timer(new TimerCallback(MailCheck), new MailCheckInfo
			{
				PopHost = ConfigurationManager.AppSettings["PopHost"],
				PopPort = Convert.ToInt32(ConfigurationManager.AppSettings["PopPort"]),
				MessageStorage = container.Resolve<IMessageInfoStorage>(),
				ClientStorage = container.Resolve<IClientStorage>()
			}, 0, 100000);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(container.Resolve<FormMain>());
		}

		private static IUnityContainer BuildUnityContainer()
		{
			var currentContainer = new UnityContainer();
			currentContainer.RegisterType<IComponentStorage, ComponentStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IOrderStorage, OrderStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IEngineStorage, EngineStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IImplementerStorage, ImplementerStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IClientStorage, ClientStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IMessageInfoStorage, MessageInfoStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ComponentLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<OrderLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<EngineLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ReportLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ClientLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ImplementerLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<MailLogic>(new HierarchicalLifetimeManager());
			return currentContainer;
		}

		private static void MailCheck(object obj)
		{
			MailLogic.MailCheck((MailCheckInfo)obj);
		}
	}
}
