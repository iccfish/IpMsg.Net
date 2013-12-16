using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSLib.IPMessager.Network;

namespace FSLib.IPMessager.Services
{

	/// <summary>
	/// 本地消息回发过滤插件
	/// </summary>
	[Service("木鱼", "E-mail:fishcn@foxmail.com", "本地消息回发过滤插件", "(C)copyright 2009 木鱼", "如果此组件被启用，将会从主机列表中把自己隐藏，不接受来自自己的消息。", false)]
	public class RemoveLoopBackMessage : ProviderBase, IServiceProvider
	{
		#region IServiceProvider 成员

		/// <summary>
		/// 插件启动(已重载)
		/// </summary>
		public void Startup()
		{
			MessageClient.IpValidateRequired += MessageClient_IpValidateRequired;
		}

		void MessageClient_IpValidateRequired(object sender, IpValidateRequiredEventArgs e)
		{
			//检测事件
			if (e.IsPackageDroped) return;

			foreach (var ip in Client.LocalAddresses)
			{
				if (ip.IsSameIPAs(e.IPEndPoint.Address))
				{
					e.IsPackageDroped = true;
					break;
				}
			}
		}

		/// <summary>
		/// 插件卸载(已重载)
		/// </summary>
		public new void ShutDown()
		{
			MessageClient.IpValidateRequired -= MessageClient_IpValidateRequired;
		}

		/// <summary>
		/// 插件图标
		/// </summary>
		public override System.Drawing.Image PluginIcon
		{
			get
			{
				return Properties.Resources.block_16;
			}
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
