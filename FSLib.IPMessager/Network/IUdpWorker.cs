using System;
using System.Net;

namespace FSLib.IPMessager.Network
{
	/// <summary>
	/// UDP文本消息接收类
	/// </summary>
	public interface IUdpWorker
	{
		/// <summary>
		/// 关闭客户端
		/// </summary>
		void Close();
		/// <summary>
		/// 发送数据。这个请求发送的是广播请求
		/// </summary>
		/// <param name="port">远程主机端口</param>
		/// <param name="data">数据流</param>
		void Send(int port, byte[] data, ulong packageNo);

		/// <summary>
		/// 发送数据，并对数据作回应检查。当数据包编号大于0时，将会在每隔 <see cref="CheckQueueTimeInterval"></see> 的间隔后重新发送，直到收到对方的回应。
		/// 注意：网络层不会解析回应，请调用 <see cref="PopSendItemFromList"></see> 方法来告知已收到数据包。
		/// </summary>
		/// <param name="address">远程主机地址</param>
		/// <param name="port">远程主机端口</param>
		/// <param name="data">数据流</param>
		/// <param name="packageNo">数据包编号</param>
		void Send(IPAddress address, int port, byte[] data, ulong packageNo);

		/// <summary>
		/// 发送数据，并对数据作回应检查。当数据包编号大于0时，将会在每隔 <see cref="CheckQueueTimeInterval"></see> 的间隔后重新发送，直到收到对方的回应。
		/// 注意：网络层不会解析回应，请调用 <see cref="PopSendItemFromList"></see> 方法来告知已收到数据包。
		/// </summary>
		/// <param name="receiveConfirm">消息是否会回发确认包</param>
		/// <param name="address">远程主机地址</param>
		/// <param name="port">远程主机端口</param>
		/// <param name="data">数据流</param>
		/// <param name="packageNo">数据包编号</param>
		void Send(bool receiveConfirm, IPAddress address, int port, byte[] data, ulong packageNo);

		/// <summary>
		/// 发送数据，并对数据作回应检查。当数据包编号大于0时，将会在每隔 <see cref="CheckQueueTimeInterval"></see> 的间隔后重新发送，直到收到对方的回应。
		/// 注意：网络层不会解析回应，请调用 <see cref="PopSendItemFromList"></see> 方法来告知已收到数据包。
		/// </summary>
		/// <param name="address">远程主机地址</param>
		/// <param name="port">远程主机端口</param>
		/// <param name="data">数据流</param>
		/// <param name="packageNo">数据包编号</param>
		/// <param name="packageIndex">分包索引</param>
		void Send(IPAddress address, int port, byte[] data, ulong packageNo, int packageIndex);

		/// <summary>
		/// 发送数据，并对数据作回应检查。当数据包编号大于0时，将会在每隔 <see cref="CheckQueueTimeInterval"></see> 的间隔后重新发送，直到收到对方的回应。
		/// 注意：网络层不会解析回应，请调用 <see cref="PopSendItemFromList"></see> 方法来告知已收到数据包。
		/// </summary>
		/// <param name="receiveConfirm">消息是否会回发确认包</param>
		/// <param name="address">远程主机地址</param>
		/// <param name="port">远程主机端口</param>
		/// <param name="data">数据流</param>
		/// <param name="packageNo">数据包编号</param>
		/// <param name="packageIndex">分包索引</param>
		void Send(bool receiveConfirm, IPAddress address, int port, byte[] data, ulong packageNo, int packageIndex);

		/// <summary>
		/// 发送数据，并对数据作回应检查。当 <see cref="receiveConfirm"></see> 为 true 时，将会在每隔 <see cref="CheckQueueTimeInterval"></see> 的间隔后重新发送，直到收到对方的回应。
		/// 注意：网络层不会解析回应，请调用 <see cref="PopSendItemFromList"></see> 方法来告知已收到数据包。
		/// </summary>
		/// <param name="receiveConfirm">消息是否会回发确认包</param>
		/// <param name="address">远程主机地址</param>
		/// <param name="data">数据流</param>
		/// <param name="packageNo">数据包编号</param>
		/// <param name="packageIndex">分包索引</param>
		void Send(bool receiveConfirm, IPEndPoint address, byte[] data, ulong packageNo, int packageIndex);

		/// <summary>
		/// 将已经打包的消息发送出去
		/// </summary>
		/// <param name="packedMessage">已经打包的消息</param>
		/// <param name="confirmReceived">是否回发确认消息</param>
		void Send(Entity.PackedNetworkMessage packedMessage);

		/// <summary>
		/// 将数据包从列表中移除
		/// </summary>
		/// <param name="packageNo">数据包编号</param>
		/// <param name="packageIndex">数据包分包索引</param>
		void PopSendItemFromList(ulong packageNo, int packageIndex);

		/// <summary>
		/// 是否已经初始化了
		/// </summary>
		bool IsInitialized { get; }
		/// <summary>
		/// 检查发送队列间隔
		/// </summary>
		int CheckQueueTimeInterval { get; set; }

		/// <summary>
		/// 没有收到确认包时，最大重新发送的数目，超过此数目会丢弃并触发 <see cref="PackageSendFailture"></see> 事件。
		/// </summary>
		int MaxResendTimes { get; set; }

		/// <summary>
		/// 网络出现异常（如端口无法绑定等，此时无法继续工作）
		/// </summary>
		event EventHandler NetworkError;
		/// <summary>
		/// 主机消息包被丢弃时触发
		/// </summary>
		event EventHandler<NetworkPackageEventArgs> PackageDroped;

		/// <summary>
		/// 当收到远程主机的消息，需要对其进行验证时触发
		/// </summary>
		event IpValidateRequiredEventHandler IpValidateRequired;
		/// <summary>
		/// 当数据包收到时触发
		/// </summary>
		event EventHandler<PackageReceivedEventArgs> PackageReceived;
		/// <summary>
		/// 数据包发送失败
		/// </summary>
		event EventHandler<PackageSendEventArgs> PackageSendFailure;
		/// <summary>
		/// 数据包未接收到确认，重新发送
		/// </summary>
		event EventHandler<PackageSendEventArgs> PackageResend;
	}
}
