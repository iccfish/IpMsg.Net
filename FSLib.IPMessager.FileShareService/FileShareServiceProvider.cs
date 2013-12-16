using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSLib.IPMessager.Services;

namespace FSLib.IPMessager.FileShareService
{
	/// <summary>
	/// 提供文件共享的功能
	/// </summary>
	[Service("木鱼", "fishcn@foxmail.com", "文件共享", "CopyRight2005-2010 木鱼", "提供局域网内部文件共享的支持")]
	public class FileShareServiceProvider : FSLib.IPMessager.Services.ProviderBase<string>, FSLib.IPMessager.Services.IServiceProvider
	{
		private bool _enabled;


		/// <summary>
		/// 插件启动
		/// </summary>
		public void Startup()
		{
			_enabled = true;
		}

		/// <summary>
		/// 检查是否可以加载
		/// </summary>
		/// <param name="isFirstCall">是否是飞鸽传书初始化时候的加载</param>
		/// <returns></returns>
		public override bool CheckCanLoad(bool isFirstCall)
		{
			return true;
		}

		/// <summary>
		/// 初始化设置
		/// </summary>
		/// <param name="client">初始化客户端</param>
		public override void Initialize(IPMClient client)
		{
			base.Initialize(client);

			_enabled = true;
		}

		/// <summary>
		/// 插件图标
		/// </summary>
		public override System.Drawing.Image PluginIcon
		{
			get
			{
				return Properties.Resources.wallet_16;
			}
		}

		/// <summary>
		/// 是否支持中途卸载
		/// </summary>
		public override bool SupportUnload
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// 是否支持中途加载
		/// </summary>
		public override bool SupportLoad
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// 是否支持配置
		/// </summary>
		public override bool SupportsConfig
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// 获得配置的UI界面
		/// </summary>
		public override object ConfigUI
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// 插件卸载
		/// </summary>
		public override void ShutDown()
		{
			base.ShutDown();
			_enabled = false;
		}

		/// <summary>
		/// 默认提供当前插件的版本
		/// </summary>
		public override string Version
		{
			get
			{
				return base.Version;
			}
		}

		/// <summary>
		/// 是否支持在设置中心中扩展面板
		/// </summary>
		public override bool SupportControlPanel
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// 在设置中心中的配置面板
		/// </summary>
		public override object ControlPanel
		{
			get
			{
				return null;
			}
		}
	}
}
