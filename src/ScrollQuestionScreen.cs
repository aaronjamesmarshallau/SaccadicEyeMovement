using Color = System.Drawing.Color;
using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGame
{
	public class ScrollQuestionScreen : QuestionScreen
	{
		public event EventHandler StageChange;

		private Dictionary<string, List<string>> questions;
		private int _currentStage = 0;
		private int _maxStage = 3;
		private Color _backColor;
		private Color _foreColor;
		private Font _font = SwinGame.LoadFont("maven_pro_regular.ttf", 24);
		private TextStreamer _txtStream;
		private string _currentCorrectAnswer;
		private Button[] answerButtons;

		public override void Draw() {
			switch (_currentStage)
			{
			case 0:
				_txtStream.Draw ();
				break;
			default:
				int x = 10;
				int y = 10;
				string question = questions.Keys.ToList () [_currentStage - 1];
				List<string> answers = questions [question];

				SwinGame.DrawText (question, _foreColor, _font, x, y);
				for (int i = 0; i < answers.Count; i++)
				{
					x = 60;
					y = SwinGame.TextHeight (_font, "M");
					answerButtons [i].X = x - (answerButtons[i].Width + 5);
					answerButtons [i].Y = (i + 1) * (y + 10);
					if (answers[i].StartsWith("[ans]")) {
						string temp = answers[i].Substring(5, answers[i].Length - 5);
						SwinGame.DrawText(temp, _foreColor, _font, x, (i + 1) * (y + 10));
						_currentCorrectAnswer = answerButtons [i].Text.ToLower ();
					} else {
						SwinGame.DrawText (answers[i], _foreColor, _font, x, (i + 1) * (y + 10));
					}
					answerButtons [i].Draw ();
				}
				break;
			}
		}

		private int CurrentStage {
			get {
				return _currentStage;
			}
			set {
				_currentStage = value;
				if(StageChange != null) {
					StageChange(this, new EventArgs());
				}
			}
		}

		public override void Update() {
			foreach (Button b in answerButtons)
			{
				b.Update ();
			}
		}

		public override void Entering() {
			StageChange += ChangeOfStage;
			_txtStream.Start ();
		}

		public override void Leaving() {
			Font font = SwinGame.LoadFont ("maven_pro_regular.ttf", 32);
			int x = SwinGame.ScreenWidth () / 2 - SwinGame.TextWidth (font, "Stage Finished") / 2;
			int y = SwinGame.ScreenHeight () / 2 - SwinGame.TextHeight (font, "Stage Finished") / 2;

			for (int i = 0; i < 255; i++)
			{
				SwinGame.ClearScreen (_backColor);
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

		private void OnButtonClick(object sender, ButtonClickArgs e) {
			ScreenManager.UserAnswers.Add (RefVal.ToString() + ": " + e.Text.ToLower ());
			if (e.Text.ToLower () == _currentCorrectAnswer)
			{
				SM.CorrectScrollAnswers++;
			}
			CurrentStage++;
		}

		private void ChangeOfStage(object sender, EventArgs e) {
			if (_currentStage > _maxStage)
			{
				_currentStage--;
				SM.Return ();
			}
		}

		private void OnTextFinish(object sender, EventArgs e) {
			CurrentStage++;
		}

		public ScrollQuestionScreen (ScreenManager sm, string text, Dictionary<string, List<string>> questions, int time, Color backColor, Color foreColor, int val) : base(sm, text, val)
		{
			this.questions = questions;
			_backColor = backColor;
			_foreColor = foreColor;
			_txtStream = new TextStreamer (text, time);
			_txtStream.Finished += OnTextFinish;
			answerButtons = new Button[4];
			answerButtons [0] = new Button ("btnA", "A", 0, 0);
			answerButtons [1] = new Button ("btnB", "B", 0, 0);
			answerButtons [2] = new Button ("btnC", "C", 0, 0);
			answerButtons [3] = new Button ("btnD", "D", 0, 0);
			foreach (Button b in answerButtons)
			{
				b.ButtonClick += OnButtonClick;
			}
		}

		public ScrollQuestionScreen (ScreenManager sm, string text, Dictionary<string, List<string>> questions, int time, int val) : this(sm, text, questions, time, Color.White, Color.Black, val) { }
	}
}

