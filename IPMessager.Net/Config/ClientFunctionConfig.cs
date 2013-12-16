using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPMessagerNet.Config
{
	/// <summary>
	/// 客户端功能设置
	/// </summary>
	[Serializable]
	public class ClientFunctionConfig
	{
		/// <summary>
		/// 发送文件时是否计算文件夹体积
		/// </summary>
		public bool Share_CalculateFolderSize { get; set; }

		/// <summary>
		/// 最后保存的路径
		/// </summary>
		public string Share_LastSelectedPath { get; set; }

		/// <summary>
		/// 对同一个消息包中的文件保存到同一个目录中
		/// </summary>
		public bool Share_UseSameLocationToSave { get; set; }
	}
}
