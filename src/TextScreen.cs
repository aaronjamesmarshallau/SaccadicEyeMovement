using SwinGameSDK;
using System;
using System.Collections.Generic;
using Color = System.Drawing.Color;

namespace MyGame
{
	public class TextScreen : Screen
	{
		private string _text;
		private List<string> lines;
		private int height;
		private int width;
		private Font _font = SwinGame.LoadFont("maven_pro_regular.ttf", 24);
		private Button _btnFinish;

		public override void Draw() {
			int x = (int)(SwinGame.ScreenWidth() * 0.125f);
			int y = (int)(SwinGame.ScreenHeight() * 0.125f);
			int txtHeight = SwinGame.TextHeight (_font, _text);

			for (int i = 0; i < lines.Count; i++)
			{
				SwinGame.DrawText (lines [i], Color.Black, _font, x, y + i * (txtHeight + 5));
			}

			_btnFinish.Draw ();
		}

		public override void Update() {
			_btnFinish.Update ();
		}

		public override void Entering() {
			_btnFinish.ButtonClick += ButtonClick;
			_btnFinish.X = SwinGame.ScreenWidth () / 2 - _btnFinish.Width / 2;
			_btnFinish.Y = SwinGame.ScreenHeight () - (_btnFinish.Height + 5);
			GetLines ();
		}

		public override void Leaving() {
			Font font = SwinGame.LoadFont ("maven_pro_regular.ttf", 32);
			int x = SwinGame.ScreenWidth () / 2 - SwinGame.TextWidth (font, "Stage Finished") / 2;
			int y = SwinGame.ScreenHeight () / 2 - SwinGame.TextHeight (font, "Stage Finished") / 2;

			for (int i = 0; i < 255; i++)
			{
				SwinGame.ClearScreen (Color.White);
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

		private void ButtonClick(object sender, ButtonClickArgs e) {
			SM.Stop ();
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

		public TextScreen (ScreenManager sm, string text) : base(sm) {
			_text = text;
			width = (int)(SwinGame.ScreenWidth () * 0.75);
			height = (int)(SwinGame.ScreenHeight () * 0.75);
			_btnFinish = new Button ("btnFinish", "Finish", 0, 0);
		}
	}

}

