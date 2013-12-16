using System;
using System.Collections.Generic;
using System.Threading;

namespace FSLib.IPMessager.Entity
{
	/// <summary>
	/// 主机用户信息
	/// </summary>
	/// <remarks></remarks>
	public class Host
	{

		/// <summary>
		/// 索引,貌似没实际的用处(仅用于界面的索引)
		/// </summary>
		/// <remarks></remarks>
		public int Index { get; set; }

		private ulong _hostFeature;

		/// <summary>
		/// 表示客户端主机的功能标识
		/// </summary>
		public ulong HostFeature
		{
			get { return _hostFeature; }
			internal set
			{
				if (value == _hostFeature) return;

				_hostFeature = value;
				OnHostFeatureRefreshed();
			}
		}

		/// <summary>
		/// 主机物理信息
		/// </summary>
		/// <remarks></remarks>
		public HostSub HostSub { get; set; }

		private string _nickName;
		/// <summary>
		/// 昵称
		/// </summary>
		/// <remarks></remarks>
		public string NickName
		{
			get
			{
				if (string.IsNullOrEmpty(_nickName)) return HostSub.UserName;
				return _nickName;
			}
			set
			{
				if (value == null || string.IsNullOrEmpty(value) || _nickName == value) return;

				if (string.IsNullOrEmpty(_nickName))
				{
					_nickName = value;
				}
				else
				{
					_nickName = value;
					OnInfoChanged(new EventArgs());
				}
			}
		}

		private string _groupName;
		/// <summary>
		/// 用户组名称
		/// </summary>
		/// <remarks></remarks>
		public string GroupName
		{
			get
			{
				if (string.IsNullOrEmpty(_groupName)) return "-";
				return _groupName;
			}
			set
			{
				if (value == null || string.IsNullOrEmpty(value) || _groupName == value) return;

				if (string.IsNullOrEmpty(_groupName))
				{
					_groupName = value;
				}
				else
				{
					_groupName = value;
					OnInfoChanged(new EventArgs());
				}
			}
		}

		///// <summary>
		///// 状态,当前未实现
		///// </summary>
		///// <remarks></remarks>
		//public UInt32 HostStatus { get; set; }

		/// <summary>
		/// 离开状态信息
		/// </summary>
		public string AbsenceMessage { get; private set; }
		/// <summary>
		/// 是否在离开状态
		/// </summary>
		public bool IsInAbsenceMode { get; private set; }

		private string _clientVersion;
		/// <summary>
		/// 客户端版本(扩展而来)
		/// </summary>
		/// <remarks></remarks>
		public string ClientVersion
		{
			get
			{
				return _clientVersion;
			}
			set
			{
				_clientVersion = value;
				OnClientVersionResolved(new EventArgs());
			}
		}
		//Public priority As Short
		//Public refCnt As Short

		private byte[] _pubKey;
		/// <summary>
		/// 加密密钥
		/// </summary>
		/// <remarks></remarks>
		public byte[] PubKey
		{
			get
			{
				return _pubKey;
			}
			private set
			{
				_pubKey = value;
				OnPublicKeyChanged(new EventArgs());
			}
		}

		/// <summary>
		/// 向量
		/// </summary>
		public byte[] Exponent { get; private set; }

		/// <summary>
		/// 设置加密信息
		/// </summary>
		public void SetEncryptInfo(byte[] pubKey, byte[] exponent)
		{
			this.PubKey = pubKey;
			this.Exponent = exponent;
		}

		/// <summary>
		/// 是否支持加密通讯
		/// </summary>
		public bool SupportEncrypt { get; set; }


		/// <summary>
		/// 加密能力-是否使用低等级加密
		/// </summary>
		public bool IsEncryptUseSmallKey { get; set; }

		/// <summary>
		/// 是否支持文件传输
		/// </summary>
		public bool SupportFileTransport { get; set; }

		//Public cryptSpec As Short
		/// <summary>
		/// 是否有文件共享(扩展而来)
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool HasShare { get; set; }

		/// <summary>
		/// 是否支持新版本的协议
		/// </summary>
		public bool IsEnhancedContractEnabled { get; set; }

		/// <summary>
		/// 等待公钥而没有发出去的信息
		/// </summary>
		public Queue<Message> QueuedMessage { get; set; }

		/// <summary>
		/// 构造一个新的对象
		/// </summary>
		/// <remarks></remarks>
		public Host()
		{
			ClientVersion = string.Empty;
			HasShare = false;
			IsEnhancedContractEnabled = false;
			AbsenceMessage = "";
		}

		#region 公共函数

