using UnityEngine;

namespace Weapons
{
	public abstract class Weapon : MonoBehaviour
	{
		private Animator _animator;

		public Transform bulletSpawnPosition;
		private Collider2D _collider;

		// Start is called before the first frame update
		public void Start()
		{
			tag = "Weapon";
			gameObject.layer = 2;
			_animator = GetComponent<Animator>();

			_collider = gameObject.AddComponent<CapsuleCollider2D>();
			_collider.isTrigger = true;
		}

		public void Fire()
		{
			_animator.Play("Fire");
		}

		public abstract void Shoot();
	}
}