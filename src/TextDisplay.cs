using System;
using SwinGameSDK;
using Color = System.Drawing.Color;
using SysTimer = System.Timers.Timer;
using System.Diagnostics;
using System.Collections.Generic;

namespace MyGame
{
	public class TextDisplay
	{
		public delegate void TextSelectHandler (object sender, TextSelectArgs e);

		public event EventHandler Finished;
		public event TextSelectHandler Selected;

		private string _text;
		private List<string> lines;
		private int _totalTime;
		private Font _font;
		private Color _foreColor;
		private SysTimer _timer;
		private float x = SwinGame.ScreenWidth() * 0.125f;
		private float y = SwinGame.ScreenHeight () * 0.125f;
		private int width = (int)(SwinGame.ScreenWidth() * 0.75f);
		private int height;
		private Rectangle selectedRect = new Rectangle();
		private string selectedWord;
		private int selectedIndex;

		public int Width {
			get {
				return this.width;
			}
			set {
				this.width = value;
			}
		}

		public int Height {
			get {
				return this.height;
			}
		}

		public float X {
			get {
				return this.x;
			}
			set {
				this.x = value;
			}
		}

		public float Y {
			get {
				return this.y;
			}
			set {
				this.y = value;
			}
		}

		public string Text {
			get {
				return _text;
			}
			set {
				_text = value;
				GetLines ();
			}
		}

		public int TotalTime {
			get {
				return _totalTime;
			}
			set {
				_totalTime = value;
			}
		}

		public Color ForeColor {
			get {
				return _foreColor;
			}
			set {
				_foreColor = value;
			}
		}

		public Font Font {
			get {
				return _font;
			}
			set {
				_font = value;
			}
		}

		public void Start() {
			_timer.Start ();
		}

		public void Stop() {
			_timer.Stop ();
			if (Finished != null)
			{
				Finished (this, new EventArgs ());
			}
		}

		public void Draw() {
			if (!_timer.Enabled)
			{
				SwinGame.FillRectangle (Color.Pink, selectedRect);
			}
			for (int i = 0; i < lines.Count; i++)
			{
				SwinGame.DrawText (lines [i], _foreColor, _font, x, y + SwinGame.TextHeight (_font, lines [i]) * i);
			}
		}

		private void GetSelection() {
			string[] temp;
			int accrued;
			int accruedIndex = 0;
			int rY, rX, rWidth, rHeight;
			Point2D mousePos = SwinGame.MousePosition();

			for (int i = 0; i < lines.Count; i++)
			{
				temp = lines [i].Split (' ');
				accrued = 0;
				rY = (int)(y + i * SwinGame.TextHeight (_font, lines[0]));
				for (int c = 0; c < temp.Length; c++)
				{
					rX = (int)(x + accrued);
					rWidth = SwinGame.TextWidth (_font, temp [c]);
					rHeight = SwinGame.TextHeight (_font, temp [c]);

					if (SwinGame.PointInRect (mousePos, rX, rY, rWidth, rHeight))
					{
						selectedRect.X = rX;
						selectedRect.Y = rY;
						selectedRect.Width = rWidth;
						selectedRect.Height = rHeight;
						selectedWord = temp [c];
						selectedIndex = accruedIndex + c;
						return;
					}
					else
					{
						selectedRect.X = 0;
						selectedRect.Y = 0;
						selectedRect.Width = 0;
						selectedRect.Height = 0;
						selectedWord = "";
						accrued += SwinGame.TextWidth (_font, temp [c] + " ");
					}
				}
				accruedIndex += temp.Length - 1;
			}
		}

		public void Update() {
			if (!_timer.Enabled)
			{
				GetSelection ();
			}
			if (SwinGame.MouseClicked (MouseButton.LeftButton) && (selectedWord != ""))
			{
				if (Selected != null)
				{
					Selected (this, new TextSelectArgs (selectedWord, selectedRect, selectedIndex));
				}
			}
		}

		private void GetLines() {
			lines = new List<string> ();
			string[] txtArr = _text.Split (' ');
			bool fin;
			string temp;
			int count = 0;
			int i = 0;

			while(txtArr.Length > count)
			{
				fin = false;
				temp = "";

				while (!fin && (txtArr.Length > count))
				{
					if (SwinGame.TextWidth(_font, temp + txtArr [count]) < width)
					{
						temp += txtArr [count] + " ";
						count++;

						if (temp.Contains("\\n ")) {
							fin = true;
							temp = temp.Substring (0, temp.Length - 3);
						} else if (temp.Contains("\n ")) {
							fin = true;
							temp = temp.Substring (0, temp.Length - 2);
						}
					}
					else
					{
						fin = true;
					}
				}
				lines.Add(temp);
				i++;
			}
			this.height = SwinGame.TextHeight (_font, "V") * lines.Count;
		}

		private void TimerTick(object sender, EventArgs e) {
			this.Stop ();
		}

		public TextDisplay (string text, int time, Color foreColor)
		{
			_text = text;
			_totalTime = time;
			_foreColor = foreColor;
			_timer = new SysTimer (time);
			_timer.Elapsed += TimerTick;
			_font = SwinGame.LoadFont ("arial.ttf", 20);
			GetLines ();
		}

		public TextDisplay (string text, int time) : this (text, time, Color.Black) {

		}
	}
}

