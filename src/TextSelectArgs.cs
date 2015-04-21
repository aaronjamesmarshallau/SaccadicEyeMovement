using System;
using SwinGameSDK;

namespace MyGame
{
	public class TextSelectArgs : EventArgs
	{
		private string selectedWord;
		private Rectangle selectionRect;
		private int selectedIndex;

		public int SelectedIndex {
			get {
				return selectedIndex;
			}
		}

		public string SelectedWord {
			get {
				return selectedWord;
			}
		}

		public Rectangle SelectedRectangle {
			get {
				return selectionRect;
			}
		}

		public TextSelectArgs (string selectedWord, Rectangle selectionRect, int selectedIndex)
		{
			this.selectedWord = selectedWord;
			this.selectionRect = selectionRect;
			this.selectedIndex = selectedIndex;
		}
	}
}

