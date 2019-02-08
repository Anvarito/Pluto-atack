using UnityEngine;

public class Player : Creature
{

	private Weapon weapon;
	public void Start()
	{
		base.Start();
		CurrentAnimation = "Idle";
	}

	public void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			CurrentAnimation = "Jump";
		}

		Animator.Play(CurrentAnimation);
		
	}

	public void Attack()
	{
		weapon.Fire();
	}

	protected override void Die()
	{
		print("Die");
	}
}