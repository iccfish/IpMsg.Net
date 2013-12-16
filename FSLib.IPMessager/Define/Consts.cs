using System.Diagnostics;
using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System;

namespace FSLib.IPMessager
{
	namespace Define
	{

		/// <summary>
		/// 一些常量的约定
		/// </summary>
		/// <remarks></remarks>
		public class Consts
		{

			/// <summary>
			/// 不允许创建对象
			/// </summary>
			/// <remarks></remarks>
			private Consts()
			{
				//默认创建类,不允许创建新对象
			}

			/// <summary>
			/// 获得数字代表的命令操作
			/// </summary>
			/// <param name="command"></param>
			/// <returns></returns>
			/// <remarks></remarks>
			public static ulong GetMsgCommandNum(ulong command)
			{
				return command & 0xFF;
			}

			/// <summary>
			/// 获得数字代表的选项
			/// </summary>
			/// <param name="command">命令编码</param>
			/// <returns></returns>
			/// <remarks></remarks>
			public static ulong GetMsgCommandOption(ulong command)
			{
				return command & 0xFFFFFF00;
			}


			//--------------------------------------------------------------
			/// <summary>
			/// 通信使用的默认端口
			/// </summary>
			/// <remarks></remarks>
			public static readonly int DefaultPort = 2425;
			/// <summary>
			/// 版本编号,此处固定为1,为了保证与飞鸽兼容。
			/// </summary>
			/// <remarks></remarks>
			public static readonly int VersionNumber = 49;

			/// <summary>
			/// 文件列表分割符
			/// </summary>
			/// <remarks></remarks>
			public static readonly char FileList_Separaror = '\n';

			/// <summary>
			/// 主机列表分隔符
			/// </summary>
			/// <remarks></remarks>
			public static readonly char HostList_Separator = '\x0A';

			/// <summary>
			/// HostList_Dummy
			/// </summary>
			public static readonly string HostList_Dummy = "\x11";


			/// <summary>
			/// UDP包最大的长度
			/// </summary>
			public static readonly int MAX_UDP_PACKAGE_LENGTH = 16384;



			//----------------------------------------------------------------
			/// <summary>
			/// IPMessager的命令,32位中的低八位
			/// </summary>
			/// <remarks></remarks>
			public enum Commands : ulong
			{
				//#define IPMSG_NOOPERATION		0x00000000UL
				/// <summary>
				/// 无操作
				/// </summary>
				/// <remarks></remarks>
				NoOperaiton = 0,

				//#define IPMSG_BR_ENTRY			0x00000001UL
				/// <summary>
				/// 进入服务,新用户上线
				/// </summary>
				/// <remarks></remarks>
				Br_Entry = 1,

				//#define IPMSG_BR_EXIT			0x00000002UL
				/// <summary>
				/// 离开服务,新用户离开服务
				/// </summary>
				/// <remarks></remarks>
				Br_Exit = 2,

				//#define IPMSG_ANSENTRY			0x00000003UL
				/// <summary>
				/// 已经接收新用户信息
				/// 这个提示信息将会在发送了IPMSG_BR_ENTRY后由收到信息的客户端返回
				/// </summary>
				/// <remarks></remarks>
				AnsEntry = 3,

				//#define IPMSG_BR_ABSENCE		0x00000004UL
				/// <summary>
				/// 改变离开状态
				/// </summary>
				/// <remarks></remarks>
				Br_Absence = 4,

				//#define IPMSG_BR_ISGETLIST		0x00000010UL
				IsGetList = 16,

				//#define IPMSG_OKGETLIST			0x00000011UL
				OkGetList = 17,

				//#define IPMSG_GETLIST			0x00000012UL
				GetList = 18,

				//#define IPMSG_ANSLIST			0x00000013UL
				AnsList = 19,

				//#define IPMSG_BR_ISGETLIST2		0x00000018UL
				IsGetList2 = 24,

				//#define IPMSG_SENDMSG			0x00000020UL
				/// <summary>
				/// 发送信息,指示发表的内容是发送信息
				/// </summary>
				/// <remarks></remarks>
				SendMsg = 32,

