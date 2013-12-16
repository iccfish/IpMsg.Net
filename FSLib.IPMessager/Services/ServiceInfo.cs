using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FSLib.IPMessager.Services
{
	/// <summary>
	/// 插件提供信息
	/// </summary>
	[Serializable]
	public class ServiceInfo
	{

		/// <summary>
		/// Initializes a new instance of the ServiceInfo class.
		/// </summary>
		public ServiceInfo()
		{
			State = ServiceState.NotInstalled;
		}

		/// <summary>
		/// 插件所在程序集
		/// </summary>
		public string Assembly { get; set; }

		/// <summary>
		/// 类别名
		/// </summary>
		public string TypeName { get; set; }

		/// <summary>
		/// 是否启用
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		/// 构造器
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		public System.Reflection.ConstructorInfo ConstructorInfo { get; set; }

		/// <summary>
		/// 实例对象
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		public IServiceProvider ServiceProvider { get; set; }

		/// <summary>
		/// 服务信息
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		public ServiceDescription ServiceDescription { get; private set; }


		/// <summary>
		/// 服务状态
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		public ServiceState State { get; private set; }

		System.Reflection.ConstructorInfo constInfo = null;

		/// <summary>
		/// 初始化加载
		/// </summary>
		/// <param name="client">绑定的客户端</param>
		/// <returns></returns>
		public bool Load(IPMClient client)
		{
			return Load(client, false);
		}

		/// <summary>
		/// 初始化加载
		/// </summary>
		/// <param name="client">绑定的客户端</param>
		/// <param name="isInitializingCall">是否是初始化加载</param>
		/// <returns></returns>
		internal bool Load(IPMClient client, bool isInitializingCall)
		{
			if (EnsureLoadAssembly() && CreateProviderInstance() && InitialzingServiceProvider(client))
			{
				LoadService(isInitializingCall);

				return true;
			}

			return false;
		}

		/// <summary>
		/// 加载程序集信息
		/// </summary>
		/// <returns></returns>
		public bool EnsureLoadAssembly()
		{
			if (constInfo != null) return true;

			System.Type tp = null;
			State = ServiceState.NotInstalled;

			if (string.IsNullOrEmpty(Assembly))
			{
				try
				{
					tp = System.Type.GetType(TypeName);
				}
				catch (Exception) { return false; }
			}
			else
			{
				string file = LocateAssemblyPath(Assembly);
				if (file == null) return false;
				try
				{
					tp = System.Reflection.Assembly.LoadFile(file).GetType(TypeName);
				}
				catch (Exception) { return false; }
			}

			State = ServiceState.LoadingError;
			if (tp == null) return false;

			//获得插件的信息
			object[] infos = tp.GetCustomAttributes(typeof(ServiceAttribute), true);
			if (infos != null && infos.Length > 0)
			{
				this.ServiceDescription = (infos[0] as ServiceAttribute).Description;
			}
			else
			{
				this.ServiceDescription = new ServiceDescription("未知", "未知", tp.Name, "未知", tp.FullName);
			}

			if ((constInfo = tp.GetConstructor(System.Type.EmptyTypes)) == null) return false;

			State = ServiceState.TypeLoaded;
			return true;
		}

		/// <summary>
		/// 创建实例对象
		/// </summary>
		/// <returns></returns>
		public bool CreateProviderInstance()
		{
			if (ServiceProvider != null) return true;

			if (State != ServiceState.TypeLoaded) return false;
			else if (!Enabled) return false;
			else
			{
				try
				{
					ServiceProvider = constInfo.Invoke(new object[] { }) as IServiceProvider;
					State = ServiceState.UnInitialized;
				}
				catch (Exception)
				{
					State = ServiceState.LoadingError;
					return false;
				}

				return ServiceProvider != null;
			}
		}

		/// <summary>
		/// 初始化指定插件
		/// </summary>
		/// <returns></returns>
		public bool InitialzingServiceProvider(IPMClient client)
		{
			if (State != ServiceState.UnInitialized) return ServiceProvider != null;
			else { ServiceProvider.Initialize(client); return true; }
		}

		/// <summary>
		/// 加载指定插件
		/// </summary>
		public bool LoadService()
		{
			return LoadService(false);
		}

		/// <summary>
		/// 加载指定插件
		/// </summary>
		/// <param name="onloadCall">是否是飞鸽传书初始化时候的请求</param>
		internal bool LoadService(bool onloadCall)
		{
			if (ServiceProvider == null || State == ServiceState.Running || !ServiceProvider.CheckCanLoad(onloadCall) || (!ServiceProvider.SupportLoad && !onloadCall)) return false;
			else
			{
				ServiceProvider.Startup();
				State = ServiceState.Running;

				return true;
			}
		}

		/// <summary>
		/// 卸载插件
		/// </summary>
		public bool ShutDown()
		{
			if (ServiceProvider == null || State == ServiceState.Unload || !ServiceProvider.SupportUnload) return false;
			else
			{
				ServiceProvider.ShutDown();
				State = ServiceState.Unload;

				return true;
			}
		}

		/// <summary>
		/// 确定程序集路径
		/// </summary>
		/// <param name="dllName">程序集名称</param>
		/// <returns>存在的路径.如果找不到,则返回null</returns>
		string LocateAssemblyPath(string dllName)
		{
			//查找同目录
			string path = System.IO.Path.Combine(IPMClient.RootPath, dllName);
			if (System.IO.File.Exists(path)) return path;

			path = System.IO.Path.Combine(IPMClient.RootPath, "addins\\" + dllName);
			if (System.IO.File.Exists(path)) return path;

			return null;
		}
	}

}
