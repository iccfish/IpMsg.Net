using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using FSLib.IPMessager.Entity;
using FSLib.IPMessager.Core;
using System.Threading;
using FSLib.IPMessager.Define;
using System.IO;
using System.Text.RegularExpressions;

namespace FSLib.IPMessager.Network
{
	/// <summary>
	/// 文件发送、接收线程
	/// </summary>
	public class TCPThread : IDisposable
	{
		#region 私有变量

		TcpListener listener;
		IPMClient _client;

		#endregion


		internal TCPThread(IPMClient client)
		{
			IsInitialized = false;
			_client = client;
			this.Config = client.Config;
			Config.EnableFileTransfer = false;

			try
			{
				listener = new TcpListener(this.IP, this.Port)
				{
				};
				StartListener();
				IsInitialized = true;
				Config.EnableFileTransfer = true;
			}
			catch (Exception)
			{
				OnNetworkError(new EventArgs());
			}
		}


		#region 属性

		/// <summary>
		/// 是否已经初始化
		/// </summary>
		public bool IsInitialized { get; private set; }

		/// <summary>
		/// 监听的端口
		/// </summary>
		public int Port { get { return Config.Port; } }

		/// <summary>
		/// 绑定的IP
		/// </summary>
		public IPAddress IP { get { return Config.BindedIP; } }

		private FileTaskManager _taskManager;
		/// <summary>
		/// 任务管理器
		/// </summary>
		public FileTaskManager TaskManager
		{
			get
			{
				return _taskManager;
			}
			set
			{
				if (_taskManager != null) _taskManager.FileReceiveRequired -= _taskManager_FileReceiveRequired;
				_taskManager = value;
				if (_taskManager != null) _taskManager.FileReceiveRequired += _taskManager_FileReceiveRequired;
			}
		}

		/// <summary>
		/// 发送缓冲区大小
		/// </summary>
		public int SendBuffer { get { return Config.Buffer; } }

		/// <summary>
		/// 配置对象
		/// </summary>
		public Config Config { get; private set; }

		#endregion

		#region 事件


		/// <summary>
		/// 网络出现异常（如端口无法绑定等，此时无法发送文件，但是可以接收文件）
		/// </summary>
		public event EventHandler NetworkError;
		SendOrPostCallback scpcNetworkError;

		protected virtual void OnNetworkError(EventArgs e)
		{
			if (NetworkError == null) return;

			IpmEvents.OnTcpNetworkError(this);

			if (!IPMClient.NeedPostMessage)
			{
				NetworkError(this, e);
			}
			else
			{
				if (scpcNetworkError == null) scpcNetworkError = (s) => { NetworkError(this, s as EventArgs); };
				IPMClient.SendASynchronizeMessage(scpcNetworkError, e);
			}
		}

		/// <summary>
		/// 文件系统操作失败
		/// </summary>
		public event EventHandler<FileSystemOperationErrorEventArgs> FileSystemOperationError;
		SendOrPostCallback scpcFileSystemOperationError;

		protected virtual void OnFileSystemOperationError(FileSystemOperationErrorEventArgs e)
		{
			if (FileSystemOperationError == null) return;

			IpmEvents.OnTcpThreadFileSystemOperationError(_client, e);

			if (!IPMClient.NeedPostMessage)
			{
				FileSystemOperationError(this, e);
			}
			else
			{
				if (scpcFileSystemOperationError == null) scpcFileSystemOperationError = (s) => FileSystemOperationError(this, s as FileSystemOperationErrorEventArgs);
				IPMClient.SendASynchronizeMessage(scpcFileSystemOperationError, e);
			}
		}

		#region 发送文件事件



		#endregion


		#endregion

		#region 发送文件-监听线程

		/// <summary>
		/// 开始发送文件的监听线程
		/// </summary>
		void StartListener()
		{
			System.Threading.ThreadStart ts = ListenForSendRequest;
			new System.Threading.Thread(ts) { IsBackground = true }.Start();
		}

