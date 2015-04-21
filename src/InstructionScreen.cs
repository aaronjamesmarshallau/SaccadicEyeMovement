using System;
using SwinGameSDK;
using System.Collections.Generic;
using System.Linq;

namespace MyGame
{
	public class InstructionScreen : Screen
	{
		public event EventHandler StageChange;

		private Button _btnNext;
		private TextDisplay _txt1;
		private TextDisplay _txt2;
		private int _currentStage;

		private const int Test1 = 18000;
		private const int Test2 = 15000;
		private const int Test3 = 12000;

		public int CurrentStage {
			get {
				return _currentStage;
			}
			set {
				_currentStage = value;
				if (StageChange != null)
				{
					StageChange (this, new EventArgs ());
				}
			}
		}

		public override void Draw() {
			_txt1.Draw ();
			_txt2.Draw ();
			_btnNext.Draw ();
		}

		public override void Update() {
			_btnNext.Update ();
		}

		public override void Entering() {

		}

		public override void Leaving() {

		}

		public override void Returning() {

		}

		private void ChangeStage(object sender, EventArgs e) {
			switch (_currentStage)
			{
			case 0:
				_txt2.Y = SwinGame.ScreenHeight () / 2;
				_txt1.Text = "Welcome to Aaron Marshall's Research Project! Thank you for taking part in my research. I made this program from scratch to measure user responses to saccadic eye movement. Saccadic Eye Movement is when your eye flicks from one word to the next. Dyslexic people often have trouble with their Saccadic Eye Movements (skipping words, repeating lines etc.)";
				_txt2.Text = "This research project aims to explore possibilities in altering IDE (Integrated Development Environments) and other text-based applications to more easily allow users to enter, and read, text on a screen.";
				break;
			case 1:
				_txt1.Text = "This research is being carried out in two parts; Testing user reading speed and testing user comprehension. Please be attentive during these tests. Performing them in silence, and with no distractions would be optimal.";
				_txt2.Text = "The first stage requires the user to read a normal block of text, then select what word they are up to when the timer runs out. You will be notified by a change of background color. Stop immediately, and select the word that you were able to read up to.";
				break;
			case 2:
				_txt1.Text = "The first stage is simply a control test, seeing how quickly a person can read under normal conditions.";
				_txt2.Text = "";
				break;
			case 3:
				_txt1.Text = "The second stage uses two separate methods to compare against standard reading techniques. The first method that is tested is a scrolling text effect, where text is scrolled in front of the user's eyes.";
				_txt2.Text = "The second method that is tested is speed reading, where words are flashed in front of the user's eyes. This technique is meant to lower eye strain, and reduce the pressure on your brain to calculate motion blurring etc.";
				break;
			case 4:
				_btnNext.Text = "Continue";
				_txt1.Text = "Click continue to get started!";
				_txt2.Text = "";
				break;
			case 5:
				Dictionary<string, List<Screen>> dict = new Dictionary<string, List<Screen>> ();
				List<Screen> tempTQS = new List<Screen> ();
				List<Screen> tempScrl = new List<Screen> ();
				List<Screen> tempSprd = new List<Screen> ();
				Random rand = new Random ();
				List<int> possVals = new List<int> ();
				int val = 0;

				for (int i = 0; i < ScreenManager.Texts.GetLength (0); i++)
				{
					possVals.Add (i);
				}

				val = possVals [rand.Next (possVals.Count - 1)];
				tempTQS.Add (new TextQuestionScreen (SM, ScreenManager.Texts [val, 0], Test1, val));
				possVals.Remove (val);

				val = possVals [rand.Next (possVals.Count - 1)];
				tempTQS.Add (new TextQuestionScreen (SM, ScreenManager.Texts [val, 0], Test2, val));
				possVals.Remove (val);

				val = possVals [rand.Next (possVals.Count - 1)];
				tempTQS.Add (new TextQuestionScreen (SM, ScreenManager.Texts [val, 0], Test3, val));
				possVals.Remove (val);

				dict.Add ("Text Questions", tempTQS);

				List<string> keys = ScreenManager.MCQuestions.Keys.ToList ();

				possVals = new List<int> ();

				for (int i = 0; i < ScreenManager.MCQuestions.Keys.Count; i++)
				{
					possVals.Add (i);
				}

				val = possVals [rand.Next (possVals.Count - 1)];
				string key = keys [val];
				tempScrl.Add (new ScrollQuestionScreen (SM, key, ScreenManager.MCQuestions [key], Test1, val));
				possVals.Remove (val);

				val = possVals [rand.Next (possVals.Count - 1)];
				key = keys [val];
				tempScrl.Add (new ScrollQuestionScreen (SM, key, ScreenManager.MCQuestions [key], Test2, val));
				possVals.Remove (val);

				val = possVals [rand.Next (possVals.Count - 1)];
				key = keys [val];
				tempScrl.Add (new ScrollQuestionScreen (SM, key, ScreenManager.MCQuestions[key], Test3, val));
				possVals.Remove (val);

				dict.Add ("Scroll Questions", tempScrl);



				val = possVals[rand.Next(possVals.Count - 1)];
				key = keys[val];
				tempSprd.Add (new SpreadQuestionScreen (SM, key, ScreenManager.MCQuestions[key], Test1, val));
				possVals.Remove(val);

				val = possVals[rand.Next(possVals.Count - 1)];
				key = keys[val];
				tempSprd.Add (new SpreadQuestionScreen (SM, key, ScreenManager.MCQuestions[key], Test2, val));
				possVals.Remove(val);

				val = possVals[rand.Next(possVals.Count - 1)];
				key = keys[val];
				tempSprd.Add (new SpreadQuestionScreen (SM, key, ScreenManager.MCQuestions[key], Test3, val));
				possVals.Remove(val);

				dict.Add ("Spread Questions", tempSprd);

				SM.Switch (new QuestionScreenManager(SM, dict));
				break;
			default:
				//do nothing
				break;
			}
			_txt1.Y = _txt2.Y - (_txt1.Height + 10);
		}

		private void ButtonClick(object sender, EventArgs e) {
			CurrentStage++;
		}

		public InstructionScreen (ScreenManager sm) : base(sm)
		{
			_btnNext = new Button ("btnNext", "Next", 0, 0);
			StageChange += ChangeStage;
			_btnNext.ButtonClick += ButtonClick;
			_btnNext.X = SwinGame.ScreenWidth () / 2 - _btnNext.Width / 2;
			_btnNext.Y = SwinGame.ScreenHeight () - (_btnNext.Height + 10);
			_txt1 = new TextDisplay ("", int.MaxValue);
			_txt2 = new TextDisplay ("", int.MaxValue);
			CurrentStage = 0;
		}
	}
}