				//#define IPMSG_RECVMSG			0x00000021UL
				/// <summary>
				/// 信息确认
				/// 这个将会确认已经接收到新信息,如果没有在指定时间内接收到,则判断为信息发送失败.
				/// 返回时将会返回原始的数据包编号
				/// </summary>
				/// <remarks></remarks>
				RecvMsg = 33,

				//#define IPMSG_READMSG			0x00000030UL
				/// <summary>
				/// 信息是加密的(带有[Secret]标志位),则发送此命令要求阅读信息
				/// 返回原始包号码
				/// </summary>
				/// <remarks></remarks>
				ReadMsg = 48,

				//#define IPMSG_DELMSG			0x00000031UL
				/// <summary>
				/// 删除信息
				/// </summary>
				/// <remarks></remarks>
				DelMsg = 49,

				//#define IPMSG_ANSREADMSG		0x00000032UL
				/// <summary>
				/// 信息确认(封装,打开提醒)
				/// </summary>
				/// <remarks></remarks>
				AnsReadMsg = 50,

				//#define IPMSG_GETINFO			0x00000040UL
				/// <summary>
				/// 获得客户端使用的版本编号
				/// </summary>
				/// <remarks></remarks>
				GetInfo = 64,

				//#define IPMSG_SENDINFO			0x00000041UL
				/// <summary>
				/// 正在发送的是客户端的版本编号
				/// </summary>
				/// <remarks></remarks>
				SendInfo = 65,

				//#define IPMSG_GETABSENCEINFO	0x00000050UL
				/// <summary>
				/// 获得离开状态
				/// </summary>
				/// <remarks></remarks>
				GetAbsenceInfo = 80,

				//#define IPMSG_SENDABSENCEINFO	0x00000051UL
				/// <summary>
				/// 正在发送的是当前用户的离开状态
				/// </summary>
				/// <remarks></remarks>
				SendAbsenceInfo = 81,

				//#define IPMSG_GETFILEDATA		0x00000060UL
				GetFileData = 96,

				//#define IPMSG_RELEASEFILES		0x00000061UL
				ReleaseFiles = 97,

				//#define IPMSG_GETDIRFILES		0x00000062UL
				GetDirFiles = 98,

				//#define IPMSG_GETPUBKEY			0x00000072UL
				/// <summary>
				/// 获得RSA公钥
				/// </summary>
				/// <remarks></remarks>
				GetPubKey = 114,

				//#define IPMSG_ANSPUBKEY			0x00000073UL
				/// <summary>
				/// 发送RSA公钥
				/// </summary>
				/// <remarks></remarks>
				AnsPubKey = 115,
				/// <summary>
				/// 转发的进入主机消息
				/// </summary>
				Br_Entry_Forward = 116,
				/// <summary>
				/// 转发的主机退出信息
				/// </summary>
				Br_Exit_Forward = 117,
				/// <summary>
				/// 扩展-消息包已经收到
				/// </summary>
				Ex_PackageRecevied = 118
			}

			/// <summary>
			/// 供所有命令用的选项
			/// </summary>
			/// <remarks></remarks>
			[Flags]
			public enum Cmd_All_Option : ulong
			{
				///*  option for all command  */
				//#define IPMSG_ABSENCEOPT		0x00000100UL
				Absence = 256,
				//#define IPMSG_SERVEROPT			0x00000200UL
				ServerPort = 512,
				//#define IPMSG_DIALUPOPT			0x00010000UL
				DialUp = 6553,
				//#define IPMSG_FILEATTACHOPT		0x00200000UL
				FileAttach = 2097152,
				//#define IPMSG_ENCRYPTOPT		0x00400000UL
				Encrypt = 4194304,
				/// <summary>
				/// 新版本的支持断点续传的文件传输模式 【0x20000000UL】
				/// </summary>
				/// <remarks></remarks>
				NewFileAttach = 536870912,
				/// <summary>
				/// 要求收到的时候给与回应
				/// </summary>
				RequireReceiveCheck = 0x40000000ul,
				/// <summary>
				/// 启用新版本协议
				/// </summary>
				EnableNewDataContract = 0x80000000ul,
				/// <summary>
				/// 是二进制消息，信息包含此消息位时，不会解析为字符串
				/// </summary>
				BinaryMessage = 0x01000000ul
			}

