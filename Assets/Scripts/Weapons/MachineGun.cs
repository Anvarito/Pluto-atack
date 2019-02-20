using System;
using Creatures;
using Projectiles;
using UnityEngine;

namespace Weapons
{
	public class MachineGun : Weapon
	{
		public Shell bullet;

		/// <summary>
		/// Метод вызывается анимацией Fire
		/// </summary>
		public override void Shoot()
		{
			var newBullet = Instantiate(bullet, bulletSpawnPosition.position, Quaternion.identity);
			var direction = bulletSpawnPosition.lossyScale.x < 0 ? Vector2.left : Vector2.right;
			var playerVelocity = new Vector2(gameObject.GetComponentInParent<Creature>().Body.velocity.x, 0);

			newBullet.Launch(direction, playerVelocity);
		}
	}
}