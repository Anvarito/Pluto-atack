using UnityEngine;
using Utils;

namespace Creatures
{
	public abstract class Creature : MonoBehaviour
	{
		public int health;
		public float moveSpeed;
		public CollisionDetector collisionDetector;

		protected Rigidbody2D Body;
		public Animator Animator;
		protected SpriteRenderer SpriteRenderer;
		protected string CurrentAnimation;
		protected Vector2 OriginalPosition;

		protected bool isFacingRight = true;

		public void TakeDamage(int damage)
		{
			health -= damage;
			if (health <= 0) Die();
		}

		protected abstract void Die();

		public abstract void Move(Vector2 movement);

		public void Start()
		{
			Body = GetComponent<Rigidbody2D>();
			Animator = GetComponent<Animator>();
			SpriteRenderer = GetComponent<SpriteRenderer>();
			OriginalPosition = transform.position;
		}

		private void Flip()
		{
			isFacingRight = !isFacingRight;
			var newScale = gameObject.transform.localScale;
			newScale.x *= -1;
			gameObject.transform.localScale = newScale;
		}

		protected void TurnRight()
		{
			if (isFacingRight) return;
			Flip();
		}

		protected void TurnLeft()
		{
			if (!isFacingRight) return;
			Flip();
		}
	}
}