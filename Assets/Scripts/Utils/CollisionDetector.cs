using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
	public class CollisionDetector : MonoBehaviour
	{
		public Transform frontTopTrigger;
		public Transform frontMiddleTrigger;
		public Transform frontBottomTrigger;
		public Transform backTopTrigger;
		public Transform backMiddleTrigger;
		public Transform backBottomTrigger;

		private const float minCollisionDistance = .1f;

		private void Start()
		{
			SetTriggersVisibility(Config.IsDebugMode);
		}

		/// <summary>
		/// Делает видимым или не видимым триггеры
		/// </summary>
		private void SetTriggersVisibility(bool visible)
		{
			new List<Transform>()
			{
				frontTopTrigger, frontBottomTrigger, frontMiddleTrigger, backTopTrigger, backBottomTrigger,
				backMiddleTrigger
			}.ForEach(t => t.gameObject.GetComponent<SpriteRenderer>().enabled = visible);
		}

		/// <summary>
		/// Стоит ли объект на земле
		/// </summary>
		public bool IsOnGroundCollision()
		{
			var triggers = new List<Transform>() {frontBottomTrigger, backBottomTrigger};
			return CheckCollision(triggers, Vector2.down);
		}

		/// <summary>
		/// Проверка столкновения с объектом спереди
		/// </summary>
		public bool IsFrontCollision(Vector2 dir)
		{
			var triggers = new List<Transform>() {frontTopTrigger, frontBottomTrigger, frontMiddleTrigger};
			
			//var direction = dir;
			return CheckCollision(triggers, dir);
		}

		/// <summary>
		/// Проверка столкновения с объектом сзади
		/// </summary>
		public bool IsBackCollision()
		{
			var triggers = new List<Transform>() {backTopTrigger, backBottomTrigger, backMiddleTrigger};
			var direction = Vector2.left;
			return CheckCollision(triggers, direction);
		}

		/// <summary>
		/// Проверка столкновения с объектом сверху
		/// </summary>
		public bool IsTopCollision()
		{
			var triggers = new List<Transform>() {frontTopTrigger, backTopTrigger};
			return CheckCollision(triggers, Vector2.up);
		}

		/// <summary>
		/// Проверить столкновение каждого триггера по заданному направлению
		/// </summary>
		/// <param name="triggers">Коллекция триггеров</param>
		/// <param name="direction">Направление проверки</param>
		/// <returns>Сталкивается ли хотя бы один триггер</returns>
		private static bool CheckCollision(IEnumerable<Transform> triggers, Vector2 direction)
		{
			
			return triggers.Select(trigger => Physics2D.Raycast(trigger.position, direction, minCollisionDistance))
				.Any(hit => hit.collider != null);
		}
	}
}