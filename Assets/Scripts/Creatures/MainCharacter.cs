using Controllers;
using DialogSystem;
using Extensions;
using UnityEngine;
using Weapons;

namespace Creatures
{
	public class MainCharacter : Creature
	{
		/// <summary>
		/// Позиция оружия перснажа
		/// </summary>
		public Transform weaponPosition;

		/// <summary>
		/// Текущее оружие
		/// </summary>
		public Weapon weapon;

		/// <summary>
		/// Сила прыжка персонажа
		/// </summary>
		public float jumpForce = 1f;

		/// <summary>
		/// Контроллер персонажа
		/// </summary>
		private MainCharacterController CharacterController;

		/// <summary>
		/// Состояние персонажа
		/// </summary>
		public string State { private get; set; }

		public bool inDialog { get; set; }

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

		/// <summary>
		/// Двигаться
		/// </summary>
		/// <param name="movement">Направление движения</param>
		public override void Move(Vector2 movement)
		{
			if (movement.x > 0 && !this.IsFacingRight())
				this.TurnRight();
			if (movement.x < 0 && this.IsFacingRight())
				this.TurnLeft();

			Body.velocity = new Vector2(movement.x * moveSpeed, Body.velocity.y);
		}

		/// <summary>
		/// Прыгнуть
		/// </summary>
		public void Jump()
		{
			Body.velocity = new Vector2(Body.velocity.x, 0);
			Body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}

		/// <summary>
		/// Атаковать из оружия, если оно есть. Иначе бить с руки
		/// </summary>
		public void Attack()
		{
			if (weapon != null)
				weapon.Fire();
			else
				MeleeAttack();
		}

		/// <summary>
		/// Атака в ближнем бою
		/// </summary>
		public void MeleeAttack()
		{
			print("I have no gun!!!");
		}

		protected override void Die()
		{
			transform.position = OriginalPosition;
		}

		/// <summary>
		/// Выбросить оружие, если оно есть, и подобрать новое
		/// </summary>
		/// <param name="newWeapon">Найденное новое оружие</param>
		internal void TakeWeapon(Weapon newWeapon)
		{
			ThrowWeapon();
			weapon = newWeapon;
			if (weapon.IsFacingRight() != this.IsFacingRight())
			{
				weapon.Flip();
			}

			weapon.transform.lossyScale.Set(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
			newWeapon.transform.SetParent(transform);
			weapon.transform.position = weaponPosition.transform.position;
		}

		/// <summary>
		/// Выбросить оружие, если оно есть
		/// </summary>
		private void ThrowWeapon()
		{
			if (weapon == null) return;
			weapon.transform.SetParent(transform.parent);
			weapon = null;
		}

		/// <summary>
		/// Найти оружие неподалеку
		/// </summary>
		/// <param name="newWeapon">Найденное новое оружие</param>
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