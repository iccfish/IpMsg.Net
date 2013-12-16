using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FSLib.IPMessager.Services
{
	/// <summary>
	/// 配置相关事件
	/// </summary>
	public class ServiceConfigEventArgs : EventArgs
	{
		/// <summary>
		/// 配置对象
		/// </summary>
		public object ConfigObject { get; set; }

		/// <summary>
		/// 是否已经处理
		/// </summary>
		public bool IsHandled { get; set; }

		private Type _type;
		/// <summary>
		/// 配置类型
		/// </summary>
		public Type Type
		{
			get
			{
				if (_type == null && ConfigObject != null) _type = ConfigObject.GetType();

				return _type;
			}
			set
			{
				_type = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the ConfigEventArgs class.
		/// </summary>
		public ServiceConfigEventArgs()
		{
			IsHandled = false;
		}
	}
}
