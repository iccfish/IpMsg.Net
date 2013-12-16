using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IPMessagerNet._Embed
{
	public partial class WaitingDialog : FunctionalForm
	{
		BackgroundWorker bgw;

		public WaitingDialog()
		{
			InitializeComponent();

			bgw = new BackgroundWorker();
			bgw.RunWorkerCompleted += (s, e) =>
			{
				WorkingResult = e;
				if (WorkComplete != null) WorkComplete(this, e);
				if (AutoClose) Close();
			};
			bgw.DoWork += (s, e) =>
			{
				Progress.WorkingData = e;
				if (ThreadWorker != null) ThreadWorker(Progress);
				if (CloseDelay > 0) System.Threading.Thread.Sleep(CloseDelay);
			};

			Progress = new ProgressIdentifier()
			{
				WorkerManager = bgw
			};

			Progress.CurrentValueChanged += (s, e) => { pgUpdate.Value = Progress.CurrentValue; };
			Progress.MaxValueChanged += (s, e) =>
			{
				pgUpdate.Style = Progress.MaxValue == 0 ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
				pgUpdate.Maximum = Progress.MaxValue;
			};
			Progress.StateMessageChanged += (s, e) =>
			{
				if (!string.IsNullOrEmpty(Progress.StateMessage))
				{
					lblMessage.Text = Progress.StateMessage;
					if (ShowLog)
					{
						var lvi = new ListViewItem(DateTime.Now.ToShortTimeString());
						lvi.SubItems.Add(Progress.StateMessage);
						logList.Items.Add(lvi);
						lvi.EnsureVisible();
					}
				}
			};
			Progress.StateObjChanged += (s, e) =>
			{
				if (!ShowLog || Progress.StateObject == null) return;

				if (Progress.StateObject is ProgressIdentifier.ProgressIdentifierEventEntry)
				{
					ProgressIdentifier.ProgressIdentifierEventEntry piee = Progress.StateObject as ProgressIdentifier.ProgressIdentifierEventEntry;
					if (!piee.IsValid) return;

					var lvi = new ListViewItem(DateTime.Now.ToShortTimeString());
					lvi.SubItems.Add(Progress.StateMessage);
					logList.Items.Add(lvi);
					lvi.EnsureVisible();
				}
			};



			AutoClose = true;
			CloseDelay = 1000;

			var fader = new FadeEffectComponent()
			{
				ParentForm = this
			};
			fader.FadeFinished += (s, e) =>
			{
				if (e.Direct != FadeEffectComponent.FadeDirection.FadeIn) return;
				RunWorker();
			};

			this.StartPosition = FormStartPosition.CenterScreen;
		}

		/// <summary>
		/// 启动操作
		/// </summary>
		void RunWorker()
		{
			Progress.CurrentValue = Progress.MaxValue = 0;
			this.pgUpdate.Style = ProgressBarStyle.Marquee;
			Progress.StateObject = null;
			bgw.RunWorkerAsync(WorkerData);
		}

		/// <summary>
		/// 传递给工作线程的参数
		/// </summary>
		public object WorkerData { get; set; }

		/// <summary>
		/// 工作返回结果
		/// </summary>
		public RunWorkerCompletedEventArgs WorkingResult { get; set; }

		/// <summary>
		/// 进度对象
		/// </summary>
		public ProgressIdentifier Progress { get; set; }

		/// <summary>
		/// 工作线程启动
		/// </summary>
		public Action<ProgressIdentifier> ThreadWorker { get; set; }

		/// <summary>
		/// 完成后关闭窗口延迟
		/// </summary>
		public int CloseDelay { get; set; }

		/// <summary>
		/// 是否自动关闭窗口
		/// </summary>
		public bool AutoClose { get; set; }

		/// <summary>
		/// 工作完成事件
		/// </summary>
		public event RunWorkerCompletedEventHandler WorkComplete;

		/// <summary>
		/// 获取或设置当前操作的说明
		/// </summary>
		public string ActionName
		{
			get
			{
				return lblOperation.Text;
			}
			set
			{
				lblOperation.Text = value;
			}
		}

		/// <summary>
		/// 是否显示日志窗口
		/// </summary>
		public bool ShowLog
		{
			get
			{
				return logList.Visible;
			}
			set
			{
				if (value)
				{
					logList.Visible = true;
					this.MinimumSize = this.MaximumSize = this.Size = new Size(this.Width, 220);
				}
				else
				{
					logList.Visible = false;
					this.MinimumSize = this.MaximumSize = this.Size = new Size(this.Width, 110);
				}
			}
		}
	}
}
