using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using FSLib.IPMessager.Define;
using FSLib.IPMessager.Core;
using FSLib.IPMessager.Entity;

namespace FSLib.IPMessager.Services
{
	/// <summary>
	/// 通信加密组件
	/// </summary>
	[FSLib.IPMessager.Services.Service("木鱼", "E-mail:fishcn@foxmail.com", "RSA加密插件", "©Copyright 2006-2009,木鱼", "提供加密功能。此插件启用后，将会对支持RSA 1024加密的主机间使用RSA加密会话，防止内容被嗅探或他人窃取。不支持加密的主机不会受到影响。")]
	public class MessageEncryptProvider : ProviderBase, IServiceProvider
	{
		/// <summary>
		/// 构造新的对象实例
		/// </summary>
		public MessageEncryptProvider()
		{
			InitializeEncryptor();
		}


		#region 加密&解密

		/// <summary>
		/// 加密用的加密器(512位)
		/// </summary>
		RSACryptoServiceProvider RSA_512_Encryptor;

		/// <summary>
		/// 加密用的加密器(512位)
		/// </summary>
		RSACryptoServiceProvider RSA_512_Decryptor;

		/// <summary>
		/// 加密用的加密器(1024位)
		/// </summary>
		RSACryptoServiceProvider RSA_1024_Encryptor;

		/// <summary>
		/// 加密用的加密器(1024位)
		/// </summary>
		RSACryptoServiceProvider RSA_1024_Decryptor;

		/// <summary>
		/// 正文消息加密
		/// </summary>
		//RC2CryptoServiceProvider RC2;

		/// <summary>
		/// BlogFish Encryptor
		/// </summary>
		BlowfishNET.BlowfishCBC blowFish;

		/// <summary>
		/// 公钥(1024)
		/// </summary>
		public byte[] PublicKey_1024 { get; set; }

		/// <summary>
		/// 公钥(512)
		/// </summary>
		public byte[] PublicKey_512 { get; set; }

		/// <summary>
		/// Exponent(1024)
		/// </summary>
		public byte[] Exponent_1024 { get; set; }

		/// <summary>
		/// Exponent(512)
		/// </summary>
		public byte[] Exponent_512 { get; set; }

		/// <summary>
		/// 加密能力
		/// </summary>
		public ulong EncryptCapa { get; private set; }

		/// <summary>
		/// 低强度加密
		/// </summary>
		public ulong EncryptSmallCapa { get; private set; }

		/// <summary>
		/// 高强度加密
		/// </summary>
		public ulong EncryptNormalCapa { get; private set; }

		/// <summary>
		/// 初始化加密类
		/// </summary>
		void InitializeEncryptor()
		{
			RSA_1024_Encryptor = new RSACryptoServiceProvider();
			RSA_512_Encryptor = new RSACryptoServiceProvider();
			RSA_512_Encryptor.KeySize = 512;

			RSA_1024_Decryptor = new RSACryptoServiceProvider();
			RSA_512_Decryptor = new RSACryptoServiceProvider();
			RSA_512_Decryptor.KeySize = 512;

			RSAParameters p = RSA_1024_Decryptor.ExportParameters(false);
			PublicKey_1024 = p.Modulus;
			Exponent_1024 = p.Exponent;

			//RC2 = new RC2CryptoServiceProvider();
			//RC2.KeySize = 40;
			//RC2.Mode = CipherMode.CBC;

			p = RSA_512_Decryptor.ExportParameters(false);
			PublicKey_512 = p.Modulus;
			Exponent_512 = p.Exponent;

			//EncryptSmallCapa = (ulong)(Define.Consts.Cmd_Encrpy_Option.RC2_40 | Define.Consts.Cmd_Encrpy_Option.RSA_512);
			EncryptSmallCapa = (ulong)(Define.Consts.Cmd_Encrpy_Option.RSA_512);	//没有找到解决方案以前，暂时标记为不支持
			EncryptNormalCapa = (ulong)(Define.Consts.Cmd_Encrpy_Option.BlowFish_128 | Define.Consts.Cmd_Encrpy_Option.RSA_1024);
			EncryptCapa = EncryptSmallCapa | EncryptNormalCapa;

			blowFish = new BlowfishNET.BlowfishCBC();
			rcsp = new RNGCryptoServiceProvider();
		}

