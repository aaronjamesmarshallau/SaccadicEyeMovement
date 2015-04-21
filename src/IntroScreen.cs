using System;
using System.Reflection;
using SwinGameSDK;
using Color = System.Drawing.Color;
using SysTmr = System.Timers.Timer;

namespace MyGame
{
	public class IntroScreen : Screen
	{
		private SysTmr tmr;
		private string[,] text;
		private int stage = 0;
		private Font font = new Font ("maven_pro_regular.ttf", 36);

		private int maxStage = 1;

		private void DrawBackground() {
			SwinGame.FillRectangle (Color.White, 0, 0, SwinGame.ScreenWidth (), SwinGame.ScreenHeight ());
		}

		private void DrawText() {
			int x = 0;
			int y = 0;
			for (int i = 0; i < text.GetLength (1); i++)
			{
				x = SwinGame.ScreenWidth () / 2 - SwinGame.TextWidth (font, text [stage, i]) / 2;
				y = SwinGame.ScreenHeight () / 2 - ((SwinGame.TextHeight (font, text [stage, i]) / 2 + 10) * -i) - 36;
				SwinGame.DrawText (text [stage, i], Color.Black, font, x, y);
			}
		}

		private void DrawFade(float i) {
			float opacity = (i / 360) * 255;
			SwinGame.FillRectangle(Color.FromArgb(Convert.ToInt32(opacity),255,255,255), 0, 0, SwinGame.ScreenWidth(), SwinGame.ScreenHeight());
		}

		public override void Draw() {
			DrawBackground ();
			DrawText ();
		}

		public override void Update() {

		}

		private void TimerTick(object sender, EventArgs e) {
			if (stage == maxStage)
			{
				Random rand = new Random ();
				tmr.Stop ();
				SM.Switch (new MenuScreen (SM));
			}
			stage++;
		}

		public override void Entering() {
			this.tmr.Elapsed += TimerTick;
			this.tmr.Start ();
			SwinGame.HideMouse ();
			for (int i = 360; i > 0; i--)
			{
				SwinGame.ClearScreen (Color.White);
				SwinGame.ProcessEvents ();
				if (SwinGame.WindowCloseRequested ())
				{
					SM.Stop ();
				}

				Draw ();
				DrawFade (i);

				SwinGame.RefreshScreen ();
			}
		}

		public override void Leaving() {
			for (int i = 0; i < 360; i++)
			{
				SwinGame.ClearScreen (Color.White);
				SwinGame.ProcessEvents ();
				if (SwinGame.WindowCloseRequested ())
				{
					SM.Stop ();
				}

				Draw ();
				DrawFade (i);

				SwinGame.RefreshScreen ();
			}
			SwinGame.ShowMouse ();
		}

		public override void Returning() {

		}

		public IntroScreen (ScreenManager sm, string[,] text) : base (sm) {
			this.tmr = new SysTmr (5000);
			this.text = text;
			this.maxStage = this.text.GetLength (0) - 1;
		}
	}


}