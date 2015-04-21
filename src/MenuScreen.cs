using System;
using System.Reflection;
using SwinGameSDK;
using Color = System.Drawing.Color;
using System.Collections.Generic;

namespace MyGame
{
	public class MenuScreen : Screen
	{
		private string[] headingText = new string[] {"Saccadic Eye Movement", "Visual Saliency"};
		private Font font = new Font ("maven_pro_regular.ttf", 24);
		private int headingY = -100;

		private Button btnStart;
		private Button btnQuit;

		private void DrawHeading(int y) {
			int x = 0;
			int textHeight = 0;
			int textWidth = 0;
			textHeight = SwinGame.TextHeight(this.font, headingText[0]);

			for (int i = 0; i < headingText.Length; i++)
			{
				textWidth = SwinGame.TextWidth (this.font, headingText [i]);
				x = SwinGame.ScreenWidth () / 2 - textWidth / 2;
				SwinGame.DrawText (headingText[i], Color.HotPink, this.font, x, y + (i * (textHeight + 10)));
			}
		}

		public void DrawBackground() {
			SwinGame.FillRectangle (Color.Black, 0, 0, SwinGame.ScreenWidth(), SwinGame.ScreenHeight());
		}

		public override void Draw() {
			DrawBackground ();
			DrawHeading (headingY);
			if (headingY < 100)
			{
				return;
			}
			btnStart.Draw ();
			btnQuit.Draw ();
		}

		public override void Update() {
			if (headingY < 100)
			{
				headingY++;
				return;
			}

			btnStart.Update ();
			btnQuit.Update ();
		}

		private void StartClick(object sender, ButtonClickArgs e) {
			SM.Switch (new InstructionScreen(SM));
		}

		private void QuitClick(object sender, ButtonClickArgs e) {
			SM.Stop ();
		}

		public override void Entering() {
			btnStart.Update ();
			btnQuit.Update ();
			btnStart.AutoSize = false;
			btnQuit.AutoSize = false;
			btnStart.Width = 200;
			btnQuit.Width = 200;
			btnStart.X = SwinGame.ScreenWidth () / 2 - btnStart.Width / 2;
			btnQuit.X = SwinGame.ScreenWidth () / 2 - btnQuit.Width / 2;
			btnStart.Y = SwinGame.ScreenHeight () - (btnStart.Height + 200);
			btnQuit.Y = SwinGame.ScreenHeight () - (btnQuit.Height + (190 - btnStart.Height));
			btnStart.BackColor = Color.HotPink;
			btnStart.ForeColor = Color.White;
			btnQuit.BackColor = Color.HotPink;
			btnQuit.ForeColor = Color.White;
			btnStart.ButtonClick += StartClick;
			btnQuit.ButtonClick += QuitClick;
		}

		public override void Leaving() {

		}

		public override void Returning() {

		}

		public MenuScreen(ScreenManager sm) : base(sm) {
			btnStart = new Button ("btnStart", "Start", 0, 0);
			btnQuit = new Button ("btnQuit", "Quit", 0, 0);
		}
	}

}