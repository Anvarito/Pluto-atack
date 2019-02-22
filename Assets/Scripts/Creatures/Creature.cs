using UnityEngine;
using Utils;

namespace Creatures
{
	public abstract class Creature : MonoBehaviour
	{
		public int health;
		public float moveSpeed;
		public CollisionDetector collisionDetector;

		internal Rigidbody2D Body;
		internal Animator Animator;
		protected SpriteRenderer SpriteRenderer;
		protected string CurrentAnimation;
		protected Vector2 OriginalPosition;

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
			Body.velocity.Set(0, 0);

			Animator = GetComponent<Animator>();
			SpriteRenderer = GetComponent<SpriteRenderer>();
			OriginalPosition = transform.position;
		}
	}
}