using System;
using System.Reflection;
using SwinGameSDK;
using Color = System.Drawing.Color;
using SysTmr = System.Timers.Timer;

namespace MyGame
{
	public class TextQuestionScreen : QuestionScreen
	{
		private int _duration;
		private TextDisplay _tdisplay;
		private Color _backColor;

		public Color BackColor {
			get {
				return _backColor;
			}
			set {
				_backColor = value;
			}
		}

		public int Duration {
			get {
				return _duration;
			}
		}

		public override void Draw() {
			SwinGame.FillRectangle (_backColor, 0, 0, SwinGame.ScreenWidth (), SwinGame.ScreenHeight ());
			_tdisplay.Draw ();
		}

		public override void Update() {
			_tdisplay.Update ();
		}

		public override void Entering() {

			_tdisplay.Start ();
		}

		public override void Leaving() {
			Font font = SwinGame.LoadFont ("maven_pro_regular.ttf", 32);
			int x = SwinGame.ScreenWidth () / 2 - SwinGame.TextWidth (font, "Stage Finished") / 2;
			int y = SwinGame.ScreenHeight () / 2 - SwinGame.TextHeight (font, "Stage Finished") / 2;

			for (int i = 0; i < 255; i++)
			{
				SwinGame.ClearScreen (this.BackColor);
				SwinGame.ProcessEvents ();

				if (SwinGame.WindowCloseRequested ())
				{
					SM.Stop ();
				}

				Draw ();

				SwinGame.FillRectangle (Color.FromArgb (i, 255, 255, 255), 0, 0, SwinGame.ScreenWidth (), SwinGame.ScreenHeight ());
				SwinGame.DrawText ("Stage Finished", Color.Black, font, x, y);

				SwinGame.RefreshScreen ();
			}
		}

		public override void Returning() {

		}

		private void OnFinish(object sender, EventArgs e) {
			_backColor = Color.SkyBlue;
		}

		private void OnSelect(object sender, TextSelectArgs e) {
			this.Answer = e.SelectedWord;
			ScreenManager.AddAnswer (RefVal + ": " + this.Answer + ", (" + e.SelectedIndex + ")");
			SM.Return ();
		}

		public TextQuestionScreen(ScreenManager sm, string text, int time, Color backColor, Color foreColor, int val) : base (sm, text, val) {
			_duration = time;
			_backColor = backColor;
			_tdisplay = new TextDisplay (this.Question, _duration, foreColor);
			_tdisplay.Finished += OnFinish;
			_tdisplay.Selected += OnSelect;
		}

		public TextQuestionScreen (ScreenManager sm, string text, int time, int val) : this(sm, text, time, Color.White, Color.Black, val) { }
	}



}