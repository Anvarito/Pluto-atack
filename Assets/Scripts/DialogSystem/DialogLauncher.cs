using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace DialogSystem
{
	public static class DialogLauncher
	{
		private static UiDialog _dialog;

		

		static DialogLauncher()
		{
			
		}

		public static void StartNewDialog(string path)
		{
			var lines = GetLines();

			if (_dialog == null)
			{
				_dialog = GameObject.Find("Ui").GetComponentInChildren<UiDialog>();
			}

			_dialog.Start(lines);
		}

		private static List<string> GetLines()
		{
			return new List<string>()
			{
				"Ебать какая пиздатая пушка",
				"Ща начну во всех шмалять",
				"Пиздец, держите меня семеро"
			};
		}
	}
}