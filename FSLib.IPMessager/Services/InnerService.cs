using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSLib.IPMessager.Services
{
	/// <summary>
	/// 内置插件枚举
	/// </summary>
	public enum InnerService : int
	{
		/// <summary>
		/// 黑名单组件
		/// </summary>
		BlackListBlocker = 0,
		/// <summary>
		/// RSA加密组件
		/// </summary>
		RSAEncryptionComponent = 1,
		/// <summary>
		/// 移除回发消息
		/// </summary>
		RemoveLoopBackMessage = 2,
		/// <summary>
		/// 脏字过滤
		/// </summary>
		MessageFilterService = 3
	}
}
