using Projectiles;
using UnityEngine;

namespace Weapons
{
	public class MachineGun : Weapon
	{
		public Shell bullet;

		void Update()
		{
		}

		public override void Fire()
		{
			Instantiate(bullet, bulletSpawnPosition.position, Quaternion.identity);
		}
	}
}