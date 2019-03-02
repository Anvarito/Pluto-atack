using System;
using System.Collections.Generic;
using System.Linq;
using Creatures;
using UnityEngine;
using Weapons;

namespace Controllers
{
	public class MainCharacterController
	{
		private readonly MainCharacter _character;
		public Weapon WeaponNearBy;

		private readonly IDictionary<KeyCode, Action> _handlers;

		/// <summary>
		/// Контроллер управления персонажем
		/// </summary>
		/// <param name="character">Персонаж</param>
		public MainCharacterController(MainCharacter character)
		{
			_character = character;
			_handlers = new Dictionary<KeyCode, Action>
			{
				{KeyCode.F, _character.Attack},
				{KeyCode.R, TakeWeaponButtonPressed},
				{KeyCode.Space, JumpButtonPressed},
				{KeyCode.A, MoveLeftButtonPressed},
				{KeyCode.D, MoveRightButtonPressed}
			};
		}

		public void Update()
		{
			Reset();

			_handlers.Keys.ToList().ForEach(key =>
			{
				if (Input.GetKey(key))
					_handlers[key]();
			});
		}

		private void TakeWeaponButtonPressed()
		{
			if (WeaponNearBy == null) return;
			_character.TakeWeapon(WeaponNearBy);
		}

		private void JumpButtonPressed()
		{
			if (!_character.collisionDetector.IsOnGroundCollision()) return;
			_character.Jump();
			_character.State = "Jumping";
		}

		private void MoveLeftButtonPressed()
		{
			if (_character.collisionDetector.IsFrontCollision(Vector2.left))
			{
				_character.State = "Idle";
				return;
			}

			_character.Move(Vector2.left);
			_character.State = "Running";
		}

		private void MoveRightButtonPressed()
		{
			if (_character.collisionDetector.IsFrontCollision(Vector2.right)) return;
			_character.Move(Vector2.right);
			_character.State = "Running";
		}

		private void Reset()
		{
			_character.Move(new Vector2());
			_character.State = "Idle";
		}
	}
}