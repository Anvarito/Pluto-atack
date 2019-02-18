using System;
using Creatures;
using UnityEngine;

namespace Controllers
{
	public class MainCharacterController
	{
		private readonly MainCharacter _character;

		public MainCharacterController(MainCharacter character)
		{
			_character = character;
		}

		public void Update()
		{
			if (Input.GetKey(KeyCode.F))
			{
				_character.Attack();
			}

			var movement = new Vector2();
			if (Input.GetKey(KeyCode.A))
			{
				movement.x = -1f;
			}

			if (Input.GetKey(KeyCode.D))
			{
				movement.x = 1f;
			}

			_character.Move(movement);
			_character.State = Math.Abs(movement.x) > 0 ? "Running" : "Idle";

			if (Input.GetButton("Jump") && _character.collisionDetector.IsOnGroundCollision())
			{
				_character.Jump();
				_character.State = "Jumping";
			}
		}
	}
}