		#endregion

		#region 公共方法

		/// <summary>
		/// 将信息解密函数挂钩
		/// </summary>
		/// <param name="cmd"></param>
		public void HandleEncryptedMessage(CommandExecutor cmd)
		{
			//加密信息预先解密
			cmd.TextMessageReceiving += (s, e) =>
			{
				if (!e.IsHandled && e.Message.IsEncrypt)
				{
					DecryptMessage(cmd, e);
				}
			};
			cmd.MessageProxy.MessageSending += (s, e) =>
			{
				if (!e.IsHandled && e.Message.Command == Consts.Commands.SendMsg && e.Message.IsEncrypt)
				{
					EncryptMessage(cmd, e);
				}
			};
			cmd.MessageProcessing += (s, e) =>
			{
				if (e.IsHandled || e.Host == null) return;

				if (e.Message.Command == Consts.Commands.AnsPubKey)
				{
					//发送确认消息
					MessageProxy.SendWithNoCheck(e.Host, Consts.Commands.RecvMsg, 0ul, e.Message.PackageNo.ToString(), "");

					//分析
					int index = e.Message.NormalMsg.IndexOf(":");
					if (index == -1) return;

					ulong capa;
					if (!ulong.TryParse(e.Message.NormalMsg.Substring(0, index++), System.Globalization.NumberStyles.AllowHexSpecifier, null, out capa)) return;
					if ((capa & EncryptCapa) == 0) return;

					e.Host.SupportEncrypt = true;
					e.Host.IsEncryptUseSmallKey = (capa & EncryptNormalCapa) == 0;

					int nextMatch;
					if ((nextMatch = e.Message.NormalMsg.IndexOf('-', index)) == -1) return;
					byte[] exp = e.Message.NormalMsg.Substring(index, nextMatch - index).ConvertToBytes();
					if (exp == null) return;

					byte[] pubKey = e.Message.NormalMsg.Substring(nextMatch + 1).ConvertToBytes();
					if (pubKey == null) return;

					e.Host.SetEncryptInfo(pubKey, exp);

					//查看有没有队列消息，有就发出去
					if (e.Host.QueuedMessage != null && e.Host.QueuedMessage.Count > 0)
					{
						Message m = null;
						while (e.Host.QueuedMessage.Count > 0)
						{
							m = e.Host.QueuedMessage.Dequeue();
							MessageProxy.Send(m);
						}
					}
				}
				else if (e.Message.Command == Consts.Commands.GetPubKey)
				{
					ulong capa;
					if (!ulong.TryParse(e.Message.NormalMsg, System.Globalization.NumberStyles.AllowHexSpecifier, null, out capa)) return;

					if ((capa & EncryptCapa) == 0) return;

					//返回响应
					e.Host.SupportEncrypt = true;
					e.Host.IsEncryptUseSmallKey = (capa & EncryptNormalCapa) == 0;

					byte[] key = e.Host.IsEncryptUseSmallKey ? PublicKey_512 : PublicKey_1024;
					byte[] exp = e.Host.IsEncryptUseSmallKey ? Exponent_512 : Exponent_1024;

					string content = string.Format("{0}:{1}-{2}", EncryptCapa.ToString("x"), BitConverter.ToString(exp).Replace("-", ""), BitConverter.ToString(key).Replace("-", "")).ToLower();
					MessageProxy.SendWithNoCheck(e.Host, Consts.Commands.AnsPubKey, 0ul, content, "");
				}
			};

		}

		#endregion


		#region 私有函数

