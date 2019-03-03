using System.Collections.Generic;
using System.IO;
using DialogSystem;

namespace Utils
{
	/// <summary>
	/// Класс, служащий для считывания диалогов из Json файлов.
	/// </summary>
	public static class JsonReader
	{
		/// <summary>
		/// Считать диалог из файла
		/// </summary>
		/// <param name="path">Путь к файлу диалога</param>
		/// <returns>Коллекция строк диалога</returns>
		public static IEnumerable<DialogLine> ReadDialogFromFile(string path)
		{
			var reader = new StreamReader(path);
			var json = reader.ReadToEnd();
			return JsonHelper.FromJson<DialogLine>(json);
		}
	}
}