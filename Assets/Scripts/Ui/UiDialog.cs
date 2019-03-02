using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class UiDialog : MonoBehaviour
	{
		private List<string> _lines;
		private Text _uiText;
		private Image _avatar;
		private Canvas _canvas;

		private int _lineNumber = 0;
		private int _characterIndex = 0;

		private void Start()
		{
			_canvas = GetComponent<Canvas>();
			_canvas.enabled = false;

			_uiText = _canvas.GetComponentInChildren<Text>();
			_uiText.text = string.Empty;
			_avatar = _canvas.GetComponentInChildren<Image>();
		}

		public void Start(List<string> lines)
		{
			_lines = lines;
			_lineNumber = 0;
			_canvas.enabled = true;
		}

		private void Update()
		{
			if (!_canvas.enabled) return;

			if (_characterIndex < _lines[_lineNumber].Length)
			{
				_uiText.text += _lines[_lineNumber][_characterIndex];
				_characterIndex++;
			}
			else
			{
				if (Input.GetKey(KeyCode.Return))
				{
					_characterIndex = 0;
					_lineNumber++;
					_uiText.text = "";
					if (_lineNumber == _lines.Count)
					{
						_canvas.enabled = false;
					}
				}
			}
		}
	}
}