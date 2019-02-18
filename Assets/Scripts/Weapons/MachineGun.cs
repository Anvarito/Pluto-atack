using Projectiles;
using UnityEngine;

namespace Weapons
{
	public class MachineGun : Weapon
	{
		public Shell bullet;

		public override void Fire()
		{
		
			animator.Play("Fire");
			
			
		}
		
		
		public void shoot(){
			var newBullet = Instantiate(bullet, bulletSpawnPosition.position, Quaternion.identity);
			var LaunchVector = bulletSpawnPosition.position;
			var direction = bulletSpawnPosition.lossyScale.x < 0 ? Vector2.left : Vector2.right;

			newBullet.Launch(direction);
		}
	}
}