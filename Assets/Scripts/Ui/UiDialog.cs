using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DialogSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class UiDialog : MonoBehaviour
	{
		private List<DialogLine> _lines;
		private Text _uiText;
		private Image _avatar;
		private Canvas _canvas;

		private int _currentLineNumber = 0;
		private int _characterIndex = 0;

		private void Start()
		{
			_canvas = GetComponent<Canvas>();
			_canvas.enabled = false;

			_uiText = _canvas.GetComponentInChildren<Text>();
			_uiText.text = string.Empty;
			_avatar = _canvas.GetComponentInChildren<Image>();
		}

		public void Start(IEnumerable<DialogLine> lines)
		{
			_lines = lines.ToList();
			_currentLineNumber = 0;
			_canvas.enabled = true;
		}

		private void Update()
		{
			if (!_canvas.enabled) return;

			var line = _lines[_currentLineNumber];
			var text = GetText(line);

			if (_characterIndex < text.Length)
			{
				_uiText.text += text[_characterIndex];
				_characterIndex++;
			}
			else
			{
				if (Input.GetKey(KeyCode.Return))
				{
					_characterIndex = 0;
					_currentLineNumber++;
					_uiText.text = "";
					if (_currentLineNumber == _lines.Count)
					{
						_canvas.enabled = false;
					}
				}
			}
		}

		private string GetText(DialogLine line)
		{
			switch (Config.Language)
			{
				case Language.Russian: return line.ru;
				case Language.English: return line.en;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}