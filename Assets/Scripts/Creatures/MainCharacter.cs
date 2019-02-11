using System;
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

		public new void Start()
		{
			base.Start();
			CharacterController = new MainCharacterController(this);
		}

		public void Update()
		{
			CharacterController.Update();
		}

		public override void Move(Vector2 movement)
		{
			if (movement.x > 0 && !isFacingRight)
				TurnRight();
			if (movement.x < 0 && isFacingRight)
				TurnLeft();
			Body.AddForce(movement * moveSpeed);
		}

		public void Jump()
		{
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