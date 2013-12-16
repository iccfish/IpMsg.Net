using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSLib.IPMessager.Network;

namespace FSLib.IPMessager.Services
{
	/// <summary>
	/// 黑名单提供插件
	/// </summary>
	[Service("木鱼", "E-mail:fishcn@foxmail.com", "黑名单屏蔽组件", "(C)copyright 2009 木鱼", "提供对加入黑名单的主机消息屏蔽功能。如果被禁用，黑名单功能将无法使用。")]
	public class BanHostServiceProvider : ProviderBase, IServiceProvider
	{
		#region IServiceProvider 成员

		/// <summary>
		/// 执行初始化(已重载)
		/// </summary>
		/// <param name="client">客户端对象</param>
		public override void Initialize(IPMClient client)
		{
			base.Initialize(client);
		}

		/// <summary>
		/// 插件启动(已重载)
		/// </summary>
		public void Startup()
		{
			Client.MessageClient.IpValidateRequired += MessageClient_IpValidateRequired;
		}

		void MessageClient_IpValidateRequired(object sender, IpValidateRequiredEventArgs e)
		{
			//如果在列表中，则丢弃
			string addr = e.IPEndPoint.Address.ToString();
			if (Config.BanedHost.Contains(addr)) e.IsPackageDroped = true;
		}

		/// <summary>
		/// 插件卸载(已重载)
		/// </summary>
		public new void ShutDown()
		{
			Client.MessageClient.IpValidateRequired -= MessageClient_IpValidateRequired;
		}

		/// <summary>
		/// 是否支持配置(已重载)
		/// </summary>
		public new bool SupportsConfig
		{
			get { return false; }
		}

		/// <summary>
		/// 获得配置的UI界面(已重载)
		/// </summary>
		public new object ConfigUI
		{
			get { return null; }
		}

		/// <summary>
		/// 是否支持重置设置(已重载)
		/// </summary>
		public new bool SupportsResetConfig
		{
			get { return true; }
		}

		/// <summary>
		/// 重置为默认设置(已重载)
		/// </summary>
		public new void ResetConfig()
		{
			Config.BanedHost.Clear();
		}

		/// <summary>
		/// 重新加载设置(已重载)
		/// </summary>
		public new void ReloadConfig()
		{
			//
		}

		/// <summary>
		/// 插件图标(已重载)
		/// </summary>
		public new System.Drawing.Image PluginIcon
		{
			get { return Properties.Resources.security_plugins; }
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
		/// 检查是否可以加载(已重载)
		/// </summary>
		/// <param name="isFirstCall">是否是飞鸽传书初始化时候的加载</param>
		/// <returns></returns>
		public override bool CheckCanLoad(bool isFirstCall)
		{
			return true;
		}
		#endregion
	}
}
