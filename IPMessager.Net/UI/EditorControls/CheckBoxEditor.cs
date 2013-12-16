using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace IPMessagerNet.UI.EditorControls
{
	public class CheckBoxEditor : CheckBox
	{
		/// <summary>
		/// Initializes a new instance of the CheckBoxEditor class.
		/// </summary>
		public CheckBoxEditor()
		{
			this.CheckedChanged += CheckBoxEditor_CheckedChanged;
		}

		#region 绑定自身事件

		void CheckBoxEditor_CheckedChanged(object sender, EventArgs e)
		{
			if (_bindingProperty == null) return;

			if (CheckAvailability != null && !CheckAvailability(this.Checked)) return;
			_bindingProperty.SetValue(_dataInstance, this.Checked, null);
		}

		#endregion

		#region 数据绑定

		private string _dataMemberName;
		/// <summary>
		/// 数据属性名
		/// </summary>
		public string DataMemberName
		{
			get
			{
				return _dataMemberName;
			}
			set
			{
				if (string.IsNullOrEmpty(value)) return;
				if (!string.IsNullOrEmpty(_dataMemberName) && _dataMemberName == value) return;

				_dataMemberName = value;
				CheckDataBinding();
			}
		}

		private object _dataInstance;
		/// <summary>
		/// 数据对象
		/// </summary>
		public object DataInstance
		{
			get
			{
				return _dataInstance;
			}
			set
			{
				if (value == null) return;
				if (_dataInstance != null && _dataInstance == value) return;

				_dataInstance = value;
				CheckDataBinding();
			}
		}

		/// <summary>
		/// 验证数据是否合法
		/// </summary>
		/// <returns></returns>
		public Func<bool, bool> CheckAvailability;


		PropertyInfo _bindingProperty;

		/// <summary>
		/// 绑定数据
		/// </summary>
		void CheckDataBinding()
		{
			if (string.IsNullOrEmpty(_dataMemberName) || _dataInstance == null) return;
			Type objType = _dataInstance.GetType();
			_bindingProperty = objType.GetProperty(_dataMemberName, typeof(bool));
			if (_bindingProperty == null) throw new InvalidOperationException("无法获得属性,请检查数据对象和成员名设置");

			this.Checked = (bool)_bindingProperty.GetValue(_dataInstance, null);
		}

		#endregion
	}
}
