using Projectiles;

namespace Weapons
{
	public class RailGun : Weapon
	{
		public Shell laser;

		public override void Shoot()
		{
			print("pew pew pew");
		}
	}
}