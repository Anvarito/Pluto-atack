using Controllers;
using UnityEngine;
using Weapons;

namespace Creatures
{
	public class MainCharacter : Creature
	{
		public Weapon weapon;
		public float jumpForce = 1f;

		private MainCharacterController CharacterController;

		public string State { private get; set; }

		public new void Start()
		{
			base.Start();
			CharacterController = new MainCharacterController(this);
		}

		public void Update()
		{
			CharacterController.Update();
			Animator.Play(State);
		}

		public override void Move(Vector2 movement)
		{
			if (movement.x > 0 && !isFacingRight)
				TurnRight();
			if (movement.x < 0 && isFacingRight)
				TurnLeft();

			Body.velocity = new Vector2(movement.x * moveSpeed, Body.velocity.y);
		}

		public void Jump()
		{
			Body.velocity = new Vector2(Body.velocity.x, 0);
			Body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}

		public void Attack()
		{
			weapon.Fire();
		}

		protected override void Die()
		{
			transform.position = OriginalPosition;
		}
	}
}