		/// <summary>
		/// 关闭监听线程
		/// </summary>
		void CloseListener()
		{
			listener.Stop();
			IsInitialized = false;
		}

		/// <summary>
		/// 监听函数
		/// </summary>
		void ListenForSendRequest()
		{
			listener.Start();
			while (IsInitialized)
			{
				try
				{
					TcpClient client = listener.AcceptTcpClient();
					//开始发送线程
					if (client != null)
					{
						System.Diagnostics.Debug.WriteLine("文件发送线程：已经接收到连接请求，远程IP：" + client.Client.RemoteEndPoint.ToString());

						new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(PerformFileSend)) { IsBackground = true }.Start(client);
					}
				}
				catch (Exception)
				{
					if (!IsInitialized) break;
				}
			}
		}

		#endregion
		#region 发送文件-发送线程

		/// <summary>
		/// 处理文件发送请求
		/// </summary>
		/// <param name="client"></param>
		void PerformFileSend(object p)
		{
			System.Diagnostics.Debug.WriteLine("文件发送线程 [0x" + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("X4") + "] 已经启动");

			using (TcpClient client = p as TcpClient)
			{
				byte[] buffer = new byte[400];	//接收或发送缓冲区
				int bytesReceived = ReceiveByBuffer(client, buffer, buffer.Length);	//第一步：接收文件传输命令

				if (bytesReceived < 1) return;	//没收到数据

				Message request = ParseSendCommand(buffer, 0, bytesReceived);	//试着解析命令
				System.Diagnostics.Debug.WriteLineIf(request == null, "未能解析收到的请求，退出发送线程");

				if (request == null) return;
				System.Diagnostics.Debug.WriteLine("已解析文件请求：" + request.NormalMsg.ToString());

				//非法请求
				//查找任务
				string[] taskInfo = request.NormalMsg.Split(':');
				ulong pid = 0ul;
				int tid = 0;

				FileTaskItem task = null;
				if (taskInfo.Length < 2 ||
					!ulong.TryParse(taskInfo[0], System.Globalization.NumberStyles.AllowHexSpecifier, null, out pid) ||
					!int.TryParse(taskInfo[1], System.Globalization.NumberStyles.AllowHexSpecifier, null, out tid) ||
					(task = TaskManager.QuerySendTask(pid, tid, (client.Client.RemoteEndPoint as IPEndPoint).Address)) == null
					)
					return;
				System.Diagnostics.Debug.WriteLine(string.Format("文件请求已经接纳，文件编号：0x{0:x8}，文件索引：0x{1:x4}", pid, tid));

				TaskManager.MarkSendTaskItemState(task, FileTaskItemState.Initializing);
				//准备发送
				if (task.IsFolder)
				{
					PerformSendDirectory(client, task.TaskInfo, task);
				}
				else
				{
					//文件支持有限的断点续传
					if (taskInfo.Length == 3)
					{
						ulong temp;
						if (ulong.TryParse(taskInfo[2], out temp))
						{
							task.CurrentFileTransfered = temp;
							task.FileTransferedAtPast = temp;
						}
						System.Diagnostics.Debug.WriteLineIf(task.CurrentFileTransfered > 0, string.Format("断点模式，从 0x{0} 处开始续传", task.CurrentFileTransfered));
					}

					PerformSendFile(client, task.TaskInfo, task);
				}
				client.Close();
			}
		}

