using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FSLib.IPMessager.Services
{
	/// <summary>
	/// 服务提供接口
	/// </summary>
	public interface IServiceProvider
	{
		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="client">客户端对象</param>
		void Initialize(IPMClient client);

		/// <summary>
		/// 插件启动
		/// </summary>
		void Startup();

		/// <summary>
		/// 插件卸载
		/// </summary>
		void ShutDown();

		/// <summary>
		/// 是否支持中途卸载
		/// </summary>
		bool SupportUnload { get; }

		/// <summary>
		/// 是否支持中途加载
		/// </summary>
		bool SupportLoad { get; }

		/// <summary>
		/// 检查是否可以加载
		/// </summary>
		/// <param name="isFirstCall">是否是飞鸽传书初始化时候的加载</param>
		/// <returns></returns>
		bool CheckCanLoad(bool isFirstCall);

		/// <summary>
		/// 当前运行的插件版本
		/// </summary>
		string Version { get; }

		#region 主机功能设置

		/// <summary>
		/// 获得主机功能标识
		/// </summary>
		/// <returns>新标识</returns>
		ulong GenerateClientFeatures();

		#endregion

		#region 配置相关

		/// <summary>
		/// 是否支持更新
		/// </summary>
		bool SupportsConfig { get; }

		/// <summary>
		/// 获得配置的UI界面
		/// </summary>
		object ConfigUI { get; }

		/// <summary>
		/// 是否支持重置设置
		/// </summary>
		bool SupportsResetConfig { get; }

		/// <summary>
		/// 重置为默认设置
		/// </summary>
		void ResetConfig();

		/// <summary>
		/// 重新加载设置
		/// </summary>
		void ReloadConfig();

		/// <summary>
		/// 插件图标
		/// </summary>
		Image PluginIcon { get; }


		/// <summary>
		/// 配置
		/// </summary>
		object ProviderConfig { get; set; }

		/// <summary>
		/// 是否支持在设置中心中扩展面板
		/// </summary>
		bool SupportControlPanel { get; }

		/// <summary>
		/// 在设置中心中的配置面板
		/// </summary>
		object ControlPanel { get; }

		#endregion

		#region 事件

		/// <summary>
		/// 请求加载配置事件
		/// </summary>
		event EventHandler<ServiceConfigEventArgs> RequireLoadConfig;

		/// <summary>
		/// 请求保存配置事件
		/// </summary>
		event EventHandler<ServiceConfigEventArgs> ReuqireSaveConfig;

		#endregion
	}
}
