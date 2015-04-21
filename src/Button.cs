using System;
using SwinGameSDK;
using Color = System.Drawing.Color;

namespace MyGame
{
	public class Button
	{
		public delegate void ButtonClickHandler(object sender, ButtonClickArgs e);

		public event ButtonClickHandler ButtonClick;

		private string _name;
		private string _text;
		private int _y;
		private int _x;
		private int _width;
		private int _height;
		private bool _autosize;
		private Font _font;
		private bool _selected;
		private Color _foreColor;
		private Color _backColor;
		private bool _centered;
		private bool _enabled = true;

		public bool Enabled {
			get {
				return _enabled;
			}
			set {
				_enabled = value;
			}
		}

		public bool Centered {
			get {
				return _centered;
			}
			set {
				_centered = value;
			}
		}

		public bool Selected {
			get {
				return _selected;
			}
		}

		public Color ForeColor {
			get {
				return _foreColor;
			}
			set {
				_foreColor = value;
			}
		}

		public Color BackColor {
			get {
				return _backColor;
			}
			set {
				_backColor = value;
			}
		}

		public string Name {
			get {
				return _name;
			}
			set {
				_name = value;
			}
		}

		public string Text {
			get {
				return _text;
			}
			set {
				_text = value;
			}
		}

		public int Y {
			get {
				return _y;
			}
			set {
				_y = value;
				Console.WriteLine (value);
			}
		}

		public int X {
			get {
				return _x;
			}
			set {
				_x = value;
			}
		}

		public int Width {
			get {
				return _width;
			}
			set {
				if (!_autosize)
				{
					_width = value;
				}
			}
		}

		public int Height {
			get {
				return _height;
			}
			set {
				if (!_autosize)
				{
					_height = value;
				}
			}
		}

		public Font Font {
			get {
				return _font;
			}
			set {
				_font = value;
			}
		}

		public bool AutoSize {
			get {
				return _autosize;
			}
			set {
				_autosize = value;
				if (_autosize)
				{
					_width = SwinGame.TextWidth (_font, _text) + 20;
					_height = SwinGame.TextHeight (_font, _text) + 10;
				}
			}
		}

		public void Draw ()	{
			Color drawBackColor;
			Color drawForeColor;
			if (_selected)
			{
				drawForeColor = _backColor;
				drawBackColor = _foreColor;
			}
			else
			{
				drawForeColor = _foreColor;
				drawBackColor = _backColor;
			}

			if (!_enabled)
			{
				drawForeColor = Color.White;
				drawBackColor = Color.LightGray;
			}

			SwinGame.FillRectangle (drawBackColor, _x, _y, _width, _height);
			for (int i = 0; i < 3; i++)
			{
				SwinGame.DrawRectangle (drawForeColor, _x - i, _y - i, _width + i * 2, _height + i * 2);
			}

			if (_centered)
			{
				SwinGame.DrawText (_text, drawForeColor, _font, _x + (_width / 2 - SwinGame.TextWidth(_font, _text) / 2), _y + 5);
			}
			else
			{
				SwinGame.DrawText (_text, drawForeColor, _font, _x + 10, _y + 5);
			}
			//Console.WriteLine("Drawing button at: ({0}, {1})", _x, _y);
		}

		public void Update() {
			if (_enabled)
			{
				if (SwinGame.KeyTyped (KeyCode.vk_RETURN))
				{
					if (ButtonClick != null)
					{
						ButtonClick (this, new ButtonClickArgs (this));
					}
				}

				if (_autosize)
				{
					_width = SwinGame.TextWidth (_font, _text) + 20;
					_height = SwinGame.TextHeight (_font, _text) + 10;
				}

				if (SwinGame.PointInRect (SwinGame.MousePosition (), _x, _y, _width, _height))
				{
					_selected = true;
					if (SwinGame.MouseClicked (MouseButton.LeftButton))
					{
						if (ButtonClick != null)
						{
							ButtonClick (this, new ButtonClickArgs (this));
						}
					}
				}
				else
				{
					_selected = false;
				}
			}
		}

		public Button (string name, string text, int x, int y, bool autosize) {
			_name = name;
			_text = text;
			_x = x;
			_y = y;
			_font = SwinGame.LoadFont ("maven_pro_regular.ttf", 18);
			AutoSize = autosize;
			_centered = true;
			_backColor = Color.OrangeRed;
			_foreColor = Color.White;
		}

		public Button(string name, string text, int x, int y, int width, int height) : this(name, text, x, y, false) {
			_width = width;
			_height = height;
		}

		public Button (string name, string text, int x, int y) : this (name, text, x, y, true) {

		}
	}



}

