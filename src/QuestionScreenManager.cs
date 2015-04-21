using SwinGameSDK;
using System;
using System.Collections.Generic;

namespace MyGame
{
	public class QuestionScreenManager : Screen
	{	
		private Dictionary<string, List<Screen>> _screens = new Dictionary<string, List<Screen>> ();
		private Dictionary<string, List<Button>> _buttons;

		/// <summary>
		/// Gets the screens.
		/// </summary>
		/// <value>The screens.</value>
		public Dictionary<string, List<Screen>> Screens {
			get {
				return _screens;
			}
		}

		public bool IsFinished {
			get {
				bool finished = true;
				foreach (KeyValuePair<string, List<Button>> kvp in _buttons) {
					foreach (Button btn in kvp.Value)
					{
						if (btn.Enabled == true)
						{
							finished = false;
						}
					}
				}
				return finished;
			}
		}

		public int ScreenCount {
			get {
				int count = 0;
				foreach (KeyValuePair<string, List<Screen>> kvp in _screens) {
					for (int i = 0; i < kvp.Value.Count; i++)
					{
						count++;
					}
				}
				return count;
			}
		}

		public override void Draw() {
			foreach (KeyValuePair<string, List<Button>> kvp in _buttons) {
				foreach (Button btn in kvp.Value)
				{
					btn.Draw ();
				}
			}
		}

		public override void Update() {
			foreach (KeyValuePair<string, List<Button>> kvp in _buttons) {
				foreach (Button btn in kvp.Value)
				{
					btn.Update ();
				}
			}
		}

		public override void Entering() {
			_buttons = new Dictionary<string, List<Button>> ();
			int count = 0;
			int friendlyCount = 1;
			int width = 200;
			int height = 30;
			int x = SwinGame.ScreenWidth () / 2 - width / 2;
			int y = 0;
			List<Button> btnList = new List<Button> ();
			Button tempBtn;

			foreach (KeyValuePair<string, List<Screen>> kvp in _screens)
			{
				foreach (Screen s in kvp.Value)
				{
					tempBtn = new Button ("btn" + kvp.Key + count.ToString (), kvp.Key + " " + friendlyCount, x, y + 60 + (height + 10) * (friendlyCount - 1) + 150 * count, width, height);
					tempBtn.ButtonClick += ButtonClick;
					btnList.Add (tempBtn);
					friendlyCount++;
				}
				count++;
				_buttons.Add (kvp.Key, btnList);
				friendlyCount = 1;
			}
		}

		private int GetButtonValueIndex(Dictionary<string, List<Button>> dict, Button value) {
			int count = 0;
			foreach (KeyValuePair<string, List<Button>> kvp in dict)
			{
				foreach (Button btn in kvp.Value)
				{
					count++;
				}
			}
			return count;
		}

		private void ButtonClick(object sender, ButtonClickArgs e) {
			string btnSection = e.Text.Substring (0, e.Text.Length - 2);
			int number = Convert.ToInt32(e.Text.Substring (e.Text.Length - 1, 1)) - 1;
			List<Screen> screens = _screens [btnSection];
			e.Button.Enabled = false;

			if (screens != null) {
				for (int i = 0; i < screens.Count; i++) {
					if (i == number)
					{
						SM.Add (screens [i]);
					}
				}
			}
		}

		public override void Leaving() {

		}

		public override void Returning() {
			if (IsFinished)
			{
				SM.ConsoleDump ();
				SM.WriteResults ();
				SM.Switch (new TextScreen(SM, "That concludes my awesome research project! Thank you very much for taking part! \\n In order to submit your results, simply send the \"results.txt\" file on your Desktop to either:\\n 7666071@student.swin.edu.au \\n or send it to Aaron Marshall via Facebook!\\n The research will be anonymous, and all data will be confidential!\\n Thank you again for you participation!"));
			}
		}

		public QuestionScreenManager (ScreenManager sm, Dictionary<string, List<Screen>> screens) : base (sm)
		{
			_screens = screens;
		}
	}
}

