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
			if (Input.GetButton("Jump"))
			{
				if (_character.collisionDetector.IsOnGroundCollision())
				{
					_character.Jump();
				}
			}

			if (Input.GetKey(KeyCode.F))
			{
				_character.Attack();
			}

			var moveHorizontal = Input.GetAxis("Horizontal");
			var movement = new Vector2(moveHorizontal, 0);
			_character.Move(movement);
		}
	}
}