using System;
using SwinGameSDK;
using System.Collections.Generic;
using Color = System.Drawing.Color;
using System.IO;

namespace MyGame
{
	public class ScreenManager
	{
		private Stack<Screen> screens = new Stack<Screen>();
		private bool doQuit = false;
		private int correctScrollAnswers = 0;
		private int correctSpreadAnswers = 0;

		private static string[,] texts = new string[10,2];
		private static List<string> answers = new List<string>();

		private static Dictionary<string, Dictionary<string, List<string>>> mcQuestions = new Dictionary<string, Dictionary<string, List<string>>> ();

		public static List<string> UserAnswers {
			get {
				return ScreenManager.answers;
			}
		}

		public void ConsoleDump() {
			Console.WriteLine ("Number of correct Scroll Answers: {0}", correctScrollAnswers);
			Console.WriteLine ("Number of correct Spread Answers: {0}", correctSpreadAnswers);
			Console.WriteLine ("User answers were: ");
			foreach (string s in ScreenManager.UserAnswers)
			{
				Console.WriteLine ("    " + s);
			}
		}

		public void WriteResults() {
			string path = Environment.GetFolderPath (Environment.SpecialFolder.Desktop) + "\\results.txt";
			StreamWriter sw = new StreamWriter (path);

			sw.WriteLine("Number of correct Scroll Answers: {0}", correctScrollAnswers);
			sw.WriteLine ("Number of correct Spread Answers: {0}", correctSpreadAnswers);
			sw.WriteLine ("User answers were: ");
			foreach (string s in ScreenManager.UserAnswers)
			{
				sw.WriteLine ("    " + s);
			}

			sw.Close ();
			sw.Dispose();
		}

		public static void AddAnswer(string answer) {
			answers.Add (answer);
		}

		/// <summary>
		/// Gets the possible texts.
		/// </summary>
		/// <value>The texts.</value>
		public static string[,] Texts {
			get {
				return texts;
			}
		}

		public static Dictionary<string, Dictionary<string, List<string>>> MCQuestions {
			get {
				return mcQuestions;
			}
		}

		public int CorrectScrollAnswers {
			get {
				return correctScrollAnswers;
			}
			set {
				correctScrollAnswers = value;
			}
		}

		public int CorrectSpreadAnswers {
			get  {
				return correctSpreadAnswers;
			}
			set {
				correctSpreadAnswers = value;
			}
		}

		private static void LoadTexts() {
			string[] fileNames = Directory.GetFiles ("./Resources/texts/");

			for (int i = 0; i < fileNames.Length; i++)
			{
				StreamReader sr = new StreamReader (fileNames[i]);

				texts [i, 0] = sr.ReadLine (); //Text
				texts [i, 1] = sr.ReadLine (); //Source

				sr.Close ();
				sr.Dispose ();
			}
		}

		private static void LoadMCQuestions() {
			string[] fileNames = Directory.GetFiles ("./Resources/mcQuestions/");
			string questionText = "";
			string question = "";
			List<string> mcanswers;
			string[] temp = new string[5];
			Dictionary<string, List<string>> tempDict;

			for (int i = 0; i < fileNames.Length; i++)
			{
				StreamReader sr = new StreamReader (fileNames[i]);

				tempDict = new Dictionary<string, List<string>> ();

				questionText = sr.ReadLine ();
				do
				{
					mcanswers = new List<string> ();
					temp = sr.ReadLine ().Split (';');
					question = temp [0];
					for (int b = 1; b < temp.Length; b++)
					{
						mcanswers.Add (temp [b]);
					}
					tempDict.Add (question, mcanswers);
				} while(!sr.EndOfStream);

				mcQuestions.Add (questionText, tempDict);

				sr.Close ();
				sr.Dispose ();
			}
		}

		public static void Load() {
			LoadTexts ();
			LoadMCQuestions ();
		}

		/// <summary>
		/// Gets the current screen.
		/// </summary>
		/// <value>The current screen.</value>
		public Screen CurrentScreen {
			get {
				if (screens.Count > 0)
				{
					return screens.Peek ();
				}
				return null;
			}
		}

		/// <summary>
		/// Add the passed Screen.
		/// </summary>
		/// <param name="s">Screen.</param>
		public void Add(Screen s) {
			if (this.screens.Count > 0)
			{
				CurrentScreen.Leaving ();
			}
			this.screens.Push (s);
			CurrentScreen.Entering ();
		}

		public void AddSilent(Screen s) {
			this.screens.Push (s);
		}

		/// <summary>
		/// Switch to the specified Screen, removing the previous Screen.
		/// </summary>
		/// <param name="s">Screen.</param>
		public void Switch(Screen s) {
			if (this.screens.Count > 0)
			{
				Return ();
			}
			Add (s);
		}

		/// <summary>
		/// Return from the current Screen, moving to the previous Screen.
		/// </summary>
		public void Return() {
			if (this.screens.Count > 0)
			{
				CurrentScreen.Leaving ();
				this.screens.Pop ();
			}
			if (CurrentScreen != null)
			{
				CurrentScreen.Returning ();
			}
		}

		/// <summary>
		/// Stops the ScreenManager, ending the Screen drawing sequence.
		/// </summary>
		public void Stop() {
			doQuit = true;
		}

		/// <summary>
		/// Starts the ScreenManager, drawing each screen.
		/// </summary>
		public void Start() {
			while (!SwinGame.WindowCloseRequested() && !doQuit)
			{
				SwinGame.ClearScreen (Color.White);
				SwinGame.ProcessEvents ();

				CurrentScreen.Draw ();
				CurrentScreen.Update ();

				SwinGame.RefreshScreen (60);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MyGame.ScreenManager"/> class.
		/// </summary>
		public ScreenManager ()
		{
		}
	}
}

