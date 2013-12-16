using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IPMessagerNet.Core;
using IPMessagerNet._Embed;

namespace IPMessagerNet.UI.Base
{
	public class DialogBase : FormBase
	{
		public DialogBase()
		{
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

			if (Env.ClientConfig != null)
			{
				this.Icon = this.GetThemeIcon();

				if (Environment.OSVersion.Version.Major < 5)
				{
					FadeEffectComponent fec = new FadeEffectComponent()
					{
						ParentForm = this
					};
				}
			}
		}
	}
}
