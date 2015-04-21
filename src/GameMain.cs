using System;
using System.Reflection;
using SwinGameSDK;
using Color = System.Drawing.Color;

namespace MyGame
{
    public class GameMain
    {
        public static void Main()
        {
			ScreenManager sm = new ScreenManager ();
			ScreenManager.Load ();
			            
            //Open the game window
            SwinGame.OpenGraphicsWindow("Aaron Marshall's Research App", 800, 600);

			sm.Add (new IntroScreen(sm, new string[,] {{"COS20007", "Object-oriented Programming"},{"Research Project", "Aaron Marshall"}}));
			sm.Start ();
            
            //End the audio
            SwinGame.CloseAudio();
             
            //Close any resources we were using
            SwinGame.ReleaseAllResources();
        }
    }
}