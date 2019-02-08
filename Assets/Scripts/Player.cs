using UnityEngine;

public class Player : Creature
{
	public void Start()
	{
		base.Start();
		CurrentAnimation = "Idle";
	}

	public void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			CurrentAnimation = "jump";
		}

		Animator.Play(CurrentAnimation);
	}

	protected override void Die()
	{
		throw new System.NotImplementedException();
	}
}