		/// <summary>
		/// 随机数
		/// </summary>
		RNGCryptoServiceProvider rcsp;

		/// <summary>
		/// 加密信息
		/// </summary>
		/// <param name="cmd">Commander</param>
		/// <param name="e">事件参数</param>
		void EncryptMessage(CommandExecutor cmd, Entity.MessageEventArgs e)
		{
			Entity.Host host = null;
			if (cmd.LivedHost == null || (host = cmd.LivedHost.GetHost(e.Message.HostAddr.Address.ToString())) == null || !host.SupportEncrypt) return;

			if (host.PubKey == null)
			{
				//压入队列
				if (host.QueuedMessage == null) host.QueuedMessage = new Queue<FSLib.IPMessager.Entity.Message>();
				host.QueuedMessage.Enqueue(e.Message);
				e.IsHandled = true;

				//请求公钥
				GetPubKey(host);
			}
			else
			{
				if (e.Message.NormalMsgBytes == null)
				{
					System.Text.Encoding enc = e.Message.IsEncodingUnicode ? System.Text.Encoding.Unicode : System.Text.Encoding.Default;
					if (!string.IsNullOrEmpty(e.Message.NormalMsg)) e.Message.NormalMsgBytes = enc.GetBytes(e.Message.NormalMsg);
					else e.Message.NormalMsgBytes = new byte[] { 0 };

					if (!string.IsNullOrEmpty(e.Message.ExtendMessage)) e.Message.ExtendMessageBytes = enc.GetBytes(e.Message.ExtendMessage);
					else e.Message.ExtendMessageBytes = new byte[] { 0 };
				}
				if (e.Message.NormalMsgBytes == null) return;

				//加密
				if (host.IsEncryptUseSmallKey)
				{
					//RSA512&&RC2
					//暂时不支持。。。
					//TODO:加密支持RC2
					e.Message.IsEncrypt = false;
				}
				else
				{
					//RSA1024&BlowFish
					byte[] key = new byte[8];
					rcsp.GetBytes(key);

					blowFish.Initialize(key, 0, key.Length);
					byte[] content = new byte[(int)Math.Ceiling(e.Message.NormalMsgBytes.Length / 16.0) * 16];	//BlowFish加密必须是16字节的整数倍
					e.Message.NormalMsgBytes.CopyTo(content, 0);

					blowFish.Encrypt(content, 0, content, 0, content.Length);
					blowFish.Invalidate();

					//加密Key
					RSAParameters p = new RSAParameters()
					{
						Modulus = host.PubKey,
						Exponent = host.Exponent
					};
					RSA_1024_Encryptor.ImportParameters(p);
					key = RSA_1024_Encryptor.Encrypt(key, false);
					//Array.Reverse(key);

					//组装消息
					System.Text.StringBuilder sb = new StringBuilder();
					sb.Append(EncryptNormalCapa.ToString("x"));
					sb.Append(":");
					sb.Append(key.ConvertToString());
					sb.Append(":");
					sb.Append(content.ConvertToString());

					e.Message.NormalMsg = sb.ToString();
					e.Message.NormalMsgBytes = null;
				}
			}
		}

