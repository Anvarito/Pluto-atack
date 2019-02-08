using System;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
	public int Health;
	public float MoveSpeed { get; set; }
	public CollisionDetector collisionDetector;

	protected Rigidbody2D Body;
	protected Animator Animator;
	protected SpriteRenderer SpriteRenderer;
	protected string CurrentAnimation;
	protected Vector2 OriginalPosition;

	protected bool isFacingRight = true;

	protected bool inAir => !collisionDetector.IsOnGroundCollision();

	public void TakeDamage(int damage)
	{
		Health -= damage;
		if (Health <= 0) Die();
	}

	protected abstract void Die();

	public void Start()
	{
		Body = GetComponent<Rigidbody2D>();
		Animator = GetComponent<Animator>();
		SpriteRenderer = GetComponent<SpriteRenderer>();
		OriginalPosition = transform.position;
	}

	public void Flip(bool right)
	{
		var newScale = gameObject.transform.localScale;
		newScale.x *= -1;
		gameObject.transform.localScale = newScale;
	}

	public void TurnRight()
	{
	}

	public void TurnLeft()
	{
	}
}