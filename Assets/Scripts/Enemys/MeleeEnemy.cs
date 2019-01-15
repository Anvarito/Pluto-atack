using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{

    private Vector2 Dir;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Atack(Vector2 DirectionToPlayer)
    {
        Dir = DirectionToPlayer;
        animator.Play("Shoot");
    }
}
