using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IPMessagerNet.Utility
{
	static class Helper
	{

		static Regex jsConventor = new Regex(@"['""\\]");

		/// <summary>
		/// 将字符串转换为JS格式
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string ConvertJsString(string str)
		{
			if (string.IsNullOrEmpty(str)) return string.Empty;
			return jsConventor.Replace(str, (s) => { return string.Concat("\\", s.Value); }).Replace("\r", "\\r").Replace("\n", "\\n");
		}
	}
}
