using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPMessagerNet.UI.Controls.HostTreeView
{
	class HostEncryptNode : HostInfoNodeBase
	{
		public HostEncryptNode(FSLib.IPMessager.Entity.Host host)
			: base(host)
		{
			SetInfo();
			host.PublicKeyChanged += (s, e) => { SetInfo(); };
		}

		#region 界面响应

		void SetInfo()
		{
			this.Text = Host.SupportEncrypt ? (Host.PubKey == null ? "支持加密通信但尚未使用" : "已使用加密发送消息") : "不支持加密";
			SetIcon(Host.SupportEncrypt ? (Host.PubKey == null ? HostIcon.HostInfo_Enc_NotInitialize : HostIcon.HostInfo_Enc_Ok) : HostIcon.HostInfo_Enc_Disabled);
		}


		#endregion

	}
}
