using System;
using SysTmr = System.Timers.Timer;
using SwinGameSDK;
using Color = System.Drawing.Color;

namespace MyGame
{
	public class TextSpreader
	{
		public event EventHandler Finished;

		private string[] _text;
		private int _currentWord = 0;

		private int _duration;
		private int _interval;
		private Font _font = SwinGame.LoadFont("maven_pro_regular.ttf", 48);
		private SysTmr _tmr;

		public void Draw() {
			if (_tmr.Enabled)
			{
				SwinGame.DrawText (_text [_currentWord], Color.Black, _font, SwinGame.ScreenWidth () / 2 - SwinGame.TextWidth (_font, _text [_currentWord]) / 2, SwinGame.ScreenHeight () / 2 - SwinGame.TextHeight (_font, _text [_currentWord]) / 2);
			}
		}

		private void TimerTick (object sender, EventArgs e) {
			_currentWord += 1;
			if (_currentWord >= _text.Length)
			{
				_tmr.Stop ();
				if (Finished != null)
				{
					Finished (this, new EventArgs ());
				}
			}
		}

		public void Start() {
			_tmr.Start ();
		}

		public TextSpreader ( string text, int duration)
		{
			_text = text.Split(' ');
			_duration = duration;
			_interval = _duration / _text.Length;

			_tmr = new SysTmr (_interval);

			_tmr.Elapsed += TimerTick;

		}
	}
}