			/// <summary>
			/// 供信息命令用的选项
			/// </summary>
			/// <remarks></remarks>
			[Flags]
			public enum Cmd_Send_Option : ulong
			{
				///*  option for send command  */
				//#define IPMSG_SENDCHECKOPT		0x00000100UL
				SendCheck = 256,

				//#define IPMSG_SECRETOPT		0x00000200UL
				Secret = 512,

				//#define IPMSG_BROADCASTOPT		0x00000400UL
				BroadCast = 1024,

				//#define IPMSG_MULTICASTOPT		0x00000800UL
				MultiCast = 2048,

				//#define IPMSG_NOPOPUPOPT		0x00001000UL
				NoPopup = 4096,

				//#define IPMSG_AUTORETOPT		0x00002000UL
				AutoRet = 8192,

				//#define IPMSG_RETRYOPT			0x00004000UL
				Retry = 16384,

				//#define IPMSG_PASSWORDOPT		0x00008000UL
				Password = 32768,

				//#define IPMSG_NOLOGOPT			0x00020000UL
				NoLog = 131072,

				//#define IPMSG_NEWMUTIOPT		0x00040000UL
				NewMuti = 262144,

				//#define IPMSG_NOADDLISTOPT		0x00080000UL
				NoAddList = 524288,

				//#define IPMSG_READCHECKOPT		0x00100000UL
				ReadCheck = 1048576,

				//#define IPMSG_SECRETEXOPT		(IPMSG_READCHECKOPT|IPMSG_SECRETOPT)
				SecretEx = 1049088,
				/// <summary>
				/// HTML格式信息
				/// </summary>
				Content_Html = 0x02000000ul,
				/// <summary>
				/// RTF格式信息
				/// </summary>
				Content_RTF = 0x04000000ul,
				/// <summary>
				/// Unicode格式文本
				/// </summary>
				Content_Unicode = 0x08000000ul
			}

			/// <summary>
			/// 供加密命令用的选项
			/// </summary>
			/// <remarks></remarks>
			[Flags]
			public enum Cmd_Encrpy_Option : ulong
			{
				///* encryption flags for encrypt command */
				//#define IPMSG_RSA_512			0x00000001UL
				RSA_512 = 1,

				//#define IPMSG_RSA_1024			0x00000002UL
				RSA_1024 = 2,

				//#define IPMSG_RSA_2048			0x00000004UL
				RSA_2048 = 4,

				//#define IPMSG_RC2_40			0x00001000UL
				RC2_40 = 4096,

				//#define IPMSG_RC2_128			0x00004000UL
				RC2_128 = 16384,

				//#define IPMSG_RC2_256			0x00008000UL
				RC2_256 = 32768,

				//#define IPMSG_BLOWFISH_128		0x00020000UL
				BlowFish_128 = 131072,

				//#define IPMSG_BLOWFISH_256		0x00040000UL
				BlowFish_256 = 262144,

				//#define IPMSG_SIGN_MD5			0x10000000UL
				Sign_MD5 = 268435456
			}

			/// <summary>
			/// compatibilty for Win beta version
			/// </summary>
			/// <remarks></remarks>
			[Flags]
			public enum Cmd_Compat_Option : ulong
			{
				//#define IPMSG_RC2_40OLD			0x00000010UL	// for beta1-4 only
				Rc2_40Old = 16,

				//#define IPMSG_RC2_128OLD		0x00000040UL	// for beta1-4 only
				Rc2_128Old = 64,

				//#define IPMSG_BLOWFISH_128OLD	0x00000400UL	// for beta1-4 only
				BlowFish_128Old = 1024,

				//#define IPMSG_RC2_40ALL			(IPMSG_RC2_40|IPMSG_RC2_40OLD)
				Rc2_40ALL = 4112,

				//#define IPMSG_RC2_128ALL		(IPMSG_RC2_128|IPMSG_RC2_128OLD)
				RC2_128All = 16448,

