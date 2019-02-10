using UnityEngine;
using Weapons;

namespace Creatures
{
	public class MainCharacter : Creature
	{
		public Weapon weapon;
		public float jumpForce = 1f;

		public new void Start()
		{
			base.Start();
		}

		public void Update()
		{
			if (Input.GetKey(KeyCode.F))
			{
				Attack();
			}

			if (Input.GetButton("Jump"))
			{
				if (collisionDetector.IsOnGroundCollision()) Jump();
			}
		}

		private void Jump()
		{
			Body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}

		private void Attack()
		{
			weapon.Fire();
		}

		protected override void Die()
		{
			transform.position = OriginalPosition;
		}
	}
}