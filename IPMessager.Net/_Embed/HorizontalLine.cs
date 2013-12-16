using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IPMessagerNet._Embed
{
	public partial class HorizontalLine : System.Windows.Forms.Control
	{
		public HorizontalLine()
		{
			InitializeComponent();

			SetStyle(ControlStyles.SupportsTransparentBackColor, true);

			TextAlign = ContentAlignment.MiddleCenter;
			PanelAlign = ContentAlignment.MiddleCenter;
			LineColor = SystemColors.WindowFrame;
			BackColor = Color.Transparent;
			LineHeight = 1;

			this.FontChanged += (object sender, EventArgs e) => this.Invalidate();
			this.TextChanged += (object sender, EventArgs e) => this.Invalidate();
			this.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.TextFont = new Font("Tahoma", 9);
			TotalWidth = 100;

			this.DoubleBuffered = true;
		}

		protected override CreateParams CreateParams
		{
			get
			{
				System.Windows.Forms.CreateParams ps = base.CreateParams;

				ps.ExStyle = ps.ExStyle | 0x20;
				return ps;
			}
		}

		private ContentAlignment _textalign;
		/// <summary>
		/// 文本对齐选项
		/// </summary>
		[DisplayName("文字对齐"), Description("文字在显示时如何对齐")]
		public System.Drawing.ContentAlignment TextAlign { get { return _textalign; } set { _textalign = value; this.Invalidate(); } }

		private ContentAlignment _panelalign;
		/// <summary>
		/// 对象对齐选项
		/// </summary>
		[DisplayName("对象对齐"), Description("对象在显示时如何对齐")]
		public System.Drawing.ContentAlignment PanelAlign { get { return _panelalign; } set { _panelalign = value; this.Invalidate(); } }

		Color _linecolor;
		/// <summary>
		/// 线条颜色
		/// </summary>
		[DisplayName("线条颜色"), Description("线条颜色")]
		public System.Drawing.Color LineColor { get { return _linecolor; } set { _linecolor = value; this.Invalidate(); } }

		int _lineheight;
		/// <summary>
		/// 横线高度
		/// </summary>
		[DisplayName("线条高度"), Description("线条高度"), DefaultValue(1)]
		public int LineHeight { get { return _lineheight; } set { _lineheight = value; this.Invalidate(); } }

		/// <summary>
		/// 文本字体
		/// </summary>
		[DisplayName("文本字体"), Description("文本字体")]
		public Font TextFont
		{
			get { return this.Font; }
			set { this.Font = value; this.Invalidate(); }
		}

		/// <summary>
		/// 文本颜色
		/// </summary>
		[DisplayName("文本颜色"), Description("文本颜色")]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
				this.Invalidate();
			}
		}

		int _totalWidth;

		/// <summary>
		/// 线条总宽度
		/// </summary>
		[DisplayName("线条总宽度"), Description("线条总宽度"), DefaultValue(100)]
		public int TotalWidth
		{
			get { return _totalWidth; }
			set
			{
				if (TotalWidth < 0 || TotalWidth > 100) throw new System.ArgumentOutOfRangeException("TotalWidth", "总宽度必须位于 0 - 100 之间");
				_totalWidth = value; this.Invalidate();
			}
		}

		/// <summary>
		/// 背景色
		/// </summary>
		[DisplayName("背景色"), Description("背景色")]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			float rectwidth = this.Width * TotalWidth / 100;
			float rectHeight;

			float startX, startY, endX, endY;
			float textHeight, textWidth;
			SizeF textSize;

			Graphics g = pe.Graphics;

			//确定文字大小
			textSize = g.MeasureString(this.Text, Font);
			textHeight = textSize.Height;
			textWidth = textSize.Width;
			rectHeight = textHeight > LineHeight ? textHeight : LineHeight;
			if (rectHeight > this.Height) rectHeight = this.Height;

			if (LineHeight > rectHeight) LineHeight = (int)rectHeight;

			//起点
			if (TextAlign == ContentAlignment.BottomCenter || TextAlign == ContentAlignment.MiddleCenter || TextAlign == ContentAlignment.TopCenter)
			{
				startX = (this.Width - rectwidth) / 2;
			}
			else if (TextAlign == ContentAlignment.BottomLeft || TextAlign == ContentAlignment.MiddleLeft || TextAlign == ContentAlignment.TopLeft)
			{
				startX = 0;
			}
			else
			{
				startX = this.Width - rectwidth;
			}
			if (TextAlign == ContentAlignment.BottomCenter || TextAlign == ContentAlignment.BottomLeft || TextAlign == ContentAlignment.BottomRight)
			{
				startY = this.Height - rectHeight;
			}
			else if (TextAlign == ContentAlignment.MiddleCenter || TextAlign == ContentAlignment.MiddleLeft || TextAlign == ContentAlignment.MiddleRight)
			{
				startY = (this.Height - rectHeight) / 2;
			}
			else
			{
				startY = 0;
			}

			//终点
			endX = startX + rectwidth;
			endY = startY + rectHeight;

			int textStartX, textStartY, lineStartX, lineStartY;
			textStartY = textStartX = lineStartY = lineStartX = 0;

			switch (_textalign)
			{
				case ContentAlignment.BottomCenter:
					textStartY = (int)(rectHeight - textHeight) + 1;
					textStartX = (int)((rectwidth - textWidth) / 2);
					lineStartX = 0;
					lineStartY = textStartY - 1;
					break;
				case ContentAlignment.BottomLeft:
					textStartY = (int)(rectHeight - textHeight) + 1;
					textStartX = 0;
					lineStartX = 0;
					lineStartY = textStartY - 1;
					break;
				case ContentAlignment.BottomRight:
					textStartY = (int)(rectHeight - textHeight) + 1;
					textStartX = (int)((rectwidth - textWidth));
					lineStartX = 0;
					lineStartY = textStartY - 1;
					break;
				case ContentAlignment.MiddleCenter:
					textStartY = (int)((rectHeight - textHeight) / 2);
					textStartX = (int)((rectwidth - textWidth) / 2);
					lineStartX = 0;
					lineStartY = (int)(rectHeight / 2);
					break;
				case ContentAlignment.MiddleLeft:
					textStartY = (int)((rectHeight - textHeight) / 2);
					textStartX = 0;
					lineStartX = (int)(textStartY + textWidth + 1);
					lineStartY = (int)(rectHeight / 2);
					break;
				case ContentAlignment.MiddleRight:
					textStartY = (int)((rectHeight - textHeight) / 2);
					textStartX = (int)((rectwidth - textWidth));
					lineStartX = 0;
					lineStartY = (int)(rectHeight / 2);
					break;
				case ContentAlignment.TopCenter:
					textStartY = 0;
					textStartX = (int)((rectwidth - textWidth) / 2);
					lineStartX = 0;
					lineStartY = (int)(textHeight + 1);
					break;
				case ContentAlignment.TopLeft:
					textStartY = 0;
					textStartX = 0;
					lineStartX = 0;
					lineStartY = (int)(textHeight + 1);
					break;
				case ContentAlignment.TopRight:
					textStartY = 0;
					textStartX = (int)((rectwidth - textWidth));
					lineStartX = 0;
					lineStartY = (int)(textHeight + 1);
					break;
				default:
					break;
			}

			//开始画中间的线
			Pen p = new Pen(LineColor);

			if (TextAlign == ContentAlignment.MiddleCenter)
			{
				int singleLienWidth = (int)(rectwidth / 2 - textWidth / 2);
				g.FillRectangle(new SolidBrush(LineColor), new Rectangle(new Point(lineStartX, lineStartY), new Size(singleLienWidth, LineHeight)));
				g.FillRectangle(new SolidBrush(LineColor), new Rectangle(new Point((int)(lineStartX + singleLienWidth + textWidth), lineStartY), new Size(singleLienWidth, LineHeight)));
			}
			else if (_textalign == ContentAlignment.MiddleLeft || _textalign == ContentAlignment.MiddleRight)
			{
				int singleLienWidth = (int)(rectwidth - textWidth);
				g.FillRectangle(new SolidBrush(LineColor), new Rectangle(new Point(lineStartX, lineStartY), new Size(singleLienWidth, LineHeight)));
			}
			else
			{
				g.FillRectangle(new SolidBrush(LineColor), new Rectangle(new Point(lineStartX, lineStartY), new Size((int)rectwidth, LineHeight)));
			}

			//将文字要出现的地方涂抹掉
			//g.FillRectangle(new SolidBrush(BackColor), new Rectangle(new Point(singleLienWidth, (int)((rectHeight - textHeight) / 2)), new Size((int)textWidth, (int)textHeight)));

			//书写文字
			g.DrawString(Text, TextFont, new SolidBrush(ForeColor), new PointF(textStartX, textStartY));
		}
	}
}
