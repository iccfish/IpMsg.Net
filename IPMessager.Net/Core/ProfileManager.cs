using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IPMessagerNet._Embed;

namespace IPMessagerNet.Core
{
	static class ProfileManager
	{
		//配置文件存储路径
		static string ProfilePath;
		static string BinPath;

		static ProfileManager()
		{
			ProfilePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "飞鸽传书.Net");
			BinPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
		}

		#region 存取配置文件

		/// <summary>
		/// 加载配置
		/// </summary>
		/// <typeparam name="T">配置类型</typeparam>
		/// <returns></returns>
		public static T LoadConfig<T>() where T : class, new()
		{
			Type t = typeof(T);
			string path = System.IO.Path.Combine(ProfilePath, t.Name + ".xml");
			if (!System.IO.File.Exists(path)) return null;
			else
			{
				T obj = null;

				using (System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.Unicode))
				{
					System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(t);
					obj = xml.Deserialize(sr) as T;
					sr.Close();
				}
				return obj;
			}
		}

		/// <summary>
		/// 加载配置
		/// </summary>
		/// <typeparam name="T">配置类型</typeparam>
		/// <returns></returns>
		public static object LoadConfig(Type t, string configName)
		{
			string path = System.IO.Path.Combine(ProfilePath, configName + ".xml");
			if (!System.IO.File.Exists(path)) return null;
			else
			{
				object obj = null;

				using (System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.Unicode))
				{
					System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(t);
					obj = xml.Deserialize(sr);
					sr.Close();
				}
				return obj;
			}
		}

		/// <summary>
		/// 保存配置
		/// </summary>
		/// <param name="cfg">要保存的配置对象</param>
		public static void SaveConfig(object cfg)
		{
			if (cfg == null) throw new ArgumentNullException();

			Type t = cfg.GetType();
			string path = System.IO.Path.Combine(ProfilePath, t.Name + ".xml");
			System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));

			using (System.IO.StreamWriter sr = new System.IO.StreamWriter(path, false, System.Text.Encoding.Unicode))
			{
				System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(t);
				xml.Serialize(sr, cfg);
				sr.Close();
			}
		}

		/// <summary>
		/// 保存配置
		/// </summary>
		/// <param name="cfg">要保存的配置对象</param>
		public static void SaveConfig(string configName, object cfg)
		{
			if (cfg == null) throw new ArgumentNullException();

			Type t = cfg.GetType();
			string path = System.IO.Path.Combine(ProfilePath, configName + ".xml");
			System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));

			using (System.IO.StreamWriter sr = new System.IO.StreamWriter(path, false, System.Text.Encoding.Unicode))
			{
				System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(t);
				xml.Serialize(sr, cfg);
				sr.Close();
			}
		}

		#endregion

		#region 存取资源文件

		/// <summary>
		/// 获得主题所在文件夹位置
		/// </summary>
		/// <returns></returns>
		public static string GetThemeFolderRoot()
		{
			return string.Format("{0}{1}Themes{1}", BinPath, System.IO.Path.DirectorySeparatorChar);
		}

		/// <summary>
		/// 获得主题资源文件夹位置
		/// </summary>
		/// <returns></returns>
		public static string GetThemeFolder()
		{
			return string.Format("{0}{2}Themes{2}{1}{2}", BinPath, Env.ClientConfig.Themes, System.IO.Path.DirectorySeparatorChar);
		}

		/// <summary>
		/// 获得资源图片位置
		/// </summary>
		/// <typeparam name="T">资源类型</typeparam>
		/// <returns></returns>
		public static string GetThemePicturePath<T>()
		{
			return string.Format("{0}{2}Themes{2}{1}{2}Toolbar{2}{3}.png", BinPath, Env.ClientConfig.Themes, System.IO.Path.DirectorySeparatorChar, typeof(T).Name);
		}

		/// <summary>
		/// 获得资源图片位置
		/// </summary>
		/// <returns></returns>
		public static string GetThemeFilePath(string catName, string fileName)
		{
			return string.Format("{0}{2}Themes{2}{1}{2}{3}{2}{4}{5}", BinPath, Env.ClientConfig.Themes, System.IO.Path.DirectorySeparatorChar, catName, fileName, fileName.IndexOf(".") > 0 ? "" : ".png");
		}

		/// <summary>
		/// 获得资源图标位置
		/// </summary>
		/// <typeparam name="T">资源类型</typeparam>
		/// <returns></returns>
		public static string GetThemeIconPath<T>()
		{
			return string.Format("{0}{2}Themes{2}{1}{2}FormIcon{2}{3}.ico", BinPath, Env.ClientConfig.Themes, System.IO.Path.DirectorySeparatorChar, typeof(T).Name);
		}

		/// <summary>
		/// 获得资源图标位置
		/// </summary>
		/// <returns></returns>
		public static string GetThemeIconPath(string catName, string fileName)
		{
			return string.Format("{0}{2}Themes{2}{1}{2}{3}{2}{4}.ico", BinPath, Env.ClientConfig.Themes, System.IO.Path.DirectorySeparatorChar, catName, fileName);
		}

		/// <summary>
		/// 获得资源图片(Alert图标)
		/// </summary>
		/// <returns></returns>
		public static System.Drawing.Image GetThemePicture_Alert()
		{
			return GetThemePicture("Signal", "Alert");
		}

		/// <summary>
		/// 获得资源图片
		/// </summary>
		/// <returns></returns>
		public static System.Drawing.Image GetThemePicture(string catName, string fileName)
		{
			string path = GetThemeFilePath(catName, fileName);
			if (System.IO.File.Exists(path)) return ImageHelper.LoadFromFile(path);
			else return null;
		}

		/// <summary>
		/// 获得资源图片
		/// </summary>
		/// <returns></returns>
		public static System.Drawing.Image GetThemePicture(this object obj, string cata)
		{
			string path = GetThemeFilePath(cata, obj.GetType().Name);
			if (System.IO.File.Exists(path)) return ImageHelper.LoadFromFile(path);
			else return null;
		}

		/// <summary>
		/// 获得资源图标
		/// </summary>
		/// <returns></returns>
		public static System.Drawing.Icon GetThemeIcon(string catName, string fileName)
		{
			string path = GetThemeIconPath(catName, fileName);
			if (System.IO.File.Exists(path)) return IconHelper.LoadIcon(path);
			else return null;
		}

		/// <summary>
		/// 获得资源图标
		/// </summary>
		/// <returns></returns>
		public static System.Drawing.Icon GetThemeIcon<T>()
		{
			string path = GetThemeIconPath<T>();
			if (System.IO.File.Exists(path)) return IconHelper.LoadIcon(path);
			else return null;
		}

		/// <summary>
		/// 获得资源图标
		/// </summary>
		/// <returns></returns>
		public static System.Drawing.Icon GetThemeIcon(this object obj)
		{
			return GetThemeIcon("FormIcon", obj.GetType().Name);
		}


		#endregion
	}
}
