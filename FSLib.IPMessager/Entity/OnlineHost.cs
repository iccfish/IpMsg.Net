using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization;


namespace FSLib.IPMessager.Entity
{
	/// <summary>
	/// 主机在线状态维护类
	/// </summary>
	public class OnlineHost : Dictionary<string, Host>
	{
		#region 线程同步

		System.Threading.SendOrPostCallback sopHostAdd;
		System.Threading.SendOrPostCallback sopHostClear;
		System.Threading.SendOrPostCallback sopHostRemove;

		#endregion

		/// <summary>
		/// 创建一个新的 OnlineHost 对象.
		/// </summary>
		/// <param name="config">使用的配置</param>
		public OnlineHost(Config config/*, Network.MessageTranslator messageProxy*/)
		{
			Config = config;
			//MessageProxy = messageProxy;

			//
			sopHostAdd = (s) =>
			{
				HostOnline(this, s as HostEventArgs);
			};
			sopHostRemove = (s) =>
			{
				HostOffline(this, s as HostEventArgs);
			};
			sopHostClear = (s) =>
			{
				HostCleared(this, s as EventArgs);
			};
		}

		#region 公共方法

		/// <summary>
		/// 返回指定的主机是否支持增强协议模式
		/// </summary>
		/// <param name="ipaddress">远程主机的IP地址</param>
		/// <returns></returns>
		public bool IsEnhancedContractEnabled(string ipaddress)
		{
			if (this.ContainsKey(ipaddress)) return this[ipaddress].IsEnhancedContractEnabled;
			else return false;
		}

		/// <summary>
		/// 返回指定主机的信息
		/// </summary>
		/// <param name="ipaddress">主机地址</param>
		/// <returns></returns>
		public Host GetHost(string ipaddress)
		{
			if (this.ContainsKey(ipaddress)) return this[ipaddress];
			else return null;
		}

		/// <summary>
		/// 添加主机到主机列表中
		/// </summary>
		/// <param name="host">主机信息</param>
		public new void Add(string ipaddress, Host host)
		{
			if (!this.ContainsKey(ipaddress))
			{
				base.Add(ipaddress, host);
				OnHostOnline(new HostEventArgs(host));
			}
		}

		/// <summary>
		/// 将主机从列表中删除
		/// </summary>
		/// <param name="ipaddress">主机地址</param>
		/// <returns>被删除的主机</returns>
		public Host Delete(string ipaddress)
		{
			if (this.ContainsKey(ipaddress))
			{
				Host h = this[ipaddress];
				base.Remove(ipaddress);
				OnHostOffline(new HostEventArgs(h));

				return h;
			}
			else { return null; }
		}

		/// <summary>
		/// 清空主机列表
		/// </summary>
		public new void Clear()
		{
			base.Clear();
			OnHostCleared();
		}

		#endregion

		#region 属性

		/// <summary>
		/// 配置对象
		/// </summary>
		public Config Config { get; set; }

		///// <summary>
		///// 消息通信类
		///// </summary>
		//public Network.MessageTranslator MessageProxy { get; set; }

		#endregion


		#region 事件

		/// <summary>
		/// 主机事件数据类
		/// </summary>
		public class HostEventArgs : EventArgs
		{
			/// <summary>
			/// 主机
			/// </summary>
			public Entity.Host Host { get; set; }

			/// <summary>
			/// 创建一个新的 HostEventArgs 对象.
			/// </summary>
			public HostEventArgs(Entity.Host host)
			{
				Host = host;
			}
		}

		/// <summary>
		/// 主机上线
		/// </summary>
		public event EventHandler<HostEventArgs> HostOnline;

		/// <summary>
		/// 主机下线
		/// </summary>
		public event EventHandler<HostEventArgs> HostOffline;

		/// <summary>
		/// 主机列表清空
		/// </summary>
		public event EventHandler HostCleared;

		/// <summary>
		/// 触发主机上线事件
		/// </summary>
		/// <param name="e">参数</param>
		protected virtual void OnHostOnline(HostEventArgs e)
		{

			if (HostOnline == null) return;

			if (IPMClient.NeedPostMessage)
			{
				IPMClient.SendSynchronizeMessage(sopHostAdd, e);
			}
			else
			{
				HostOnline(this, e);
			}
		}

		/// <summary>
		/// 触发主机下线事件
		/// </summary>
		/// <param name="e">参数</param>
		protected virtual void OnHostOffline(HostEventArgs e)
		{
			if (HostOffline == null) return;

			if (IPMClient.NeedPostMessage)
			{
				IPMClient.SendSynchronizeMessage(sopHostRemove, e);
			}
			else
			{
				HostOffline(this, e);
			}
		}

		/// <summary>
		/// 触发主机列表清空事件
		/// </summary>
		/// <param name="e">参数</param>
		protected virtual void OnHostCleared()
		{
			if (HostCleared == null) return;

			if (IPMClient.NeedPostMessage)
			{
				IPMClient.SendSynchronizeMessage(sopHostClear, null);
			}
			else
			{
				HostCleared(this, EventArgs.Empty);
			}
		}

		#endregion

	}
}
