using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IPMessagerNet.API;

namespace IPMessagerNet.Core
{
	internal class ServiceManager
	{
		public static IEditorService GetEditor()
		{
			return new UI.Controls.Editor.TextEditor();
		}
	}
}