				//#define IPMSG_BLOWFISH_128ALL	(IPMSG_BLOWFISH_128|IPMSG_BLOWFISH_128OLD)
				BlowFish_128All = 132096
			}

			/// <summary>
			/// file types for fileattach command
			/// 文件类型标志
			/// </summary>
			/// <remarks></remarks>
			[Flags]
			public enum Cmd_FileType_Option : ulong
			{
				//#define IPMSG_FILE_REGULAR		0x00000001UL
				Regular = 1,

				//#define IPMSG_FILE_DIR			0x00000002UL
				Dir = 2,

				//#define IPMSG_FILE_RETPARENT	0x00000003UL	// return parent directory
				RetParent = 3,

				//#define IPMSG_FILE_SYMLINK		0x00000004UL
				SymLink = 4,

				//#define IPMSG_FILE_CDEV			0x00000005UL	// for UNIX
				Cdev = 5,

				//#define IPMSG_FILE_BDEV			0x00000006UL	// for UNIX
				Bdev = 6,

				//#define IPMSG_FILE_FIFO			0x00000007UL	// for UNIX
				Fifo = 7,

				//#define IPMSG_FILE_RESFORK		0x00000010UL	// for Mac
				Resfork = 16
			}

			/// <summary>
			/// file attribute options for fileattach command
			/// 文件属性
			/// </summary>
			/// <remarks></remarks>
			[Flags]
			public enum Cmd_FileAttr_Option : ulong
			{
				///* file attribute options for fileattach command */
				//#define IPMSG_FILE_RONLYOPT		0x00000100UL
				Read_Only = 256,

				//#define IPMSG_FILE_HIDDENOPT	0x00001000UL
				Hidden = 4096,

				//#define IPMSG_FILE_EXHIDDENOPT	0x00002000UL	// for MacOS X
				ExHidden = 8192,

				//#define IPMSG_FILE_ARCHIVEOPT	0x00004000UL
				Archive = 16384,

				//#define IPMSG_FILE_SYSTEMOPT	0x00008000UL
				System = 32768
			}

			/// <summary>
			/// extend attribute types for fileattach command
			/// </summary>
			/// <remarks></remarks>
			[Flags]
			public enum Cmd_ExtFileAttr_Option : ulong
			{
				//#define IPMSG_FILE_UID			0x00000001UL
				Uid = 1,

				//#define IPMSG_FILE_USERNAME		0x00000002UL	// uid by string
				UserName = 2,

				//#define IPMSG_FILE_GID			0x00000003UL
				Gid = 3,

				//#define IPMSG_FILE_GROUPNAME	0x00000004UL	// gid by string
				GroupName = 4,

				//#define IPMSG_FILE_PERM			0x00000010UL	// for UNIX
				Perm = 16,

				//#define IPMSG_FILE_MAJORNO		0x00000011UL	// for UNIX devfile
				MajorNo = 17,

				//#define IPMSG_FILE_MINORNO		0x00000012UL	// for UNIX devfile
				MinorNo = 18,

				//#define IPMSG_FILE_CTIME		0x00000013UL	// for UNIX
				CTime = 19,

				//#define IPMSG_FILE_MTIME		0x00000014UL
				MTime = 20,

				//#define IPMSG_FILE_ATIME		0x00000015UL
				ATime = 21,

				//#define IPMSG_FILE_CREATETIME	0x00000016UL
				CreateTime = 22,

				//#define IPMSG_FILE_CREATOR		0x00000020UL	// for Mac
				Creator = 32,

				//#define IPMSG_FILE_FILETYPE		0x00000021UL	// for Mac
				FileType = 33,

				//#define IPMSG_FILE_FINDERINFO	0x00000022UL	// for Mac
				FinderInfo = 34,

				//#define IPMSG_FILE_ACL			0x00000030UL
				Acl = 48,

				//#define IPMSG_FILE_ALIASFNAME	0x00000040UL	// alias fname
				AliasFName = 64,

				//#define IPMSG_FILE_UNICODEFNAME	0x00000041UL	// UNICODE fname
				UnicodeName = 65
			}


