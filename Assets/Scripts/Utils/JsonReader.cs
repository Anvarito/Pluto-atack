using System;
using System.Collections.Generic;
using System.IO;
using DialogSystem;
using UnityEngine;

namespace Utils
{
	/// <summary>
	/// Класс, служащий для считывания диалогов из Json файлов.
	/// </summary>
	public static class JsonReader
	{
		/// <summary>
		/// Считать из файла диалоги из нужной главы
		/// </summary>
		/// <param name="number">Номер главы</param>
		/// <returns>Коллекция диалогов</returns>
		public static IEnumerable<string> ReadFromChapter(int number)
		{
			var path = $"Assets//Dialogs/Chapter{number}.json";
			var json = new StreamReader(path).ReadToEnd();
			var result = JsonUtility.FromJson<string>(json);
			Debug.Log(result);
			return null;
		}
	}
}