		//发送文件夹
		void PerformSendDirectory(TcpClient client, FileTaskInfo task, FileTaskItem item)
		{
			NetworkStream stream = null;
			try
			{
				stream = client.GetStream();
			}
			catch (Exception)
			{
				TaskManager.MarkSendTaskItemState(item, FileTaskItemState.Failure);
				return;
			}

			string currentDirectory = item.FullPath;			//当前处理路径
			Stack<string> directoryStack = new Stack<string>();	//文件夹栈
			directoryStack.Push("");

			bool needUpdateTotal = item.TotalSize == 0;		//是否需要在发送的时候更新统计
			TaskManager.MarkSendTaskItemState(item, FileTaskItemState.Processing);
			item.StartTime = DateTime.Now;

			using (stream)
			{
				while (!string.IsNullOrEmpty(currentDirectory) || directoryStack.Count > 0)
				{
					if (!PerformSendDirectory_SendCreateDirCommand(currentDirectory, stream, task, item)) return;
					if (string.IsNullOrEmpty(currentDirectory))
					{
						if (directoryStack.Count > 0) currentDirectory = directoryStack.Pop();	//当前是空目录，则向上递归
						continue;
					}

					//扫描目录和文件
					string[] files = null, directories = null;
					try
					{
						files = System.IO.Directory.GetFiles(currentDirectory);
						directories = System.IO.Directory.GetDirectories(currentDirectory);

						item.FolderCount += directories.Length;
						item.FileCount += files.Length;
					}
					catch (Exception)
					{
						TaskManager.MarkSendTaskItemState(item, FileTaskItemState.Failure);
						OnFileSystemOperationError(new FileSystemOperationErrorEventArgs(FileSystemOperationType.QueryDirectory, item.FullPath, task.RemoteHost));
						return;
					}

					//优先发送文件
					foreach (var f in files)
					{
						if (!PerformSendDirectory_SendFilesInDirectory(f, stream, task, item, needUpdateTotal)) return;
					}

					//扫描子目录
					if (directories.Length > 0)
					{
						directoryStack.Push("");
						Array.ForEach(directories, s => { directoryStack.Push(s); });
						currentDirectory = directoryStack.Pop();	//取出一个文件夹来发送
					}
					else
					{
						//如果没有子目录，则置空当前目录，以命令流程向上返回
						currentDirectory = null;
					}
				}
				if (item.State != FileTaskItemState.Failure) TaskManager.MarkSendTaskItemState(item, FileTaskItemState.Finished);
			}
		}

		//发送文件夹中的文件
		bool PerformSendDirectory_SendFilesInDirectory(string path, NetworkStream stream, FileTaskInfo task, FileTaskItem item, bool updateTotal)
		{
			System.IO.FileInfo fileinfo = new System.IO.FileInfo(path);
			if (!fileinfo.Exists)
			{
				OnFileSystemOperationError(new FileSystemOperationErrorEventArgs(FileSystemOperationType.OpenFileToSend, path, task.RemoteHost));
			}

			if (updateTotal) item.TotalSize += (ulong)fileinfo.Length;
			item.CurrentName = fileinfo.Name;
			item.CurrentFileTransfered = 0;
			item.CurrentFileSize = (ulong)fileinfo.Length;

			string strCMD = string.Format(":{0}:{1:x}:{2:x}:", fileinfo.Name, fileinfo.Length, (int)Consts.Cmd_FileType_Option.Regular);

			byte[] bytes = null;
			if (task.RemoteHost.IsEnhancedContractEnabled) bytes = System.Text.Encoding.Unicode.GetBytes(strCMD);
			else bytes = System.Text.Encoding.Default.GetBytes(strCMD);

			try
			{
				stream.Write(System.Text.Encoding.Default.GetBytes((bytes.Length + 4).ToString("x4")), 0, 4);
				stream.Write(bytes, 0, bytes.Length);
			}
			catch (Exception)
			{
				TaskManager.MarkSendTaskItemState(item, FileTaskItemState.Failure);
				return false;
			}

			//写入文件数据
			FileStream reader = null;
			try
			{
				reader = fileinfo.OpenRead();
			}
			catch (Exception)
			{
				OnFileSystemOperationError(new FileSystemOperationErrorEventArgs(FileSystemOperationType.OpenFileToSend, path, task.RemoteHost));
				TaskManager.MarkSendTaskItemState(item, FileTaskItemState.Failure);
				return false;
			}

			using (reader)
			{
				byte[] buffer = new byte[SendBuffer];
				while (item.CurrentFileTransfered < (ulong)reader.Length)
				{
					int bytesRead = reader.Read(buffer, 0, buffer.Length);
					item.CurrentFileTransfered += (ulong)bytesRead;
					item.FinishedSize += (ulong)bytesRead;

					try
					{
						stream.Write(buffer, 0, bytesRead);
					}
					catch (Exception)
					{
						TaskManager.MarkSendTaskItemState(item, FileTaskItemState.Failure);
						return false;
					}
				}
				reader.Close();
			}
			item.FinishedFileCount++;

			return true;
		}

