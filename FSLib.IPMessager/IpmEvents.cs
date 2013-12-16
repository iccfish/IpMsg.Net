using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSLib.IPMessager.Entity;
using FSLib.IPMessager.Network;

namespace FSLib.IPMessager
{
	/// <summary>
	/// 表示所有公开的飞鸽传书客户端的事件集合
	/// </summary>
	public static class IpmEvents
	{
		/// <summary>
		/// 获得或设置是否允许启用跨线程调用模式。如果设置为true，将会把一切事件都封送到创建事件的线程上进行调用
		/// </summary>
		public static bool EnableThreadContextSynchroization { get; set; }

		/// <summary>
		/// 进行指定的事件回调
		/// </summary>
		/// <param name="sync">true表示同步回调，false则是异步。这仅在启用跨线程调用时有效</param>
		/// <param name="handler">事件句柄</param>
		/// <param name="client">当前的客户端</param>
		static void SendOrPostCallback(bool sync, EventHandler handler, IPMClient client)
		{
			if (handler == null) return;

			if (EnableThreadContextSynchroization && client.ThreadContext != null)
			{
				if (sync) client.ThreadContext.Send(_ => handler(client, EventArgs.Empty), null);
				else client.ThreadContext.Post(_ => handler(client, EventArgs.Empty), null);
			}
			else
			{
				handler(client, EventArgs.Empty);
			}
		}

		/// <summary>
		/// 进行指定的事件回调
		/// </summary>
		/// <param name="sync">true表示同步回调，false则是异步。这仅在启用跨线程调用时有效</param>
		/// <param name="handler">事件句柄</param>
		/// <param name="client">当前的客户端</param>
		/// <param name="args">要回调用的事件数据</param>
		static void SendOrPostCallback<T>(bool sync, EventHandler<T> handler, IPMClient client, T args) where T : EventArgs
		{
			if (handler == null) return;

			if (EnableThreadContextSynchroization && client.ThreadContext != null)
			{
				if (sync) client.ThreadContext.Send(_ => handler(client, args), null);
				else client.ThreadContext.Post(_ => handler(client, args), null);
			}
			else
			{
				handler(client, args);
			}
		}

		#region UDP接口事件

		/// <summary>
		/// 网络出现异常（如端口无法绑定等，此时无法继续工作）
		/// </summary>
		public static event EventHandler UdpNetworkError;

		/// <summary>
		/// 当UDP网络出现错误的时候触发
		/// </summary>
		/// <param name="client">引发此事件的源对象</param>
		/// <param name="e">包含此事件的参数</param>
		internal static void OnUdpNetworkError(IPMClient client, EventArgs e)
		{
			SendOrPostCallback(false, UdpNetworkError, client);
		}

		/// <summary>
		/// 主机消息包被丢弃时触发
		/// </summary>
		public static event EventHandler<NetworkPackageEventArgs> UdpPackageDroped;

		/// <summary>
		/// 主机消息包被丢弃时，被调用
		/// </summary>
		/// <param name="client">引发此事件的源对象</param>
		/// <param name="e">包含事件的参数</param>
		internal static void OnUdpPackageDroped(IPMClient client, NetworkPackageEventArgs e)
		{
			SendOrPostCallback(false, UdpPackageDroped, client, e);
		}

		/// <summary>
		/// 当收到远程主机的消息，需要对其进行验证时触发
		/// </summary>
		public static event EventHandler<IpValidateRequiredEventArgs> UdpIpValidateRequired;

		/// <summary>
		/// 当事件触发时调用
		/// </summary>
		/// <param name="client">引发此事件的源对象</param>
		/// <param name="e"></param>
		internal static void OnUdpIpValidateRequired(IPMClient client, IpValidateRequiredEventArgs e)
		{
			SendOrPostCallback(false, UdpIpValidateRequired, client, e);
		}


		/// <summary>
		/// 当数据包收到时触发
		/// </summary>
		public static event EventHandler<PackageReceivedEventArgs> UdpPackageReceived;

		/// <summary>
		/// 当数据包收到事件触发时，被调用
		/// </summary>
		/// <param name="client">引发此事件的源对象</param>
		/// <param name="e">包含事件的参数</param>
		internal static void OnUdpPackageReceived(IPMClient client, PackageReceivedEventArgs e)
		{
			SendOrPostCallback(false, UdpPackageReceived, client, e);
		}
		/// <summary>
		/// 数据包发送失败
		/// </summary>
		public static event EventHandler<PackageSendEventArgs> UdpPackageSendFailure;

		/// <summary>
		/// 当数据发送失败时调用
		/// </summary>
		/// <param name="client">引发此事件的源对象</param>
		/// <param name="e">包含事件的参数</param>
		internal static void OnUdpPackageSendFailure(IPMClient client, PackageSendEventArgs e)
		{
			SendOrPostCallback(false, UdpPackageSendFailure, client, e);
		}

		/// <summary>
		/// 数据包未接收到确认，重新发送
		/// </summary>
		public static event EventHandler<PackageSendEventArgs> UdpPackageResend;


		/// <summary>
		/// 触发重新发送事件
		/// </summary>
		/// <param name="client">引发此事件的源对象</param>
		/// <param name="e">包含事件的参数</param>
		internal static void OnUdpPackageResend(IPMClient client, PackageSendEventArgs e)
		{
			SendOrPostCallback(false, UdpPackageResend, client, e);
		}



		#endregion

		#region TCP 接口事件

		/// <summary>
		/// 在文件操作中出现异常
		/// </summary>
        public static event EventHandler<FileSystemOperationErrorEventArgs> TcpThreadFileSystemOperationError;


		/// <summary>
		/// 引发 <see cref="TcpThreadFileSystemOperationError" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		/// <param name="ea">包含此事件的参数</param>
		internal static void OnTcpThreadFileSystemOperationError(IPMClient sender, FileSystemOperationErrorEventArgs ea)
		{
			SendOrPostCallback(false, TcpThreadFileSystemOperationError, sender, ea);
		}

		/// <summary>
		/// TCP端口绑定出现错误
		/// </summary>
        public static event EventHandler TcpNetworkError;


		/// <summary>
		/// 引发 <see cref="TcpNetworkError" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		internal static void OnTcpNetworkError(object sender)
		{
			var handler = TcpNetworkError;
			if (handler != null)
				handler(sender, EventArgs.Empty);
		}



		#endregion

		#region 文本消息事件

		#endregion


		#region 文件传输事件

		#endregion
	}
}
