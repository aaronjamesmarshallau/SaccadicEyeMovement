using System;
using System.Reflection;
using SwinGameSDK;
using Color = System.Drawing.Color;
using SysTmr = System.Timers.Timer;

namespace MyGame
{
	public abstract class QuestionScreen : Screen
	{
		private string _question;
		private string _answer;
		private int _refVal;

		public int RefVal {
			get {
				return _refVal;
			}
			set {
				_refVal = value;
			}
		}

		public string Answer {
			get {
				return _answer;
			}
			set {
				_answer = value;
			}
		}

		public string Question {
			get {
				return _question;
			}
			set {
				_question = value;
			}
		}

		public QuestionScreen (ScreenManager sm, string question, int refVal) : base(sm) {
			_question = question;
			this._refVal = refVal;
		}
	}
}