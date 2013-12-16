using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace IPMessagerNet.UI.EditorControls
{
	public class TextBoxEditor:TextBox
	{
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
		public Func<string, bool> CheckAvailability;


		PropertyInfo _bindingProperty;

		/// <summary>
		/// 绑定数据
		/// </summary>
		void CheckDataBinding()
		{
			if (string.IsNullOrEmpty(_dataMemberName) || _dataInstance == null) return;
			Type objType = _dataInstance.GetType();
			_bindingProperty = objType.GetProperty(_dataMemberName, typeof(string));
			if (_bindingProperty == null) throw new InvalidOperationException("无法获得属性,请检查数据对象和成员名设置");

			this.Text = (string)_bindingProperty.GetValue(_dataInstance, null);
		}

		/// <summary>
		/// 是否允许空值
		/// </summary>
		public bool AllowBlank { get; set; }

		#endregion

		/// <summary>
		/// Initializes a new instance of the TextBoxEditor class.
		/// </summary>
		public TextBoxEditor()
		{
			this.TextChanged += TextBoxEditor_TextChanged;
		}

		void TextBoxEditor_TextChanged(object sender, EventArgs e)
		{
			if (_bindingProperty == null) return;

			if (!AllowBlank && string.IsNullOrEmpty(Text))
			{
				MessageBox.Show("这项不能为空，请重新填写", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			if (CheckAvailability != null && !CheckAvailability(this.Text)) return;
			_bindingProperty.SetValue(_dataInstance, this.Text, null);

		}
	}
}
