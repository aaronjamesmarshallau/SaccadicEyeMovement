using System;

namespace MyGame
{
	public abstract class Screen
	{
		ScreenManager sm;

		/// <summary>
		/// Gets the ScreenManager
		/// </summary>
		/// <value>The ScreenManager.</value>
		public ScreenManager SM {
			get {
				return sm;
			}
		}

		public abstract void Draw();

		public abstract void Update();

		public abstract void Entering ();

		public abstract void Leaving();

		public abstract void Returning ();

		/// <summary>
		/// Default constructor for any Screen
		/// </summary>
		/// <param name="sm">ScreenManager.</param>
		public Screen (ScreenManager sm)
		{
			this.sm = sm;
		}
	}
}

