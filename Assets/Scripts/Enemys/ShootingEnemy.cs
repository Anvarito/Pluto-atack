using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ai_Enemy))]
public class ShootingEnemy : MonoBehaviour
{

    Ai_Enemy _Ai;
    public Transform pointForShoot;
    public Rigidbody2D Bullet;


    void Start()
    {
        _Ai = GetComponent<Ai_Enemy>();
    }

    void Update()
    {
        if (_Ai._PlayerInZoneAtack) // пока игрок в зоне атаки
            Shot();
    }

    void Shot()
    {
        _Ai.Shoot(Bullet,pointForShoot);
    }
}
