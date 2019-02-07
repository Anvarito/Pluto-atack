using UnityEngine;

public class Creature : MonoBehaviour
{
	public int Health { get; set; }

	public float MoveSpeed { get; set; }
	public CreatureState State { get; set; }

	public bool needDrawLine = true;

	public CollisionDetector collisionDetector;

	protected Rigidbody2D _body;
	protected Animator _animation;
	protected SpriteRenderer _spriteRenderer;
	protected bool isFacingRight = true;

	public void TakeDamage(int damage)
	{
		Health -= damage;
		if (Health > 0) return;
		{
			State = CreatureState.Dead;
		}
	}

	public void Start()
	{
		_body = GetComponent<Rigidbody2D>();
		_animation = GetComponent<Animator>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Flip(bool right)
	{
		// @TODO реализовать логику разворота
	}
}