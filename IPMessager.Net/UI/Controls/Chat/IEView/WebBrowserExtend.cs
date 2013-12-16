using System;
using System.Windows.Forms;

namespace IPMessagerNet.UI.Controls.Chat.IEView
{
	public static class WebBrowserExtend
	{
		public static bool IsReady(this WebBrowser browser)
		{
			return browser.ReadyState == WebBrowserReadyState.Complete;
		}

		public static object InvokeScript(this WebBrowser browser, string scriptName, params object[] param)
		{
			if (browser.Document == null || !browser.IsReady()) return null;

			return browser.Document.InvokeScript(scriptName, param);
		}
	}
}
