using System;
using System.Windows.Forms;

namespace IPMessagerNet._Embed
{
	public class FunctionalUserControl : UserControl
	{
		#region 消息函数重载

		/// <summary>
		/// 显示信息对话框
		/// </summary>
		/// <param name="content">要显示的内容</param>
		public static void Information(string content)
		{
			Information("提示", content);
		}
		/// <summary>
		/// 显示信息对话框
		/// </summary>
		/// <param name="title">要显示的标题</param>
		/// <param name="content">要显示的内容</param>
		public static void Information(string title, string content)
		{
			MessageBox.Show(content, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// 显示错误对话框
		/// </summary>
		/// <param name="content">要显示的内容</param>
		public static void Error(string content)
		{
			Error("错误", content);
		}
		/// <summary>
		/// 显示错误对话框
		/// </summary>
		/// <param name="title">要显示的标题</param>
		/// <param name="content">要显示的内容</param>
		public static void Error(string title, string content)
		{
			MessageBox.Show(title, content, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}


		/// <summary>
		/// 显示停止对话框
		/// </summary>
		/// <param name="content">要显示的内容</param>
		public static void Stop(string content)
		{
			Stop("错误", content);
		}

		/// <summary>
		/// 显示停止对话框
		/// </summary>
		/// <param name="title">要显示的标题</param>
		/// <param name="content">要显示的内容</param>
		public static void Stop(string title, string content)
		{
			MessageBox.Show(title, content, MessageBoxButtons.OK, MessageBoxIcon.Stop);
		}

		/// <summary>
		/// 提示对话框
		/// </summary>
		/// <param name="content">提示内容</param>
		/// <param name="isYesNo">提示内容，true是 “是/否”，false为“确定”、“取消”</param>
		/// <returns></returns>
		public static bool Question(string content, bool isYesNo)
		{
			return Question(content, "确定", isYesNo);
		}

		/// <summary>
		/// 提示对话框
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">提示内容</param>
		/// <param name="isYesNo">提示内容，true是 “是/否”，false为“确定”、“取消”</param>
		/// <returns></returns>
		public static bool Question(string title, string content, bool isYesNo)
		{
			return MessageBox.Show(title, content, isYesNo ? MessageBoxButtons.YesNo : MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == (isYesNo ? DialogResult.Yes : DialogResult.OK);
		}



		/// <summary>
		/// 提示对话框（带有取消）
		/// </summary>
		/// <param name="content">提示内容</param>
		/// <returns></returns>
		public static DialogResult QuestionWithCancel(string content)
		{
			return QuestionWithCancel("确定", content);
		}

		/// <summary>
		/// 提示对话框（带有取消）
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">提示内容</param>
		/// <returns></returns>
		public static DialogResult QuestionWithCancel(string title, string content)
		{
			return MessageBox.Show(title, content, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
		}

		/// <summary>
		/// 重试
		/// </summary>
		/// <param name="title"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public static bool RetryError(string title, string content)
		{
			return MessageBox.Show(title, content, MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Retry;
		}

		/// <summary>
		/// 重试
		/// </summary>
		/// <param name="title"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public static bool RetryCommon(string title, string content)
		{
			return MessageBox.Show(title, content, MessageBoxButtons.RetryCancel, MessageBoxIcon.Question) == DialogResult.Retry;
		}



		/// <summary>
		/// 创建提示输入对话框
		/// </summary>
		/// <param name="description">描述</param>
		/// <returns></returns>
		public static InputBox CreateInputBox(string description)
		{
			return CreateInputBox(String.Empty, description, String.Empty);
		}

		/// <summary>
		/// 创建提示输入对话框
		/// </summary>
		/// <param name="title">对话框标题</param>
		/// <param name="description">描述</param>
		/// <param name="defaultValue">默认值</param>
		/// <returns></returns>
		public static InputBox CreateInputBox(string title, string description, string defaultValue)
		{
			return CreateInputBox(title, description, defaultValue, false);
		}

		/// <summary>
		/// 创建提示输入对话框
		/// </summary>
		/// <param name="title">对话框标题</param>
		/// <param name="description">描述</param>
		/// <returns></returns>
		public static InputBox CreateInputBox(string title, string description)
		{
			return CreateInputBox(title, description, false);
		}

		/// <summary>
		/// 创建提示输入对话框
		/// </summary>
		/// <param name="title">对话框标题</param>
		/// <param name="description">描述</param>
		/// <param name="allowBlank">允许空</param>
		/// <returns></returns>
		public static InputBox CreateInputBox(string title, string description, bool allowBlank)
		{
			return CreateInputBox(title, description, String.Empty, allowBlank);
		}

		/// <summary>
		/// 创建提示输入对话框
		/// </summary>
		/// <param name="title">对话框标题</param>
		/// <param name="description">描述</param>
		/// <param name="defaultValue">默认值</param>
		/// <param name="allowBlank">允许空</param>
		/// <returns></returns>
		public static InputBox CreateInputBox(string title, string description, string defaultValue, bool allowBlank)
		{
			return new InputBox()
			{
				Text = title,
				TipMessage = description,
				InputedText = defaultValue,
				AllowBlank = allowBlank
			};
		}

		#endregion
	}
}