		//发送要求创建文件命令
		bool PerformSendDirectory_SendCreateDirCommand(string path, NetworkStream stream, FileTaskInfo task, FileTaskItem item)
		{
			string strCMD = string.Format(":{0}:{1}:{2:x}:", string.IsNullOrEmpty(path) ? "." : System.IO.Path.GetFileName(path), 0, (int)(string.IsNullOrEmpty(path) ? Consts.Cmd_FileType_Option.RetParent : Consts.Cmd_FileType_Option.Dir));

			byte[] bytes = null;
			if (task.RemoteHost.IsEnhancedContractEnabled) bytes = System.Text.Encoding.Unicode.GetBytes(strCMD);
			else bytes = System.Text.Encoding.Default.GetBytes(strCMD);

			if (!string.IsNullOrEmpty(path)) item.FinishedFolderCount++;

			try
			{
				stream.Write(System.Text.Encoding.Default.GetBytes((bytes.Length + 4).ToString("x4")), 0, 4);
				stream.Write(bytes, 0, bytes.Length);
				return true;
			}
			catch (Exception)
			{
				TaskManager.MarkSendTaskItemState(item, FileTaskItemState.Failure);
				return false;
			}
		}

		/// <summary>
		/// 发送单个文件
		/// </summary>
		/// <param name="client"></param>
		/// <param name="task"></param>
		/// <param name="item"></param>
		void PerformSendFile(TcpClient client, FileTaskInfo task, FileTaskItem item)
		{
			System.IO.FileStream fs = null;
			System.Net.Sockets.NetworkStream writer = null;
			try
			{
				writer = client.GetStream();
			}
			catch (Exception)
			{
				TaskManager.MarkSendTaskItemState(item, FileTaskItemState.Failure);
				return;
			}
			try
			{
				fs = new System.IO.FileStream(item.FullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
			}
			catch (Exception)
			{
				if (writer != null) writer.Close();
				TaskManager.MarkSendTaskItemState(item, FileTaskItemState.Failure);
				OnFileSystemOperationError(new FileSystemOperationErrorEventArgs(FileSystemOperationType.OpenFileToSend, item.FullPath, task.RemoteHost));

				return;
			}
			using (fs)
			{
				//检测断点数据是否正确
				if (item.CurrentFileTransfered < 0 || item.CurrentFileTransfered > (ulong)fs.Length) item.CurrentFileTransfered = 0;
				fs.Seek((long)item.CurrentFileTransfered, System.IO.SeekOrigin.Begin);

				//设置当前任务信息
				item.CurrentFileSize = (ulong)fs.Length;
				TaskManager.MarkSendTaskItemState(item, FileTaskItemState.Processing);
				item.StartTime = DateTime.Now;

				using (writer)
				{
					byte[] buffer = new byte[SendBuffer];	//缓冲区

					while (item.CurrentFileTransfered < item.CurrentFileSize)
					{

						int bytesRead = fs.Read(buffer, 0, buffer.Length);
						try
						{
							writer.Write(buffer, 0, bytesRead);
						}
						catch (Exception)
						{
							TaskManager.MarkSendTaskItemState(item, FileTaskItemState.Failure);
							break;
						}

						//更新进度
						item.CurrentFileTransfered += (ulong)bytesRead;
						item.FinishedSize += (ulong)bytesRead;
					}
					item.FinishedFileCount++;
					writer.Close();
				}
				fs.Close();
				//标记任务完成
				if (item.State != FileTaskItemState.Failure) TaskManager.MarkSendTaskItemState(item, FileTaskItemState.Finished);
			}
		}

		/// <summary>
		/// 解析请求的命令
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="startIndex"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		Message ParseSendCommand(byte[] buffer, int startIndex, int length)
		{
			byte[] cmdInfo = new byte[length];
			Array.Copy(buffer, 0, cmdInfo, 0, length);

			if (!MessagePacker.Test(cmdInfo)) return null;
			Message msg = MessagePacker.TryToTranslateMessage(cmdInfo, null);
			if (msg == null || (msg.Command != Consts.Commands.GetDirFiles && msg.Command != Consts.Commands.GetFileData)) return null;

			return msg;
		}

		/// <summary>
		/// 接收数据
		/// </summary>
		/// <param name="buffer">缓冲区</param>
		/// <returns></returns>
		int ReceiveByBuffer(TcpClient client, byte[] buffer)
		{
			try
			{
				return client.Client.Receive(buffer);
			}
			catch (Exception)
			{
				//网络异常
				return -1;
			}
		}

		/// <summary>
		/// 接收数据
		/// </summary>
		/// <param name="buffer">缓冲区</param>
		/// <returns></returns>
		int ReceiveByBuffer(TcpClient client, byte[] buffer, int length)
		{
			try
			{
				return client.Client.Receive(buffer, length, SocketFlags.None);
			}
			catch (Exception)
			{
				//网络异常
				return -1;
			}
		}

		/// <summary>
		/// 接收数据
		/// </summary>
		/// <param name="buffer">缓冲区</param>
		/// <returns></returns>
		int ReceiveByBuffer(NetworkStream stream, byte[] buffer)
		{
			try
			{
				return stream.Read(buffer, 0, buffer.Length);
			}
			catch (Exception)
			{
				//网络异常
				return -1;
			}
		}

		/// <summary>
		/// 接收数据
		/// </summary>
		/// <param name="buffer">缓冲区</param>
		/// <returns></returns>
		int ReceiveByBuffer(NetworkStream stream, byte[] buffer, int length)
		{
			try
			{
				return stream.Read(buffer, 0, length);
			}
			catch (Exception)
			{
				//网络异常
				return -1;
			}
		}

		#endregion

		#region 接收文件

		/// <summary>
		/// 请求接收文件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void _taskManager_FileReceiveRequired(object sender, FileReceiveRequiredEventArgs e)
		{
			if (!IsInitialized) return;

			new Thread(() => { PerformFileReceive(e.Task, e.Item); }) { IsBackground = true }.Start();
		}

		/// <summary>
		/// 开始接收文件任务
		/// </summary>
		/// <param name="task">任务信息</param>
		/// <param name="item">任务项目</param>
		internal void PerformFileReceive(FileTaskInfo task, FileTaskItem item)
		{
#if DEBUG
			System.Console.WriteLine("开始接受文件，尝试连接到 " + task.RemoteHost.HostSub.Ipv4Address.ToString());

#endif
			using (TcpClient client = new TcpClient())
			{
				client.ReceiveTimeout = this.Config.ConnectionTimeout;
				client.SendTimeout = this.Config.ConnectionTimeout;
				try
				{
					client.Connect(task.RemoteHost.HostSub.Ipv4Address);
				}
				catch (Exception)
				{
					this.TaskManager.MarkReceiveTaskItemState(item, FileTaskItemState.Failure);
					if (client.Connected) client.Close();
					return;
				}
				//检测是文件夹还是单个文件
				if (item.IsFolder)
				{
					PerformFileReceive_Folder(client, task, item);
				}
				else
				{
					PerformFileReceive_File(client, task, item);
				}
				client.Close();
			}
		}

		/// <summary>
		/// 接收文件夹
		/// </summary>
		/// <param name="client"></param>
		/// <param name="task"></param>
		/// <param name="item"></param>
		void PerformFileReceive_Folder(TcpClient client, FileTaskInfo task, FileTaskItem item)
		{
			TaskManager.MarkReceiveTaskItemState(item, FileTaskItemState.Initializing);
			//创建网络流
			NetworkStream netStream = null;
			try
			{
				netStream = client.GetStream();
			}
			catch (Exception)
			{
				OnNetworkError(new EventArgs());
				TaskManager.MarkReceiveTaskItemState(item, FileTaskItemState.Failure);
				return;
			}

			bool updateTotal = item.TotalSize == 0;	//是否在更新进度的时候需要更新总数

			using (netStream)
			{
				//发送文件请求
				PackedNetworkMessage pnm = MessagePacker.BuildNetworkMessage(Message.Create(task.RemoteHost, task.RemoteHost.HostSub.Ipv4Address, Config.GetRandomTick(), Config.HostName, Config.HostUserName, Consts.Commands.GetDirFiles, 0ul, string.Format("{0:x}:{1:x}:0:", task.PackageID, item.Index), ""));
				if (!SendDataInBuffer(netStream, pnm.Data, pnm.Data.Length)) return;	//发送请求

				TaskManager.MarkReceiveTaskItemState(item, FileTaskItemState.Processing);
				item.StartTime = DateTime.Now;

				string filename;
				ulong size;
				bool isFolder, isRet;
				Stack<string> pathStack = new Stack<string>();
				string currentPath = item.FullPath;
				while (client.Connected && item.State != FileTaskItemState.Canceling && ReadCmdHeader(netStream, task.RemoteHost, out filename, out size, out isFolder, out isRet))
				{
					if (isRet)
					{
						if (pathStack.Count > 0)
						{
							currentPath = pathStack.Pop();
							if (pathStack.Count == 0) break; //Fixed: 当当前队列不再有信息时，其实接收已经完成了。因为当前队列中最后一个肯定是文件夹自己。
						}
						else break;
					}
					else if (isFolder)
					{
						//建立文件夹
						if (!ProcessDirCmd(item, ref currentPath, pathStack, filename, updateTotal))
						{
							TaskManager.MarkReceiveTaskItemState(item, FileTaskItemState.Failure);
							return;
						}
					}
					else
					{
						//传文件
						if (!ProcessFileTransfer(client, netStream, task, item, currentPath, filename, size, updateTotal))
						{
							TaskManager.MarkReceiveTaskItemState(item, FileTaskItemState.Failure);
							return;
						}
					}
				}

				netStream.Close();
			}

			if (item.State == FileTaskItemState.Canceling) TaskManager.MarkReceiveTaskItemState(item, FileTaskItemState.Canceled);
			else if (item.State != FileTaskItemState.Failure) TaskManager.MarkReceiveTaskItemState(item, FileTaskItemState.Finished);
		}

		//传输文件
		bool ProcessFileTransfer(TcpClient client, NetworkStream netStream, FileTaskInfo task, FileTaskItem item, string currentDirectory, string fileName, ulong fileSize, bool updateTotal)
		{
			FileStream writer = null;
			string path = Path.Combine(currentDirectory, fileName);
			try
			{
				System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
				writer = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
			}
			catch (Exception)
			{
				OnFileSystemOperationError(new FileSystemOperationErrorEventArgs(FileSystemOperationType.CreateFile, path, task.RemoteHost));
				TaskManager.MarkReceiveTaskItemState(item, FileTaskItemState.Failure);
				return false;
			}
			//初始化传输参数
			item.CurrentFileSize = fileSize;
			item.CurrentFileTransfered = 0ul;
			item.CurrentName = fileName;
			if (updateTotal)
			{
				item.TotalSize += fileSize;
			}
			item.FileCount++;

			using (writer)
			{
				byte[] buffer = new byte[SendBuffer];
				int length = 0;

				int readlength = buffer.Length;
				if ((ulong)readlength > item.CurrentFileSize) readlength = (int)item.CurrentFileSize;
				while (item.State != FileTaskItemState.Canceling && client.Connected && item.CurrentFileTransfered < item.CurrentFileSize && (length = ReceiveByBuffer(netStream, buffer, readlength)) > 0)
				{
					if (!WriteBufferToFile(task.RemoteHost, writer, path, buffer, length)) break;
					item.CurrentFileTransfered += (ulong)length;
					item.FinishedSize += (ulong)length;

					readlength = buffer.Length;
					if ((ulong)readlength > item.CurrentFileSize - item.CurrentFileTransfered) readlength = (int)(item.CurrentFileSize - item.CurrentFileTransfered);
				}

				writer.Close();

				System.Diagnostics.Debug.Assert(item.CurrentFileTransfered <= item.CurrentFileSize, string.Format("传输文件异常。{0} 预计长度：{1} ，实际写入 {2}", item.CurrentName, item.CurrentFileSize, item.CurrentFileTransfered));

				if (item.CurrentFileTransfered < item.CurrentFileSize)
				{
					if (item.State != FileTaskItemState.Canceling)
					{
						TaskManager.MarkReceiveTaskItemState(item, FileTaskItemState.Failure);
					}
					return false;
				}
				else
				{
					item.FinishedFileCount++;
					return true;
				}
			}
		}

		//建立文件夹
		bool ProcessDirCmd(FileTaskItem taskitem, ref string currentDirectory, Stack<string> pathStack, string folderName, bool updateTotal)
		{
			string newPath = Path.Combine(currentDirectory, folderName);
			try
			{
				Directory.CreateDirectory(newPath);
			}
			catch (Exception)
			{
				OnFileSystemOperationError(new FileSystemOperationErrorEventArgs(FileSystemOperationType.CreateDirectory, newPath, taskitem.TaskInfo.RemoteHost));
				TaskManager.MarkReceiveTaskItemState(taskitem, FileTaskItemState.Failure);
				return false;
			}

			pathStack.Push(currentDirectory);
			currentDirectory = newPath;

			//更新进度
			taskitem.FinishedFolderCount++;
			taskitem.FolderCount++;


			return true;
		}

		internal static readonly Regex replaceReg = new Regex(@"(\x00|\?|\*|\\|\/|:|<|>|""|\|)", RegexOptions.IgnoreCase);

		//接收并返回文件夹命令
		bool ReadCmdHeader(NetworkStream stream, Host remoteHost, out string filename, out ulong size, out bool isFolder, out bool isRet)
		{
			filename = "";
			size = 0ul;
			isFolder = false;
			isRet = false;
			//命令长度
			byte[] buffer = new byte[4];
			if (ReceiveByBuffer(stream, buffer) != 4) return false;
			//读取头
			int headerLength = 0;
			if (!int.TryParse(System.Text.Encoding.ASCII.GetString(buffer), System.Globalization.NumberStyles.AllowHexSpecifier, null, out headerLength))
			{
				return false;
			}

			buffer = new byte[headerLength - 4];
			if (ReceiveByBuffer(stream, buffer) != buffer.Length)
			{
				return false;
			}

			//解析命令
			string cmd = remoteHost.IsEnhancedContractEnabled ? System.Text.Encoding.Unicode.GetString(buffer) : System.Text.Encoding.Default.GetString(buffer);
			string[] header = cmd.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
			ulong filesize, cmdNo;
			if (
				header.Length < 3
				||
				!ulong.TryParse(header[1], System.Globalization.NumberStyles.AllowHexSpecifier, null, out filesize)
				||
				!ulong.TryParse(header[2], System.Globalization.NumberStyles.AllowHexSpecifier, null, out cmdNo)
				) return false;

			filename = replaceReg.Replace(header[0], "_");
			size = filesize;
			isRet = ((ulong)Consts.Cmd_FileType_Option.RetParent == cmdNo);
			isFolder = ((ulong)Consts.Cmd_FileType_Option.Dir == cmdNo);

			return true;
		}

		/// <summary>
		/// 接收文件
		/// </summary>
		/// <param name="client"></param>
		/// <param name="task"></param>
		/// <param name="item"></param>
		void PerformFileReceive_File(TcpClient client, FileTaskInfo task, FileTaskItem item)
		{
			TaskManager.MarkReceiveTaskItemState(item, FileTaskItemState.Initializing);
			//创建文件
			System.IO.FileInfo fileinfo = new FileInfo(item.FullPath);
			if (fileinfo.Exists) item.CurrentFileTransfered = (ulong)fileinfo.Length;
			if (item.CurrentFileSize <= item.CurrentFileTransfered)
			{
				TaskManager.MarkReceiveTaskItemState(item, FileTaskItemState.Finished);
				return;
			}
			//创建文件
			System.IO.FileStream writer = null;
			try
			{
				System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(item.FullPath));
				writer = fileinfo.OpenWrite();
			}
			catch (Exception)
			{
				OnFileSystemOperationError(new FileSystemOperationErrorEventArgs(FileSystemOperationType.CreateFile, item.FullPath, task.RemoteHost));
				TaskManager.MarkReceiveTaskItemState(item, FileTaskItemState.Failure);
				return;
			}
			//创建网络流
			NetworkStream netStream = null;
			try
			{
				netStream = client.GetStream();
			}
			catch (Exception)
			{
				OnNetworkError(new EventArgs());
				TaskManager.MarkReceiveTaskItemState(item, FileTaskItemState.Failure);
				return;
			}
			using (writer)
			{
				using (netStream)
				{
					//发送文件请求
					PackedNetworkMessage pnm = MessagePacker.BuildNetworkMessage(Message.Create(task.RemoteHost, task.RemoteHost.HostSub.Ipv4Address, Config.GetRandomTick(), Config.HostName, Config.HostUserName, Consts.Commands.GetFileData, 0ul, string.Format("{0:x}:{1:x}:{2}", task.PackageID, item.Index, item.CurrentFileTransfered), ""));
					if (!SendDataInBuffer(netStream, pnm.Data, pnm.Data.Length)) return;	//发送请求

					TaskManager.MarkReceiveTaskItemState(item, FileTaskItemState.Processing);
					item.StartTime = DateTime.Now;

					byte[] buffer = new byte[SendBuffer];
					int length = buffer.Length;

					while (item.State != FileTaskItemState.Canceling && client.Connected && (length = ReceiveByBuffer(netStream, buffer, length)) > 0)
					{
						if (!WriteBufferToFile(task.RemoteHost, writer, item.FullPath, buffer, length)) return;
						item.CurrentFileTransfered += (ulong)length;
						item.FinishedSize += (ulong)length;

						//判断还有多少
						length = buffer.Length;
						ulong restLength = item.CurrentFileSize - item.CurrentFileTransfered;
						if (restLength < (ulong)length) length = (int)restLength;
					}
				}
			}

			//判断是否完成
			if (item.State == FileTaskItemState.Canceling) TaskManager.MarkReceiveTaskItemState(item, FileTaskItemState.Canceled);
			else if (item.CurrentFileTransfered == item.CurrentFileSize) TaskManager.MarkReceiveTaskItemState(item, FileTaskItemState.Finished);
			else TaskManager.MarkReceiveTaskItemState(item, FileTaskItemState.Failure);
		}

		//发送缓冲区中的数据
		bool SendDataInBuffer(NetworkStream stream, byte[] buffer, int length)
		{
			try
			{
				stream.Write(buffer, 0, length);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		//将缓冲区内容写入文件
		bool WriteBufferToFile(Host host, FileStream stream, string filepath, byte[] buffer, int length)
		{
			try
			{
				stream.Write(buffer, 0, length);
				return true;
			}
			catch (Exception)
			{
				OnFileSystemOperationError(new FileSystemOperationErrorEventArgs(FileSystemOperationType.WriteData, filepath, host));
				return false;
			}
		}

		#endregion

		#region IDisposable 成员

		bool isdisposed = false;

		public void Dispose()
		{
			if (!isdisposed)
			{
				isdisposed = true;
				CloseListener();
			}
		}

		#endregion
	}
}
