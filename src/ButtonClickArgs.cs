using System;

namespace MyGame
{
	public class ButtonClickArgs : EventArgs
	{
		private Button _btn;

		public string Text {
			get {
				return _btn.Text;
			}
		}

		public string Name {
			get {
				return _btn.Name;
			}
		}

		public Button Button {
			get {
				return _btn;
			}
		}

		public int ButtonX {
			get {
				return _btn.X;
			}
		}

		public int ButtonY {
			get {
				return _btn.Y;
			}
		}

		public ButtonClickArgs (Button btn)
		{
			_btn = btn;
		}
	}
}

