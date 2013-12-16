using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSLib.IPMessager.Services
{
	/// <summary>
	/// 提供的服务基类
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ProviderBase<T> : ProviderBase where T : class
	{
		/// <summary>
		/// 配置（已重载）
		/// </summary>
		public new T ProviderConfig
		{
			get
			{
				return base.ProviderConfig as T;
			}
			set
			{
				base.ProviderConfig = value;
			}
		}

		/// <summary>
		/// 加载配置（已重载）
		/// </summary>
		protected virtual void LoadConfig()
		{
			base.LoadConfig<T>();
		}
	}
}
