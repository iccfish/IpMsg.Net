using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace FSLib.IPMessager.Services
{


	/// <summary>
	/// 服务提供基类
	/// </summary>
	public abstract class ProviderBase
	{
		#region 基类属性

		/// <summary>
		/// 关联的用户属性
		/// </summary>
		public IPMClient Client { get; set; }

		/// <summary>
		/// 命令执行对象
		/// </summary>
		public Core.CommandExecutor Commander { get { return Client.Commander; } }

		/// <summary>
		/// 文件管理器
		/// </summary>
		public Core.FileTaskManager FileTaskManager { get { return Client.FileTaskManager; } }

		/// <summary>
		/// 配置对象
		/// </summary>
		public Entity.Config Config { get { return Client.Config; } }

		/// <summary>
		/// UDP文本信息网络层对象
		/// </summary>
		public Network.UDPThread MessageClient { get { return Commander.Client; } }

		/// <summary>
		/// 文本信息翻译层对象
		/// </summary>
		public Network.MessageTranslator MessageProxy { get { return Commander.MessageProxy; } }

		/// <summary>
		/// 在线主机维护列表
		/// </summary>
		public Entity.OnlineHost OnlineHost { get { return Commander.LivedHost; } }

		/// <summary>
		/// 插件的配置
		/// </summary>
		public object ProviderConfig { get; set; }

		#endregion

		#region IServiceProvider 成员

		/// <summary>
		/// 初始化设置
		/// </summary>
		/// <param name="client">初始化客户端</param>
		public virtual void Initialize(IPMClient client)
		{
			Client = client;
		}

		/// <summary>
		/// 插件卸载
		/// </summary>
		public virtual void ShutDown()
		{
		}

		#endregion

		#region 配置


		/// <summary>
		/// 是否支持配置
		/// </summary>
		public virtual bool SupportsConfig { get { return false; } }

		/// <summary>
		/// 获得配置的UI界面
		/// </summary>
		public virtual object ConfigUI { get { return null; } }

		/// <summary>
		/// 是否支持重置设置
		/// </summary>
		public virtual bool SupportsResetConfig { get { return false; } }

		/// <summary>
		/// 重置为默认设置
		/// </summary>
		public virtual void ResetConfig() { }

		/// <summary>
		/// 默认提供当前插件的版本
		/// </summary>
		public virtual string Version
		{
			get
			{
				return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion;
			}
		}

		/// <summary>
		/// 重新加载设置
		/// </summary>
		public virtual void ReloadConfig()
		{
			if (ProviderConfig != null) OnRequireSaveConfig(new ServiceConfigEventArgs() { ConfigObject = ProviderConfig });
		}


		/// <summary>
		/// 插件图标
		/// </summary>
		public virtual Image PluginIcon { get { return null; } }

		/// <summary>
		/// 是否支持中途卸载
		/// </summary>
		public virtual bool SupportUnload { get { return false; } }


		/// <summary>
		/// 是否支持中途加载
		/// </summary>
		public virtual bool SupportLoad { get { return false; } }

		/// <summary>
		/// 是否支持在设置中心中扩展面板
		/// </summary>
		public virtual bool SupportControlPanel
		{
			get { return false; }
		}

		/// <summary>
		/// 在设置中心中的配置面板
		/// </summary>
		public virtual object ControlPanel
		{
			get { return false; }
		}


		/// <summary>
		/// 检查是否可以加载
		/// </summary>
		/// <param name="isFirstCall">是否是飞鸽传书初始化时候的加载</param>
		/// <returns></returns>
		public virtual bool CheckCanLoad(bool isFirstCall)
		{
			return isFirstCall;
		}


		/// <summary>
		/// 获得主机功能标识
		/// </summary>
		/// <returns>新标识</returns>
		public ulong GenerateClientFeatures()
		{
			return 0ul;
		}
		#endregion

		#region 事件

		/// <summary>
		/// 请求加载配置事件
		/// </summary>
		public event EventHandler<ServiceConfigEventArgs> RequireLoadConfig;

		/// <summary>
		/// 触发请求加载配置事件
		/// </summary>
		/// <param name="e">事件参数</param>
		/// <returns>返回是否被处理</returns>
		protected virtual bool OnRequireLoadConfig(ServiceConfigEventArgs e)
		{
			if (RequireLoadConfig != null) RequireLoadConfig(this, e);

			return e.IsHandled;
		}

		/// <summary>
		/// 请求保存配置事件
		/// </summary>
		public event EventHandler<ServiceConfigEventArgs> ReuqireSaveConfig;

		/// <summary>
		/// 触发请求保存配置事件
		/// </summary>
		/// <param name="e">事件参数</param>
		protected virtual void OnRequireSaveConfig(ServiceConfigEventArgs e)
		{
			if (ReuqireSaveConfig != null) ReuqireSaveConfig(this, e);
		}

		#endregion

		#region 功能性函数

		/// <summary>
		/// 加载配置
		/// </summary>
		/// <typeparam name="T">配置类型</typeparam>
		/// <returns>加载的配置</returns>
		protected virtual T LoadConfig<T>() where T : class
		{
			if (ProviderConfig == null)
			{
				ServiceConfigEventArgs e = new ServiceConfigEventArgs() { Type = typeof(T) };
				if (OnRequireLoadConfig(e)) ProviderConfig = e.ConfigObject;
			}

			return ProviderConfig as T;
		}

		/// <summary>
		/// 保存配置
		/// </summary>
		protected virtual void SaveConfig()
		{
			if (ProviderConfig != null)
			{
				ServiceConfigEventArgs e = new ServiceConfigEventArgs() { ConfigObject = ProviderConfig };
				OnRequireSaveConfig(e);
			}
		}

		#endregion
	}
}
