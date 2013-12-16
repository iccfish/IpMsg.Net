using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FSLib.IPMessager.Entity;


namespace IPMessagerNet.UI.Controls.HostTreeView
{
	public class HostInfoNodeBase : TreeNode
	{
		/// <summary>
		/// 当前节点显示的主机
		/// </summary>
		public FSLib.IPMessager.Entity.Host Host { get; set; }

		/// <summary>
		/// 图标状态
		/// </summary>
		public enum HostIcon : int
		{
			None = -1,
			UserGroup = 0,
			HostIP = 1,
			HostStatus_Online = 2,
			HostStatus_Absence = 3,
			HostInfo_Name = 4,
			HostInfo_IP = 5,
			HostInfo_Status_Online = 6,
			HostInfo_Status_Absence = 7,
			HostInfo_File_Ok = 8,
			HostInfo_File_Disabled = 9,
			HostInfo_Enc_Disabled = 10,
			HostInfo_Enc_NotInitialize = 11,
			HostInfo_Enc_Ok = 12,
			HostInfo_Share_None = 13,
			HostInfo_Share_Ok = 14,
			HostInfo_Version = 15
		}

		/// <summary>
		/// 设置当前节点的图标
		/// </summary>
		/// <param name="type">图标类型</param>
		protected void SetIcon(HostIcon type)
		{
			this.SelectedImageIndex = this.ImageIndex = (int)type;	
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="host">绑定的客户端</param>
		public HostInfoNodeBase(Host host)
		{
			this.Host = host;
		}
		
	}
}
/*
 * 用户组排序
IP段排序

主机-在线-离开


名称
IPV4地址


在线状态-在线-离开
文件传输-支持-不支持
加密状态-不支持-支持、未初始化-支持、已初始化
文件共享-没有共享-有共享
客户版本
*/
