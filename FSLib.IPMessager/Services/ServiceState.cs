using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace FSLib.IPMessager.Services
{
	/// <summary>
	/// 服务运行状态
	/// </summary>
	public enum ServiceState : int
	{
		/// <summary>
		/// 未安装
		/// </summary>
		NotInstalled = 0,
		/// <summary>
		/// 正在运行
		/// </summary>
		Running = 1,
		/// <summary>
		/// 已禁用
		/// </summary>
		Disabled = 2,
		/// <summary>
		/// 加载错误
		/// </summary>
		LoadingError = 3,
		/// <summary>
		/// 未初始化
		/// </summary>
		UnInitialized = 5,
		/// <summary>
		/// 已卸载
		/// </summary>
		Unload = 4,
		/// <summary>
		/// 类型已加载,但是未初始化
		/// </summary>
		TypeLoaded = 6
	}
}
