using UnityEngine;

namespace Extensions
{
	/// <summary>
	/// Класс, расширяющий MonoBehavior
	/// </summary>
	public static class MonoBehaviorExtension
	{
		/// <summary>
		/// Развернуть Transform в противоположную сторону
		/// </summary>
		public static void Flip(this MonoBehaviour monoBehaviour)
		{
			var transform = monoBehaviour.transform;
			var newScale = transform.localScale;
			newScale.x *= -1;
			transform.localScale = newScale;
		}

		/// <summary>
		/// Повернут ли Transform направо
		/// </summary>
		public static bool IsFacingRight(this MonoBehaviour monoBehaviour)
		{
			return monoBehaviour.transform.localScale.x > 0;
		}

		/// <summary>
		/// Развернуть Transform направо
		/// </summary>
		public static void TurnRight(this MonoBehaviour monoBehaviour)
		{
			if (monoBehaviour.IsFacingRight()) return;
			monoBehaviour.Flip();
		}

		/// <summary>
		/// Развернуть Transform налево
		/// </summary>
		public static void TurnLeft(this MonoBehaviour monoBehaviour)
		{
			if (!monoBehaviour.IsFacingRight()) return;
			monoBehaviour.Flip();
		}
	}
}