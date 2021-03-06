﻿using ProjectileHitEffects;
using UnityEngine;

namespace Projectiles
{
	/// <inheritdoc />
	/// <summary>
	/// Снаряд оружия
	/// </summary>
	[RequireComponent(typeof(Rigidbody2D))]
	public abstract class Shell : MonoBehaviour
	{
		public float speed = 1;
		public int damage = 1;
		public float mass;
		public float gravityScale;
		public ProjectileHitEffect hitEffect;
		private Rigidbody2D body;

		protected void Start()
		{
			hitEffect = gameObject.AddComponent<DefaultHitEffect>();
			body = GetComponent<Rigidbody2D>();
			body.mass = mass;
			body.gravityScale = gravityScale;
		}

		public void Launch(Vector2 launchVector, Vector2 playerVelocity)
		{
			body = GetComponent<Rigidbody2D>();
			body.AddForce(launchVector * speed + playerVelocity, ForceMode2D.Impulse);
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag(tag)) return;

			hitEffect.Initiate(collision.gameObject, this);
			Destroy(gameObject);
		}

		private void OnBecameInvisible()
		{
			Destroy(gameObject);
		}
	}
}