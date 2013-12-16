using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FSLib.IPMessager.Services
{
	/// <summary>
	/// 服务提供标记
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public class ServiceAttribute : System.Attribute
	{
		/// <summary>
		/// 创建 ServiceAttribute class 的新实例
		/// </summary>
		public ServiceAttribute(string author, string contact, string name, string copyRight, string description)
		{
			Description = new ServiceDescription(author, contact, name, copyRight, description);
		}

		/// <summary>
		/// 创建 ServiceAttribute class 的新实例
		/// </summary>
		public ServiceAttribute(string author, string contact, string name, string copyRight, string description, bool defaultEnabled)
		{
			Description = new ServiceDescription(author, contact, name, copyRight, description, defaultEnabled);
		}

		/// <summary>
		/// 描述
		/// </summary>
		public ServiceDescription Description { get; set; }


	}
}
