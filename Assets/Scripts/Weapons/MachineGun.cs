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
			var newShell = Instantiate(bullet, bulletSpawnPosition.position, Quaternion.identity);
//			newShell.Launch();
		}
	}
}