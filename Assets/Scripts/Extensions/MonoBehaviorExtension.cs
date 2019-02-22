using UnityEngine;

namespace Extensions
{
	public static class MonoBehaviorExtension
	{
		private static void Flip(this MonoBehaviour monoBehaviour)
		{
			var transform = monoBehaviour.transform;
			var newScale = transform.localScale;
			newScale.x *= -1;
			transform.localScale = newScale;
		}

		public static bool IsFacingRight(this MonoBehaviour monoBehaviour)
		{
			return monoBehaviour.transform.localScale.x > 0;
		}

		public static void TurnRight(this MonoBehaviour monoBehaviour)
		{
			if (monoBehaviour.IsFacingRight()) return;
			monoBehaviour.Flip();
		}

		public static void TurnLeft(this MonoBehaviour monoBehaviour)
		{
			if (!monoBehaviour.IsFacingRight()) return;
			monoBehaviour.Flip();
		}
	}
}