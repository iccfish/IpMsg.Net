using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPMessagerNet.Plugins
{
	/// <summary>
	/// 插件接口对象，用于提供插件规范化的实现
	/// </summary>
	public interface IPlugins
	{
		#region 插件信息

		/// <summary>
		/// 插件作者
		/// </summary>
		string Author { get; }

		/// <summary>
		/// 联系方式
		/// </summary>
		string Contact { get; }

		/// <summary>
		/// 版本信息
		/// </summary>
		string VersionInfo { get; }

		/// <summary>
		/// 名称
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// 描述
		/// </summary>
		string Description { get; set; }

		#endregion

		/// <summary>
		/// 初始化插件
		/// </summary>
		void Init();
	}
}
