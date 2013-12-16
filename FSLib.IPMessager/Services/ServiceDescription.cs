using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FSLib.IPMessager.Services
{
	/// <summary>
	/// 插件信息
	/// </summary>
	[Serializable]
	public class ServiceDescription
	{
		/// <summary>
		/// 创建 ServiceDescription class 的新实例
		/// </summary>
		public ServiceDescription()
		{
		}

		/// <summary>
		/// 创建 ServiceAttribute class 的新实例
		/// </summary>
		public ServiceDescription(string author, string contact, string name, string copyRight, string description)
		{
			Author = author;
			Contact = contact;
			Name = name;
			CopyRight = copyRight;
			Description = description;
			DefaultEnabled = true;
		}

		/// <summary>
		/// 创建 ServiceAttribute class 的新实例
		/// </summary>
		public ServiceDescription(string author, string contact, string name, string copyRight, string description, bool defaultEnabled)
		{
			Author = author;
			Contact = contact;
			Name = name;
			CopyRight = copyRight;
			Description = description;
			DefaultEnabled = defaultEnabled;
		}

		/// <summary>
		/// 作者
		/// </summary>
		public string Author { get; set; }

		/// <summary>
		/// 联系方式
		/// </summary>
		public string Contact { get; set; }

		/// <summary>
		/// 服务名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 版权标记
		/// </summary>
		public string CopyRight { get; set; }

		/// <summary>
		/// 功能描述
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// 是否默认开启
		/// </summary>
		public bool DefaultEnabled { get; set; }

	}
}