		/// <summary>
		/// 解析加密信息
		/// </summary>
		/// <param name="cmd">Commander</param>
		/// <param name="e">事件参数</param>
		void DecryptMessage(CommandExecutor cmd, Entity.MessageEventArgs e)
		{
			int index = 0;
			int nextMatch = 0;

			if ((nextMatch = e.Message.NormalMsg.IndexOf(':', index)) == -1) return;
			ulong capa;
			if (!ulong.TryParse(e.Message.NormalMsg.Substring(index, nextMatch - index), System.Globalization.NumberStyles.AllowHexSpecifier, null, out capa)) return;
			//查找密钥
			index = nextMatch + 1;
			if ((nextMatch = e.Message.NormalMsg.IndexOf(':', index)) == -1) return;
			byte[] buf = e.Message.NormalMsg.ConvertToBytes(index, nextMatch);
			//消息加密内容
			byte[] content = e.Message.NormalMsg.ConvertToBytes(nextMatch + 1, e.Message.NormalMsg.Length);

			//解密密钥
			bool isSuccess = false;
			Define.Consts.Cmd_Encrpy_Option encOpt = (Define.Consts.Cmd_Encrpy_Option)capa;
			if ((encOpt & Consts.Cmd_Encrpy_Option.RSA_1024) == Consts.Cmd_Encrpy_Option.RSA_1024)
			{
				//RSA1024
				try
				{
					buf = this.RSA_1024_Decryptor.Decrypt(buf, false);
					isSuccess = true;
				}
				catch (Exception) { }
			}
			else
			{
				try
				{
					buf = this.RSA_512_Decryptor.Decrypt(buf, false);
					isSuccess = true;
				}
				catch (Exception) { }
			}

			if (!isSuccess)
			{
				if ((encOpt & Consts.Cmd_Encrpy_Option.RSA_1024) == Consts.Cmd_Encrpy_Option.RSA_1024)
				{
					cmd.SendCommand(e.Message.HostAddr, Consts.Commands.SendMsg, 0ul, "[AUTO=PUBKEY_EXP]\n\n公钥已经过期，无法解密信息，请刷新主机列表。", "", false, false, true, true, true, false, false, false);
					e.IsHandled = true;	//过滤掉消息
					return;
				}
				else
				{
					cmd.SendCommand(e.Message.HostAddr, Consts.Commands.SendMsg, 0ul, "[AUTO=KEY_NOT_SUPPORT]\n\n您的客户端所使用的加密等级过低，无法支持。", "", false, false, true, true, true, false, false, false);
					e.IsHandled = true;	//过滤掉消息
					return;
				}
			}
			else
			{
				//解密文本
				if ((encOpt & Consts.Cmd_Encrpy_Option.RC2_40) == Consts.Cmd_Encrpy_Option.RC2_40)
				{

					//TODO:RC2加密解决

				}
				else
				{
					//BlowFish
					blowFish.Initialize(buf, 0, buf.Length);
					blowFish.Decrypt(content, 0, content, 0, content.Length);
					blowFish.Invalidate();
				}

				int endIndex = Array.FindIndex(content, (s) => s == 0);
				if (endIndex == -1) endIndex = content.Length;

				if (Define.Consts.Check(e.Message.Options, Define.Consts.Cmd_Send_Option.Content_Unicode))
				{
					e.Message.NormalMsg = System.Text.Encoding.Unicode.GetString(content, 0, endIndex);
				}
				else
				{
					e.Message.NormalMsg = System.Text.Encoding.Default.GetString(content, 0, endIndex);
				}

			}
		}


		/// <summary>
		/// 获得指定主机的公钥
		/// </summary>
		/// <param name="host">主机</param>
		void GetPubKey(Host host)
		{
			if (!host.SupportEncrypt) return;

			Commander.SendCommand(host, Consts.Commands.GetPubKey, 0ul, EncryptCapa.ToString("x"), "", false, false, false, false, false, false, false, false);
		}


		#endregion

		#region IServiceProvider 成员

		/// <summary>
		/// 插件启动(已重载)
		/// </summary>
		public void Startup()
		{
			HandleEncryptedMessage(this.Commander);
		}

		/// <summary>
		/// 获得主机功能标识(已重载)
		/// </summary>
		/// <returns>新标识</returns>
		public new ulong GenerateClientFeatures()
		{
			return (ulong)Define.Consts.Cmd_All_Option.Encrypt;
		}

		/// <summary>
		/// 插件图标(已重载)
		/// </summary>
		public override System.Drawing.Image PluginIcon
		{
			get
			{
				return Properties.Resources.security_plugins;
			}
		}

		#endregion
	}
}
