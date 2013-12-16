using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace FSLib.IPMessager.Services
{
	/// <summary>
	/// 插件服务管理
	/// </summary>
	public class ServiceManager
	{
		/// <summary>
		/// 内置的插件类名映射字典
		/// </summary>
		public readonly static Dictionary<InnerService, string> InnerServiceTypeList;

		static ServiceManager()
		{
			InnerServiceTypeList = new Dictionary<InnerService, string>();
			InnerServiceTypeList.Add(InnerService.BlackListBlocker, typeof(BanHostServiceProvider).FullName);
			InnerServiceTypeList.Add(InnerService.RSAEncryptionComponent, typeof(MessageEncryptProvider).FullName);
			InnerServiceTypeList.Add(InnerService.RemoveLoopBackMessage, typeof(RemoveLoopBackMessage).FullName);
			InnerServiceTypeList.Add(InnerService.MessageFilterService, typeof(MessageFilterServiceProvider).FullName);
		}

		/// <summary>
		/// 通过文件路径来查找所有服务
		/// </summary>
		/// <param name="assemblyPath"></param>
		/// <returns></returns>
		public static ServiceInfo[] GetServicesInAssembly(string assemblyPath)
		{
			try
			{
				return GetServicesInAssembly(Assembly.LoadFile(assemblyPath));
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// 查找指定程序集中所有的服务类
		/// </summary>
		/// <param name="assembly"></param>
		/// <returns></returns>
		public static ServiceInfo[] GetServicesInAssembly(Assembly assembly)
		{
			System.Type[] types = assembly.GetTypes();

			List<ServiceInfo> typeList = new List<ServiceInfo>();
			Array.ForEach(types, s =>
			{
				if (!s.IsPublic || s.IsAbstract) return;
				Type t = s.GetInterface(typeof(IServiceProvider).FullName);
				if (t == null) return;

				object[] infos = s.GetCustomAttributes(typeof(ServiceAttribute), true);

				ServiceInfo info = new ServiceInfo();
				info.Assembly = System.IO.Path.GetFileName(assembly.Location);
				info.TypeName = s.FullName;

				if (infos == null || infos.Length == 0)
				{
					info.Enabled = true;
				}
				else
				{
					info.Enabled = (infos[0] as ServiceAttribute).Description.DefaultEnabled;
				}
				typeList.Add(info);
			});

			return typeList.ToArray();
		}

		/// <summary>
		/// 获得内置的插件
		/// </summary>
		/// <returns></returns>
		public static ServiceInfo[] GetServicesInAssembly()
		{
			return GetServicesInAssembly(System.Reflection.Assembly.GetExecutingAssembly());
		}

		/// <summary>
		/// 在指定的路径中查找服务提供者
		/// </summary>
		/// <param name="loaderPath">文件夹列表</param>
		/// <returns>查找的结果</returns>
		public static ServiceList GetServices(params string[] loaderPath)
		{
			ServiceList list = new ServiceList();
			Action<string> loader = s =>
			{
				ServiceInfo[] slist = GetServicesInAssembly(s);
				if (slist != null) list.AddRange(slist);
			};
			Action<string> folderLoader = s =>
			{
				if (!System.IO.Path.IsPathRooted(s))
					s = System.IO.Path.Combine(
						System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), s);

				string[] files = System.IO.Directory.GetFiles(s, "*.exe");
				Array.ForEach(files, loader);

				files = System.IO.Directory.GetFiles(s, "*.dll");
				Array.ForEach(files, loader);
			};

			folderLoader(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location));
			Array.ForEach(loaderPath, folderLoader);

			return list;
		}
	}
}
