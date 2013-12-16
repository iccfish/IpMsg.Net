using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FSLib.IPMessager;
using IPMessagerNet._Embed;

namespace IPMessagerNet.UI.Dialogs.Notify
{
	public partial class FetchFolderInfo : Form
	{
		Size orgSize;
		bool canClose = false;

		public FetchFolderInfo()
		{
			InitializeComponent();

			this.Load += FetchFolderInfo_Load;
			this.SizeChanged += FetchFolderInfo_SizeChanged;
			this.orgSize = this.Size;
			this.FormClosing += (s, e) => { e.Cancel = !canClose; };
			this.silder.SlideEnd += (s, e) =>
			{
				if (e.Direction == SlideComponent.SlideDirection.SlideIn) BuildExtendMessage();
			};
		}

		void FetchFolderInfo_SizeChanged(object sender, EventArgs e)
		{
			this.Size = orgSize;
		}

		void FetchFolderInfo_Load(object sender, EventArgs e)
		{
			//this.pbLoading.Image = Image.FromFile(Core.ProfileManager.GetThemePicturePath("Animation", "loading.gif"));
		}

		/// <summary>
		/// 文件列表
		/// </summary>
		public string[] FileList { get; set; }

		/// <summary>
		/// 是否计算文件夹体积
		/// </summary>
		public bool CalculateFolder { get; set; }
		/// <summary>
		/// 任务条目
		/// </summary>
		public FSLib.IPMessager.Entity.FileTaskItem[] TaskItems { get; set; }

		/// <summary>
		/// 创建带有文件信息的扩展信息
		/// </summary>
		/// <returns></returns>
		public void BuildExtendMessage()
		{
			Dictionary<string, long> sizeDefine = new Dictionary<string, long>();

			foreach (var fileName in FileList)
			{
				if (System.IO.File.Exists(fileName))
				{
					System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
					if (fileInfo.Length == 0) continue;

					sizeDefine.Add(fileName, fileInfo.Length);
				}
				else
				{
					sizeDefine.Add(fileName, 0L);
				}
			}

			if (CalculateFolder)
			{
				//启动独立的线程计算
				BackgroundWorker bgw = new BackgroundWorker();
				bgw.RunWorkerCompleted += (s, e) =>
				{
					BuildMessage(sizeDefine);
					canClose = true;
					this.Close();
				};
				updater = (s) =>
				{
					this.statusText.Text = String.Format("正在扫描 {0}", System.IO.Path.GetFileName(s));
				};
				bgw.DoWork += bgw_DoWork;
				bgw.RunWorkerAsync(sizeDefine);
			}
			else
			{
				BuildMessage(sizeDefine);
				canClose = true;
				this.Close();
			}
		}

		/// <summary>
		/// 创建信息
		/// </summary>
		/// <param name="list"></param>
		void BuildMessage(Dictionary<string, long> list)
		{
			TaskItems = list.BuildTaskList();
		}

		#region 多线程处理段


		void bgw_DoWork(object sender, DoWorkEventArgs e)
		{
			Dictionary<string, long> sizeDefine = e.Argument as Dictionary<string, long>;

			foreach (var key in sizeDefine.Keys.ToList())
			{
				if (sizeDefine[key] > 0) continue;
				sizeDefine[key] = -CalculateFolderSize(key);
			}
		}

		Action<string> updater;

		long CalculateFolderSize(string path)
		{
			if (!System.IO.Directory.Exists(path)) return 0;
			long size = 0L;

			this.Invoke(updater, path);

			string[] temp = null;
			try
			{
				temp = System.IO.Directory.GetDirectories(path);
			}
			catch (Exception)
			{
				return 0;
			}
			Array.ForEach(temp, s => size += CalculateFolderSize(s));

			temp = System.IO.Directory.GetFiles(path);
			Array.ForEach(temp, s =>
			{
				System.IO.FileInfo fi = new System.IO.FileInfo(s);
				size += fi.Length;
				fi = null;
			});

			return size;
		}

		#endregion
	}
}
