using System;

namespace IPMessagerNet._Embed
{
	/// <summary>
	/// 进度表示抽象类
	/// </summary>
	public class ProgressIdentifier
	{
		/// <summary>
		/// Initializes a new instance of the ProgressIdentifier class.
		/// </summary>
		public ProgressIdentifier()
		{
			ctx = System.Threading.SynchronizationContext.Current;
			if (ctx == null) ctx = new System.Threading.SynchronizationContext();

			EventArgs e = new EventArgs();
			spcCurrentValueChanged = s => CurrentValueChanged(this, e);
			spcMaxValueChanged = s => MaxValueChanged(this, e);
			spcStateMessageChanged = s => StateMessageChanged(this, e);
			spcStateObjChanged = s => StateObjChanged(this, e);
		}

		#region Events

		System.Threading.SynchronizationContext ctx;

		//ASynchronization
		System.Threading.SendOrPostCallback spcMaxValueChanged, spcCurrentValueChanged, spcStateMessageChanged, spcStateObjChanged;

		/// <summary>
		/// 最大值更改事件
		/// </summary>
		public event EventHandler MaxValueChanged;

		/// <summary>
		/// Triggers the MaxValueChanged event.
		/// </summary>
		public virtual void OnMaxValueChanged()
		{
			if (MaxValueChanged != null)
				ctx.Send(spcMaxValueChanged, null);
		}

		/// <summary>
		/// 当前进度更改事件
		/// </summary>
		public event EventHandler CurrentValueChanged;

		/// <summary>
		/// Triggers the MaxValueChanged event.
		/// </summary>
		public virtual void OnCurrentValueChanged()
		{
			if (CurrentValueChanged != null)
				ctx.Send(spcCurrentValueChanged, null);
		}

		/// <summary>
		/// 进度信息更改事件
		/// </summary>
		public event EventHandler StateMessageChanged;

		/// <summary>
		/// Triggers the MaxValueChanged event.
		/// </summary>
		public virtual void OnStateMessageChanged()
		{
			if (StateMessageChanged != null)
				ctx.Send(spcStateMessageChanged, null);
		}

		/// <summary>
		/// 状态对象更改事件
		/// </summary>
		public event EventHandler StateObjChanged;

		/// <summary>
		/// Triggers the MaxValueChanged event.
		/// </summary>
		public virtual void OnStateObjChanged()
		{
			if (StateObjChanged != null)
				ctx.Send(spcStateObjChanged, null);
		}

		/// <summary>
		/// 自定义报告事件数据
		/// </summary>
		public class ProgressIdentifierEventEntry
		{

			/// <summary>
			/// 事件信息
			/// </summary>
			public string Message { get; set; }

			/// <summary>
			/// 数据是否有效
			/// </summary>
			public bool IsValid { get { return !string.IsNullOrEmpty(Message); } }

		}

		#endregion


		private int _maxValue;
		/// <summary>
		/// 最大值
		/// </summary>
		public int MaxValue
		{
			get
			{
				return _maxValue;
			}
			set
			{
				if (_maxValue == value) return;
				_maxValue = value;
				if (_currentValue > _maxValue) _currentValue = _maxValue;
				OnMaxValueChanged();
			}
		}

		private int _currentValue;
		/// <summary>
		/// 当前值
		/// </summary>
		public int CurrentValue
		{
			get
			{
				return _currentValue;
			}
			set
			{
				if (_currentValue == value) return;
				_currentValue = value;
				if (_currentValue > MaxValue) _currentValue = MaxValue;
				OnCurrentValueChanged();
			}
		}

		private string _stateMessage;
		/// <summary>
		/// 状态信息
		/// </summary>
		public string StateMessage
		{
			get
			{
				return _stateMessage;
			}
			set
			{
				if (value == null ^ _stateMessage == null)
				{
					_stateMessage = value;
					OnStateMessageChanged();
				}
				else if (value != null && value != _stateMessage)
				{
					_stateMessage = value;
					OnStateMessageChanged();
				}
			}
		}

		private object _stateObject;
		/// <summary>
		/// 状态对象
		/// </summary>
		public object StateObject
		{
			get
			{
				return _stateObject;
			}
			set
			{
				_stateObject = value;
				if (value == null ^ _stateObject == null)
				{
					_stateObject = value;
					OnStateObjChanged();
				}
				else if (value != null && value != _stateObject)
				{
					_stateObject = value;
					OnStateObjChanged();
				}
			}
		}

		/// <summary>
		/// 线程后台工作组件
		/// </summary>
		public System.ComponentModel.BackgroundWorker WorkerManager { get; internal set; }

		/// <summary>
		/// 线程对象数据
		/// </summary>
		public System.ComponentModel.DoWorkEventArgs WorkingData { get; internal set; }

		/// <summary>
		/// 直接触发状态对象更换事件
		/// </summary>
		public void NotifyStateObjChanged()
		{
			OnStateObjChanged();
		}

		/// <summary>
		/// 直接触发状态对象更换事件
		/// </summary>
		public void NotifyStateObjChanged(string entryMessage)
		{
			if (string.IsNullOrEmpty(entryMessage)) throw new ArgumentOutOfRangeException();

			if (StateObject == null || !(StateObject is ProgressIdentifierEventEntry)) StateObject = new ProgressIdentifierEventEntry();
			ProgressIdentifierEventEntry piee = StateObject as ProgressIdentifierEventEntry;
			piee.Message = entryMessage;

			OnStateObjChanged();
		}

		/// <summary>
		/// 当前的进度百分比
		/// </summary>
		public int Percentage
		{
			get
			{
				if (MaxValue == 0) return 0;
				else return (int)Math.Floor(CurrentValue * 100.0 / MaxValue);
			}
		}
	}
}
