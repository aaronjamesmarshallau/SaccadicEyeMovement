using System;
using SysTmr = System.Timers.Timer;
using SwinGameSDK;
using Color = System.Drawing.Color;

namespace MyGame
{
	public class TextStreamer
	{
		public event EventHandler Finished;

		private string _text;
		private float _speed;
		private int _time;
		private Color _textColor;
		private Font _font = SwinGame.LoadFont("maven_pro_regular.ttf", 48);
		private float _x = SwinGame.ScreenWidth();
		private float _y;
		private SysTmr _tmr;

		public string Text {
			get {
				return _text;
			}
			set {
				_text = value;
			}
		}

		public float Speed {
			get {
				return _speed;
			}
		}

		public int Time {
			get {
				return _time;
			}
			set {
				_time = value;
			}
		}

		public Color TextColor {
			get {
				return _textColor;
			}
			set {
				_textColor = value;
			}
		}

		public void Start() {
			_tmr.Start ();
		}

		public void Draw() {
			if (_tmr.Enabled)
			{
				SwinGame.DrawText (_text, _textColor, _font, _x, _y);
			}
		}

		public void Update(object sender, EventArgs e) {
			if (_tmr.Enabled)
			{
				_x -= _speed;
				if (_x <= SwinGame.ScreenWidth () - 100 - SwinGame.TextWidth (_font, _text))
				{
					_tmr.Stop ();
					if (Finished != null)
					{
						Finished (this, new EventArgs ());
					}
				}
			}
		}

		public TextStreamer (string text, int time, Color textColor)
		{
			_text = text;
			_time = time;
			_textColor = textColor;
			_speed = SwinGame.TextWidth(_font, _text) / (time / 10);
			_tmr = new SysTmr (10);
			_tmr.Elapsed += Update;
			_y = SwinGame.ScreenHeight () / 2 - SwinGame.TextHeight (_font, "M") / 2;
		}

		public TextStreamer(string text, int time) : this(text, time, Color.Black) { }
	}
}

