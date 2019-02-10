using UnityEngine;

namespace Weapons
{
	public abstract class Weapon : MonoBehaviour
	{
		protected Animator animator;

		public Transform bulletSpawnPosition;

		// Start is called before the first frame update
		public void Start()
		{
			animator = GetComponent<Animator>();
		}

		public abstract void Fire();
	}
}