using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace IPMessagerNet._Embed
{
	/// <summary>
	/// 渐隐渐现控件
	/// </summary>
    [ToolboxItem(true), DefaultEvent("FadeFinished"), DefaultProperty("ParentForm"), DesignerCategory("鱼的控件库"), System.Drawing.ToolboxBitmap(typeof(FadeEffectComponent))]
	public class FadeEffectComponent : System.ComponentModel.Component
	{
		Timer _timer;
		int _opacity;
		FadeDirection _direction;

		/// <summary>
		/// 构造一个实例
		/// </summary>
		public FadeEffectComponent()
		{
			_timer = new Timer();

			TimeInterval = 25;
			EndOpacity = 100;
			StartOpacity = 0;
			_opacitystep = 5;
			FadeInEnabled = FadeOutEnabled = true;

			//绑定定时器事件
			_timer.Tick += _timer_Tick;
		}

		#region 内部处理逻辑

		/// <summary>
		/// 定时器事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void _timer_Tick(object sender, EventArgs e)
		{
			if (_direction == FadeDirection.FadeIn)
			{
				_opacity += _opacitystep;
				if (_opacity <= _endopacity)
				{
					_parentform.Opacity = _opacity / 100.0;
				}
				else
				{
					_opacity = _endopacity;
					_parentform.Opacity = _opacity / 100.0;
					_timer.Stop();
					_timer.Enabled = false;
					if (FadeFinished != null)
						FadeFinished.Invoke(this, new FadeFinishedEventArgs() { Direct = FadeDirection.FadeIn });
				}
			}
			else
			{
				_opacity -= _opacitystep;
				if (_opacity >= _startopacity)
				{
					_parentform.Opacity = _opacity / 100.0;
				}
				else
				{
					_opacity = 0;
					_parentform.Opacity = _opacity / 100.0;
					if (FadeFinished != null)
						FadeFinished.Invoke(this, new FadeFinishedEventArgs() { Direct = FadeDirection.FadeOut });
					_parentform.Close();
				}
			}
		}

		/// <summary>
		/// 判断是否能关闭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void _FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_opacity >= _endopacity && FadeOutEnabled)
			{
				e.Cancel = true; _direction = FadeDirection.FadeOut; _timer.Enabled = true; _timer.Start();
			}
		}

		/// <summary>
		/// 进入时动画
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void _Load(object sender, EventArgs e)
		{
			if (FadeInEnabled)
			{
				_parentform.Opacity = _startopacity / 100.0;
				_timer.Enabled = true; _timer.Start();
			}
			else
			{
				_parentform.Opacity = _endopacity / 100.0;
			}
		}



		#endregion

		#region 属性

		/// <summary>
		/// 开启渐现效果
		/// </summary>
		[DefaultValue(true), DisplayName("开启渐现效果"), Category("Animations")]
		public bool FadeInEnabled { get; set; }

		/// <summary>
		/// 是否开启渐隐效果
		/// </summary>
		[DefaultValue(true), DisplayName("是否开启渐隐效果"), Category("Animations")]
		public bool FadeOutEnabled { get; set; }

		System.Windows.Forms.Form _parentform;
		/// <summary>
		/// 动画所在的窗体
		/// </summary>
		[Description("动画所要实现的窗体"), DisplayName("目标窗体"), Category("Animations")]
		public System.Windows.Forms.Form ParentForm
		{
			get
			{
				return _parentform;
			}
			set
			{
				if (_parentform != null)
				{
					_parentform.Load -= new EventHandler(_Load);
					_parentform.FormClosing -= new FormClosingEventHandler(_FormClosing);
					_parentform.Opacity = _endopacity / 100.0;
				}
				_parentform = value;
				if (_parentform != null && !this.DesignMode)
				{
					_parentform.Load += new EventHandler(_Load);
					_parentform.FormClosing += new FormClosingEventHandler(_FormClosing);
					_parentform.Opacity = 0.0;
				}
			}
		}

		/// <summary>
		/// 动画的周期
		/// </summary>
		[DefaultValue(20), Description("显示窗口的时候刷新透明度的周期"), DisplayName("动画周期"), Category("Animations")]
		public int TimeInterval { get { return _timer.Interval; } set { _timer.Interval = value; } }

		private int _startopacity;
		/// <summary>
		/// 初始化透明度
		/// </summary>
		[DefaultValue(0), Description("窗口显示最开始的时候透明度"), DisplayName("初始化透明度"), Category("Animations")]
		public int StartOpacity
		{
			get
			{
				return _startopacity;
			}
			set
			{
				if (value < 0 || value > 100) { throw new ArgumentOutOfRangeException("StartOpacity"); return; }
				_startopacity = value; _opacity = value;
			}
		}

		private int _endopacity;
		/// <summary>
		/// 最终透明度
		/// </summary>
		[DefaultValue(99), Description("窗口显示最终的时候透明度"), DisplayName("最终透明度"), Category("Animations")]
		public int EndOpacity
		{
			get
			{
				return _endopacity;
			}
			set
			{
				if (value < 0 || value > 100) { throw new ArgumentOutOfRangeException("EndOpacity"); return; }
				_endopacity = value;
			}
		}

		private int _opacitystep;
		/// <summary>
		/// 最终透明度
		/// </summary>
		[DefaultValue(5), Description("窗口一次改变透明度的量"), DisplayName("透明度步进值"), Category("Animations")]
		public int OpacityStep
		{
			get
			{
				return _opacitystep;
			}
			set
			{
				if (value < 0 || value > 100) { throw new ArgumentOutOfRangeException("EndOpacity"); return; }
				_opacitystep = value;
			}
		}

		#endregion

		#region 事件

		public delegate void FadeFinishedEventHandler(object sender, FadeFinishedEventArgs e);
		/// <summary>
		/// 当动画完成的时候触发
		/// </summary>
		public event FadeFinishedEventHandler FadeFinished;

		/// <summary>
		/// 返回是否正在动画
		/// </summary>
		[Browsable(false)]
		public bool IsFadeStoped { get { return !_timer.Enabled; } }

		#endregion
		#region 子类型

		/// <summary>
		/// 动画方向
		/// </summary>
		public enum FadeDirection
		{
			/// <summary>
			/// 渐现
			/// </summary>
			FadeIn = 0,
			/// <summary>
			/// 渐隐
			/// </summary>
			FadeOut = 1
		}


		/// <summary>
		/// 动画完成的事件参数
		/// </summary>
		public class FadeFinishedEventArgs : EventArgs
		{
			/// <summary>
			/// 已完成动画的方向
			/// </summary>
			public FadeDirection Direct { get; set; }
		}

		#endregion
	}
}
