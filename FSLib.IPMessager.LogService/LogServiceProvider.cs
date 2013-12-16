using System;
using System.Collections.Generic;
using System.Text;
using FSLib.IPMessager.Services;

namespace FSLib.IPMessager.LogService
{
	/// <summary>
	/// 提供对飞鸽传书.net日志的支持
	/// </summary>
	[Service("木鱼", "fishcn@foxmail.com", "通信日志文本记录", "©Copyright2009,fishcn@fxomail.com", "提供基于文件系统的日志记录")]
	public class LogServiceProvider : ProviderBase<LogServiceConfig>, IPMessagerNet.API.ILogProvider, FSLib.IPMessager.Services.IServiceProvider
	{
		#region IServiceProvider Members

		/// <summary>
		/// 初始化请求(已重载)
		/// </summary>
		/// <param name="client">飞鸽传书客户端</param>
		public override void Initialize(IPMClient client)
		{
			
		}

		#region 捕捉事件

		bool isEnabled = false;

		#endregion


		/// <summary>
		/// 启动指定插件(已重载)
		/// </summary>
		public void Startup()
		{
			isEnabled = true;
		}

		/// <summary>
		/// 停止指定插件(已重载)
		/// </summary>
		public override void ShutDown()
		{
			base.ShutDown();
			isEnabled = false;
		}

		/// <summary>
		/// 是否支持中途加载(已重载)
		/// </summary>
		public override bool SupportLoad
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// 是否支持中途卸载(已重载)
		/// </summary>
		public override bool SupportUnload
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// 是否支持配置(已重载)
		/// </summary>
		public override bool SupportsConfig
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// 获得配置使用的UI界面(已重载)
		/// </summary>
		public override object ConfigUI
		{
			get
			{
				ShowLogViewer();
				return null;
			}
		}

		/// <summary>
		/// 获得插件的图标(已重载)
		/// </summary>
		public override System.Drawing.Image PluginIcon
		{
			get
			{
				return Properties.Resources.address_16;
			}
		}

		/// <summary>
		/// 获得插件的版本信息(已重载)
		/// </summary>
		public override string Version
		{
			get
			{
				return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion;
			}
		}

		public override bool CheckCanLoad(bool isFirstCall)
		{
			return true;
		}

		#endregion

		#region ILogProvider Members

		public void ShowLogViewer()
		{

		}

		public void ShowLogConfig()
		{

		}

		#endregion
	}
}
