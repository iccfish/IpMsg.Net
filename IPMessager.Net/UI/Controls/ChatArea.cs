using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IPMessagerNet.API;
using IPMessagerNet._Embed;

namespace IPMessagerNet.UI.Controls
{
	public partial class ChatArea : UserControl
	{
		public ChatArea()
		{
			InitializeComponent();

			if (Env.ClientConfig == null) return;

			InitToolbarStrip();
			InitTabs();
			InitEditor();
		}



		#region 属性

		/// <summary>
		/// 返回当前会话的数目
		/// </summary>
		public int SessionCount { get { return chatPage.TabPages.Count; } }

		/// <summary>
		/// 会话标签控件
		/// </summary>
		public Chat.ChatContainer ChatControl { get { return chatPage; } }

		#endregion

		#region 事件

		/// <summary>
		/// 会话数量变化
		/// </summary>
		public event EventHandler SessionCountChanged;


		protected virtual void OnSessionCountChanged(EventArgs e)
		{
			if (SessionCountChanged != null) SessionCountChanged(this, e);
		}

		#endregion

		#region 工具栏

		FolderBrowserDialog _sendFile_Fbd;
		OpenFileDialog _sendFile_ofd;

		/// <summary>
		/// 初始化工具栏
		/// </summary>
		void InitToolbarStrip()
		{
			//初始化图像
			int index = 0;
			int temp;
			ControlHelper.FillMenuButtonImage(toolStrip1, ImageHelper.SplitImage(Core.ProfileManager.GetThemePicture("Toolbar", "ChatArea"), 16, 16, out temp).ToArray(), ref index);

			//事件
			this.tChat.Click += (s, e) => { Forms.FrameContainer.ContainerForm.HostTreeView.OpenChatPage(); };
			this.tSendFile.Click += tSendFile_Click;
			this.tSendFolder.Click += tSendFolder_Click;
			this.tBlock.Click += (s, e) => { Forms.FrameContainer.ContainerForm.HostTreeView.BanHost(true); };
			this.tDialup.Click += (s, e) => { Forms.FrameContainer.ContainerForm.HostTreeView.DialUpHost(true); };
			this.tModifyMemo.Click += (s, e) => { Forms.FrameContainer.ContainerForm.HostTreeView.ModifyGroupAndMemo(); };
		}

		void tSendFolder_Click(object sender, EventArgs e)
		{
			if (ChatControl.SelectedChatPage == null) return;

			if (_sendFile_Fbd == null) _sendFile_Fbd = new FolderBrowserDialog() { Description = "选择要发送的文件夹" };
			if (_sendFile_Fbd.ShowDialog() == DialogResult.OK) ChatControl.SelectedChatPage.AddFileToSendList(_sendFile_Fbd.SelectedPath);
		}

		void tSendFile_Click(object sender, EventArgs e)
		{
			if (ChatControl.SelectedChatPage == null) return;

			if (_sendFile_ofd == null) _sendFile_ofd = new OpenFileDialog() { Filter = "所有文件(*.*)|*.*", Multiselect = true, Title = "选择要发送的文件" };
			if (_sendFile_ofd.ShowDialog() == DialogResult.OK)
			{
				Array.ForEach(_sendFile_ofd.FileNames, s => ChatControl.SelectedChatPage.AddFileToSendList(s));
			}
		}


		#endregion


		#region 聊天标签

		void InitTabs()
		{
			chatPage.SessionCountChanged += (s, e) =>
			{
				OnSessionCountChanged(e);
				editor.Enabled = chatPage.SelectedIndex != -1 && chatPage.SelectedTab is IChatService;
			};
			chatPage.SelectedIndexChanged += (s, e) =>
			{
				editor.Enabled = chatPage.SelectedIndex != -1 && chatPage.SelectedTab is IChatService;
			};
		}

		#endregion

		#region 编辑器

		/// <summary>
		/// 编辑器
		/// </summary>
		IEditorService editor;

		/// <summary>
		/// 初始化编辑器
		/// </summary>
		void InitEditor()
		{
			editor = Core.ServiceManager.GetEditor();
			this.chatContainer.Panel2.Controls.Add(editor as Control);

			//初始化编辑器属性
			editor.Enabled = false;

			//初始化编辑器事件
			editor.TextMessageSendRequired += es_TextMessageSendRequired;
		}

		/// <summary>
		/// 请求发送消息
		/// </summary>
		void es_TextMessageSendRequired(object sender, EventArgs e)
		{
			IChatService cs = chatPage.SelectedChatPage;
			if (cs == null) return;

			//发送信息
			Env.IPMClient.Commander.SendTextMessage(cs.Host, editor.Content, editor.IsHtml, editor.IsRTF, false, false);
			//显示给自己
			chatPage.MessageSend(editor.Content);
		}


		#endregion
	}
}
