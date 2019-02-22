using Controllers;
using Extensions;
using UnityEngine;
using Weapons;

namespace Creatures
{
	public class MainCharacter : Creature
	{
		public Transform weaponPosition;
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
			if (movement.x > 0 && !this.IsFacingRight())
				this.TurnRight();
			if (movement.x < 0 && this.IsFacingRight())
				this.TurnLeft();

			Body.velocity = new Vector2(movement.x * moveSpeed, Body.velocity.y);
		}

		public void Jump()
		{
			Body.velocity = new Vector2(Body.velocity.x, 0);
			Body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}

		public void Attack()
		{
			if (weapon != null)
				weapon.Fire();
			else
				MeleeAttack();
		}

		public void MeleeAttack()
		{
			print("I have no gun!!!");
		}

		protected override void Die()
		{
			transform.position = OriginalPosition;
		}

		internal void TakeWeapon(Weapon newWeapon)
		{
			ThrowWeapon();

			weapon = newWeapon;

			// @TODO: Разворачивать оружие при подбирании, если оно смотрит в другую сторону
			weapon.transform.lossyScale.Set(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
			newWeapon.transform.SetParent(transform);
			weapon.transform.position = weaponPosition.transform.position;
		}

		public void ThrowWeapon()
		{
			if (weapon == null) return;
			weapon.transform.SetParent(transform.parent);
			weapon = null;
		}

		private void FindWeapon(Weapon newWeapon)
		{
			CharacterController.WeaponNearBy = newWeapon;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			switch (other.gameObject.tag)
			{
				case "Weapon":
				{
					FindWeapon(other.gameObject.GetComponent<Weapon>());
					break;
				}
			}
		}
	}
}