		/// <summary>
		/// 设置当前主机的离开信息
		/// </summary>
		/// <param name="isInAbsenceMode">是否处于离开模式</param>
		/// <param name="absenceMessage">离开模式状态信息</param>
		public void ChangeAbsenceMode(bool isInAbsenceMode, string absenceMessage)
		{
			this.AbsenceMessage = isInAbsenceMode ? absenceMessage : "";

			if (this.IsInAbsenceMode != isInAbsenceMode)
			{

				this.IsInAbsenceMode = isInAbsenceMode;

				OnAbsenceModeChanged(new EventArgs());
			}
			else
			{
				OnAbsenceMessageChanged(new EventArgs());
			}
		}

		#endregion

		#region 事件

		/// <summary>
		/// 主机的离开状态变化事件
		/// </summary>
		public event EventHandler AbsenceModeChanged;
		SendOrPostCallback _absenceModeChangedCall;

		/// <summary>
		/// 主机昵称变化事件
		/// </summary>
		public event EventHandler NickNameChanged;
		SendOrPostCallback _nickNameChangedCall;

		/// <summary>
		/// 主机版本获得信息事件
		/// </summary>
		public event EventHandler ClientVersionResolved;

		/// <summary>
		/// 主机公钥变更
		/// </summary>
		public event EventHandler PublicKeyChanged;
		SendOrPostCallback _publicKeyChangedCall;

		/// <summary>
		/// 触发主机的离开状态变化事件
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnAbsenceModeChanged(EventArgs e)
		{
			if (AbsenceModeChanged == null) return;

			if (IPMClient.NeedPostMessage)
			{
				if (_absenceModeChangedCall == null) _absenceModeChangedCall = s => AbsenceModeChanged(this, s as EventArgs);
				IPMClient.SendSynchronizeMessage(_absenceModeChangedCall, e);
			}
			else
			{
				AbsenceModeChanged(this, e);
			}

		}

		/// <summary>
		/// 触发主机昵称变化事件
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnInfoChanged(EventArgs e)
		{
			if (NickNameChanged == null) return;

			if (IPMClient.NeedPostMessage)
			{
				if (_nickNameChangedCall == null) _nickNameChangedCall = s => NickNameChanged(this, s as EventArgs);
				IPMClient.SendSynchronizeMessage(_nickNameChangedCall, e);
			}
			else
			{
				NickNameChanged(this, e);
			}
		}

		/// <summary>
		/// 触发主机公钥变更事件
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnPublicKeyChanged(EventArgs e)
		{
			if (PublicKeyChanged == null) return;

			if (IPMClient.NeedPostMessage)
			{
				if (_publicKeyChangedCall == null) _publicKeyChangedCall = s => PublicKeyChanged(this, s as EventArgs);
				IPMClient.SendSynchronizeMessage(_publicKeyChangedCall, e);
			}
			else
			{
				PublicKeyChanged(this, e);
			}
		}


		SendOrPostCallback _asycnClientVersion;

		/// <summary>
		/// 触发主机版本获得信息事件
		/// </summary>
		/// <param name="e">时间参数</param>
		protected virtual void OnClientVersionResolved(EventArgs e)
		{
			if (ClientVersionResolved == null) return;

			if (IPMClient.NeedPostMessage)
			{
				if (_asycnClientVersion == null) _asycnClientVersion = s => ClientVersionResolved(this, s as EventArgs);
				IPMClient.SendSynchronizeMessage(_asycnClientVersion, e);
			}
			else
			{
				ClientVersionResolved(this, e);
			}
		}

		/// <summary>
		/// 离开状态信息切换
		/// </summary>
		public event EventHandler AbsenceMessageChanged;

		SendOrPostCallback _asyncAbsenceMessageChange;

		/// <summary>
		/// 触发离开状态信息切换事件
		/// </summary>
		/// <param name="e">事件参数</param>
		protected virtual void OnAbsenceMessageChanged(EventArgs e)
		{
			if (AbsenceMessageChanged == null) return;

			if (IPMClient.NeedPostMessage)
			{
				if (_asyncAbsenceMessageChange == null) _asyncAbsenceMessageChange = s => AbsenceMessageChanged(this, s as EventArgs);
				IPMClient.SendSynchronizeMessage(_asyncAbsenceMessageChange, e);
			}
			else
			{
				AbsenceMessageChanged(this, e);
			}
		}

		/// <summary>
		/// 主机功能标识被更新
		/// </summary>
		public event EventHandler HostFeatureRefreshed;

		private SendOrPostCallback _cbHostFeatureRefreshed;

		/// <summary>
		/// 引发 <see cref="HostFeatureRefreshed"/> 事件
		/// </summary>
		protected virtual void OnHostFeatureRefreshed()
		{
			if (HostFeatureRefreshed == null) return;

			if(IPMClient.NeedPostMessage)
			{
				if (_cbHostFeatureRefreshed == null) _cbHostFeatureRefreshed = s => HostFeatureRefreshed(this, s as EventArgs);
				IPMClient.SendSynchronizeMessage(_cbHostFeatureRefreshed, EventArgs.Empty);
			}else
			{
				HostFeatureRefreshed(this, EventArgs.Empty);
			}
		}

		#endregion
	}
}
