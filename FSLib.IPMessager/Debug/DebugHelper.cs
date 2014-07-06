using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SD = System.Diagnostics;

namespace FSLib.IPMessager.Debug
{
	/// <summary>
	/// 调试器辅助类
	/// <para>这个是供调试使用的辅助类，输出将会直接输出到VS的调试窗口。</para>
	/// <para>这和调试插件是不一样的</para>
	/// </summary>
	public class DebugHelper
	{
		bool isAttached;

		/// <summary>
		/// 构造一个新的调试器对象
		/// </summary>
		public DebugHelper()
		{
			isAttached = false;
		}

		/// <summary>
		/// 将调试器辅助类附加到客户端上
		/// <para>这个是自动附加上的，如果当前处于Release模式，将不会附加</para>
		/// </summary>
		/// <param name="client">要附加的客户端</param>
		[System.Diagnostics.Conditional("DEBUG")]
		internal void AttachHelperAuto(IPMClient client)
		{
			if (isAttached) return;
			AttachHelper(client);
		}

		/// <summary>
		/// 将调试器辅助类附加到客户端上
		/// <para>这个是强行附加上的，不会考虑到是DEBUG模式还是Release模式</para>
		/// </summary>
		/// <param name="client">要附加的客户端</param>
		public void AttachHelper(IPMClient client)
		{
			client.MessageClient.IpValidateRequired += new Network.IpValidateRequiredEventHandler(MessageClient_IpValidateRequired);
			client.MessageClient.PackageReceived += new EventHandler<Network.PackageReceivedEventArgs>(MessageClient_PackageReceived);
			client.MessageClient.PackageResend += new EventHandler<Network.PackageSendEventArgs>(MessageClient_PackageResend);
			client.MessageClient.PackageSendFailure += new EventHandler<Network.PackageSendEventArgs>(MessageClient_PackageSendFailure);
			client.MessageClient.PackageDroped += new EventHandler<Network.NetworkPackageEventArgs>(MessageClient_PackageDroped);
		}

		void MessageClient_PackageDroped(object sender, Network.NetworkPackageEventArgs e)
		{
			SD.Debug.WriteLine("UDP数据包已经被过滤。远程主机：" + e.IPEndPoint.ToString() + "，数据包长度：" + e.Data.Length.ToString() + "，数据内容：" + BitConverter.ToString(e.Data).Replace("-", ""));
		}

		void MessageClient_PackageSendFailure(object sender, Network.PackageSendEventArgs e)
		{
		}

		void MessageClient_PackageResend(object sender, Network.PackageSendEventArgs e)
		{

		}

		void MessageClient_PackageReceived(object sender, Network.PackageReceivedEventArgs e)
		{

		}

		void MessageClient_IpValidateRequired(object sender, Network.IpValidateRequiredEventArgs e)
		{
			SD.Debug.WriteLine("已收到UDP数据包，正在请求对来源主机信息进行验证。远程主机：" + e.IPEndPoint.ToString() + "，数据包长度：" + e.Data.Length.ToString());
		}

		/// <summary>
		/// 客户端对象
		/// </summary>
		public IPMClient Client { get; set; }
	}
}
