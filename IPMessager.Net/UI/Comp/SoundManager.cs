using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;

namespace IPMessagerNet.UI.Comp
{
	/// <summary>
	/// 为应用程序提供声音服务
	/// </summary>
	class SoundManager:IDisposable
	{
		#region IDisposable 成员

		bool disposed = false;

		void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!disposed)
				{
					_onlinePlayer.Dispose();
					_offlinePlayer.Dispose();
					_newmsgPlayer.Dispose();
					_newfilePlayer.Dispose();
					_filesuccPlayer.Dispose();
					_fileErrorPlayer.Dispose();
				}
				//Finalize unmanged objects

				disposed = true;
			}
		}

		/// <summary>
		/// 释放对象
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// 析构函数
		/// </summary>
		~SoundManager()
		{
			Dispose(false);
		}

		#endregion

		#region 声音播放

		SoundPlayer _onlinePlayer;
		SoundPlayer _offlinePlayer;
		SoundPlayer _newmsgPlayer;
		SoundPlayer _newfilePlayer;
		SoundPlayer _filesuccPlayer;
		SoundPlayer _fileErrorPlayer;

		#endregion

		/// <summary>
		/// Initializes a new instance of the SoundManager class.
		/// </summary>
		public SoundManager()
		{
			_onlinePlayer = new SoundPlayer(Core.ProfileManager.GetThemeFilePath("sounds", "online.wav"));
			_offlinePlayer = new SoundPlayer(Core.ProfileManager.GetThemeFilePath("sounds", "offline.wav"));
			_newmsgPlayer = new SoundPlayer(Core.ProfileManager.GetThemeFilePath("sounds", "newmsg.wav"));
			_newfilePlayer = new SoundPlayer(Core.ProfileManager.GetThemeFilePath("sounds", "newfile.wav"));
			_filesuccPlayer = new SoundPlayer(Core.ProfileManager.GetThemeFilePath("sounds", "filesucc.wav"));
			_fileErrorPlayer = new SoundPlayer(Core.ProfileManager.GetThemeFilePath("sounds", "fileerror.wav"));
		}

		/// <summary>
		/// 上线提示声音
		/// </summary>
		public void PlayOnline()
		{
			if (disposed) return;
			_onlinePlayer.Play();
		}

		/// <summary>
		/// 下线提示声音
		/// </summary>
		public void PlayOffline()
		{
			if (disposed) return;
			_offlinePlayer.Play();
		}

		/// <summary>
		/// 新信息提示声音
		/// </summary>
		public void PlayNewMsg()
		{
			if (disposed) return;
			_newmsgPlayer.Play();
		}

		/// <summary>
		/// 新文件提示声音
		/// </summary>
		public void PlayNewFile()
		{
			if (disposed) return;
			_newfilePlayer.Play();
		}

		/// <summary>
		/// 文件传输成功提示声音
		/// </summary>
		public void PlayFileSucc()
		{
			if (disposed) return;
			_filesuccPlayer.Play();
		}

		/// <summary>
		/// 文件传输失败提示声音
		/// </summary>
		public void PlayFileError()
		{
			if (disposed) return;
			_fileErrorPlayer.Play();
		}
	}
}
