using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using FSLib.IPMessager.Entity;

namespace FSLib.IPMessager.Network
{
	/// <summary>
	/// 委托，供IP验证使用
	/// </summary>
	/// <param name="sender">事件源</param>
	/// <param name="e">事件的数据</param>
	public delegate void IpValidateRequiredEventHandler(object sender, IpValidateRequiredEventArgs e);
}
