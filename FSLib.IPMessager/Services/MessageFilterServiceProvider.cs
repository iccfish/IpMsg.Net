using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSLib.IPMessager.Services
{
	/// <summary>
	/// 内置的消息关键字过滤插件
	/// </summary>
	[Service("木鱼", "E-mail:fishcn@foxmail.com", "消息关键字过滤组件", "(C)copyright 2009 木鱼", "如果此组件被启用，可以过滤包含有设定关键字的消息，减少收到的垃圾信息。")]
	public class MessageFilterServiceProvider : ProviderBase<List<string>>, IServiceProvider
	{
		#region IServiceProvider 成员

		/// <summary>
		/// 启动插件(已重载)
		/// </summary>
		public void Startup()
		{
			this.Client.Commander.MessageProcessing += Commander_MessageProcessing;
		}



		void Commander_MessageProcessing(object sender, FSLib.IPMessager.Entity.MessageEventArgs e)
		{
			if (ProviderConfig == null)
			{
				LoadConfig();

				if (ProviderConfig == null)
				{
					ProviderConfig = new List<string>();
					SaveConfig();
				}
			}

			foreach (var item in ProviderConfig)
			{
				if (e.Message.NormalMsg.IndexOf(item, StringComparison.OrdinalIgnoreCase) != -1)
				{
					//e.IsHandled = true;
					break;
				}
			}
		}

		#endregion
		public override System.Drawing.Image PluginIcon
		{
			get
			{
				return Properties.Resources.filter;
			}
		}

		public override bool CheckCanLoad(bool isFirstCall)
		{
			return true;
		}

		public override void ShutDown()
		{
			this.Client.Commander.MessageProcessing -= Commander_MessageProcessing;
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

	}
}