			///// <summary>
			///// IP Messenger for Windows  internal define
			///// </summary>
			///// <remarks></remarks>
			//[Flags]
			//public enum WindowsInteralDefine : ulong
			//{
			//	///*  IP Messenger for Windows  internal define  */
			//	//#define IPMSG_REVERSEICON			0x0100
			//	ReverseIcon = 256,

			//	//#define IPMSG_TIMERINTERVAL			500
			//	TimerInterval = 500,

			//	//#define IPMSG_ENTRYMINSEC			5
			//	EntryMinSec = 5,

			//	//#define IPMSG_GETLIST_FINISH		0
			//	GetList_Finish = 0,

			//	//#define IPMSG_BROADCAST_TIMER		0x0101
			//	BroadCast_Timer = 257,

			//	//#define IPMSG_SEND_TIMER			0x0102
			//	Send_Timer = 258,

			//	//#define IPMSG_LISTGET_TIMER			0x0104
			//	ListGet_Timer = 260,

			//	//#define IPMSG_LISTGETRETRY_TIMER	0x0105
			//	ListGetRetry = 261,

			//	//#define IPMSG_ENTRY_TIMER			0x0106
			//	Entry_Timer = 262,

			//	//#define IPMSG_DUMMY_TIMER			0x0107
			//	Dummy_Timer = 263,

			//	//#define IPMSG_RECV_TIMER			0x0108
			//	Recv_Timer = 264,

			//	//#define IPMSG_ANS_TIMER				0x0109
			//	Ans_Timer = 265,

			//	//#define IPMSG_NICKNAME			1
			//	NickName = 1,

			//	//#define IPMSG_FULLNAME			2
			//	FullName = 2,

			//	//#define IPMSG_NAMESORT			0x00000000
			//	NameSort = 0,

			//	//#define IPMSG_IPADDRSORT		0x00000001
			//	IpAddrSort = 1,

			//	//#define IPMSG_HOSTSORT			0x00000002
			//	HostSort = 2,

			//	//#define IPMSG_NOGROUPSORTOPT	0x00000100
			//	NoGroupSort = 256,

			//	//#define IPMSG_ICMPSORTOPT		0x00000200
			//	IcmpSort = 512,

			//	//#define IPMSG_NOKANJISORTOPT	0x00000400
			//	NoKanJiSort = 1024,

			//	//#define IPMSG_ALLREVSORTOPT		0x00000800
			//	AllRevsort = 2048,

			//	//#define IPMSG_GROUPREVSORTOPT	0x00001000
			//	GroupRevSort = 4096,

			//	//#define IPMSG_SUBREVSORTOPT		0x00002000
			//	SubRevSort = 8192
			//}

			/// <summary>
			/// 检查选项是否存在
			/// </summary>
			/// <param name="orgData">组合值</param>
			/// <param name="toCheck">选项代码</param>
			/// <returns></returns>
			/// <remarks>组合值是用 选项代码 用 OR 的方式组合出来的,这个函数用于检测组合值是否包含了选项代码</remarks>
			public static bool Check(ulong orgData, ulong toCheck)
			{
				return (orgData & toCheck) == toCheck;
			}

			/// <summary>
			/// 检查选项是否存在
			/// </summary>
			/// <param name="orgData">组合值</param>
			/// <param name="toCheck">选项代码</param>
			/// <returns></returns>
			/// <remarks>组合值是用 选项代码 用 OR 的方式组合出来的,这个函数用于检测组合值是否包含了选项代码</remarks>
			public static bool Check(ulong orgData, Cmd_All_Option toCheck)
			{
				return (orgData & (ulong)toCheck) == (ulong)toCheck;
			}

			/// <summary>
			/// 检查选项是否存在
			/// </summary>
			/// <param name="orgData">组合值</param>
			/// <param name="toCheck">选项代码</param>
			/// <returns></returns>
			/// <remarks>组合值是用 选项代码 用 OR 的方式组合出来的,这个函数用于检测组合值是否包含了选项代码</remarks>
			public static bool Check(ulong orgData, Cmd_Send_Option toCheck)
			{
				return (orgData & (ulong)toCheck) == (ulong)toCheck;
			}
		}
	}
}
