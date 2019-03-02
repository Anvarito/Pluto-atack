using Creatures;
using DialogSystem;
using UnityEngine;

namespace Utils
{
	public class DialogTrigger : MonoBehaviour
	{
		/// <summary>
		/// Путь к файлу диалога
		/// </summary>
		public string dialogPath;

		/// <summary>
		/// Радиус коллайдера
		/// </summary>
		public float radius = .1f;

		private CircleCollider2D collider2d;

		private void Start()
		{
			collider2d = gameObject.AddComponent<CircleCollider2D>();
			collider2d.isTrigger = true;
			collider2d.radius = radius;

			GetComponent<SpriteRenderer>().enabled = Config.IsDebugMode;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			var character = other.GetComponentInParent<MainCharacter>();
			if (character != null)
			{
				DialogLauncher.StartNewDialog(dialogPath);
			}
		}
	}
}