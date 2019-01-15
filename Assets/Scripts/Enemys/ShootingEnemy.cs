using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ai_Enemy))]
[RequireComponent(typeof(Animator))]
public class ShootingEnemy : MonoBehaviour
{

   // Ai_Enemy _Ai;
    public Transform pointForShoot;
    public Rigidbody2D Bullet;
    private Vector2 Dir;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        //_Ai = GetComponent<Ai_Enemy>();
    }

    void Update()
    {
        //if (_Ai._PlayerInZoneAtack) // пока игрок в зоне атаки
        //    Shot();
    }

    //void Shot()
    //{
    //    _Ai.Shoot(Bullet,pointForShoot);
    //}

    public void Shoot(Vector2 DirectionToPlayer)
    {
        Dir = DirectionToPlayer;
        animator.Play("Shoot");
      
    }//стрельба
    void BulletShoot()
    {
        Rigidbody2D buletObj = Instantiate(Bullet, pointForShoot.transform.position, Quaternion.identity) as Rigidbody2D;
        buletObj.AddForce(Dir * buletObj.GetComponent<Bullet>().bullet_speed, ForceMode2D.Impulse);
    }
}
