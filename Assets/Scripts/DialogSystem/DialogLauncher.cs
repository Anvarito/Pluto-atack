using System.Collections.Generic;
using Ui;
using UnityEngine;
using Utils;

namespace DialogSystem
{
	public static class DialogLauncher
	{
		private static UiDialog _dialog;

		public static void StartNewDialog(string path)
		{
			var lines = JsonReader.ReadDialogFromFile(path);

			if (_dialog == null)
			{
				_dialog = GameObject.Find("Ui").GetComponentInChildren<UiDialog>();
			}

			_dialog.Start(lines);
		}
	}
}