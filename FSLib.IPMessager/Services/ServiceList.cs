using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSLib.IPMessager.Services
{
	/// <summary>
	/// 服务集合
	/// </summary>
	public class ServiceList : List<ServiceInfo>
	{
		/// <summary>
		/// 对集合执行操作
		/// </summary>
		/// <param name="action">要执行的函数</param>
		public void ProviderExecute(Action<IServiceProvider> action)
		{
			this.ForEach(s => {
				if (s.ServiceProvider == null) return;
				action(s.ServiceProvider);
			});
		}

	}
}
