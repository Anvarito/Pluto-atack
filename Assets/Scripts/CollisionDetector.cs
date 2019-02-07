using System.Collections.Generic;
using System.Security.Authentication;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
	public Transform frontTopTrigger;
	public Transform frontMiddleTrigger;
	public Transform frontBottomTrigger;
	public Transform backTopTrigger;
	public Transform backMiddleTrigger;
	public Transform backBottomTrigger;

	private const float minCollisionDistance = .1f;

	/// <summary>
	/// Стоит ли объект на земле
	/// </summary>
	public bool IsOnGroundCollision()
	{
		var triggers = new List<Transform>() {frontBottomTrigger, backBottomTrigger};
		return CheckCollision(triggers, Vector2.down);
	}

	public bool IsFrontCollision(bool isFacingRight)
	{
		var triggers = new List<Transform>() {frontTopTrigger, frontBottomTrigger, frontMiddleTrigger};
		var direction = isFacingRight ? Vector2.right : Vector2.left;
		return CheckCollision(triggers, direction);
	}

	public bool IsBackCollision(bool isFacingRight)
	{
		var triggers = new List<Transform>() {backTopTrigger, backBottomTrigger, backMiddleTrigger};
		var direction = isFacingRight ? Vector2.left : Vector2.right;
		return CheckCollision(triggers, direction);
	}

	public bool IsTopCollision()
	{
		var triggers = new List<Transform>() {frontTopTrigger, backTopTrigger};
		return CheckCollision(triggers, Vector2.down);
	}

	private bool CheckCollision(IEnumerable<Transform> triggers, Vector2 direction)
	{
		foreach (var trigger in triggers)
		{
			var hit = Physics2D.Raycast(trigger.position, direction, minCollisionDistance);
			if (hit.collider != null)
			{
				return true;
			}
		}

		return false;
	}
}