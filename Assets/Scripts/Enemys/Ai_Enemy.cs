using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MoveEnemy))]
public class Ai_Enemy : MonoBehaviour
{

    [System.NonSerialized]
    public bool moveAllow = false;//позволено двигаться
    [System.NonSerialized]
    public bool shootAllow = false;//позволено стрелять
    [System.NonSerialized]
    public bool meleeAllow = false;//позволен ближний бой

    Rigidbody2D _rigidbody;
    MoveEnemy MOVE;
    ShootingEnemy SHOOT;
    MeleeEnemy MELEE;
    Death DEATH;
    public float HP = 3;
    public float mass = 50;
    public GameObject bulletHitEff;


    [System.NonSerialized]
    public Vector2 DirectionToPlayer;
    [System.NonSerialized]
    public float DistanceToPlayer;
    [System.NonSerialized]
    public float Dot;

    public float numberDistanceForMove = 7; //это расстояние для начала реагирования на игрока, вне этого диапазона АИ не будет ничего делать
    public float numberDistanceForShoot = 3;//расстояние для стрельбы
    public float numberDistanceForMelee = 1;//расстояние для милишной атаки


    void Start()
    {
        AImanager.AIlist.Add(this);//this или getComponnent? передаём ссылку на экземпляр в менеджер
        gameObject.name = "Monster " + Random.Range(0, 100);



        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.mass = mass;

        MOVE = GetComponent<MoveEnemy>();
        SHOOT = GetComponent<ShootingEnemy>();
        MELEE = GetComponent<MeleeEnemy>();
        DEATH = GetComponent<Death>();
        //player = GameObject.Find("Player");
        //_animation = GetComponent<Animator>();
        //_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //print("run " + moveAllow + " shoot " + shootAllow + " mele " + meleeAllow);
        if (HP <= 0)
        {
            DEATH.Die();
        }
        else
        {

            if (moveAllow && !shootAllow && !meleeAllow)
            {
                try
                {
                  //   print(gameObject.name + "i run");
                    MOVE.Turn(Dot);
                    MOVE.Run(DirectionToPlayer);
                }
                catch
                {
                    print(gameObject.name + ": i cant move");
                }
            }
            else if (shootAllow && !meleeAllow && !moveAllow)
            {
                //  print("i shoot");
                try
                {
                    MOVE.Turn(Dot);
                    SHOOT.Shoot(DirectionToPlayer);
                }
                catch
                {
                    // print(gameObject.name + ": i cant shoot");
                    MOVE.Run(DirectionToPlayer);
                }
            }
            else if (meleeAllow)
            {
                MOVE.Turn(Dot);
                try
                {

                    //  print(gameObject.name + "i melee");
                    MELEE.Atack();
                }
                catch
                {
                    // print(gameObject.name + ": i cant melee");
                    MOVE.Stay();
                }
            }
            else
            {
                try
                {
                    // print(gameObject.name + "i stay");

                    MOVE.Stay();
                }
                catch
                {
                    // print(gameObject.name + ": i cant move");
                }
            }
        }
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // print("hit");
        if (collision.transform.tag == "bullet")
        {
            ContactPoint2D contact = collision.contacts[0];
            Vector3 pos = contact.point;
            HP -= collision.gameObject.GetComponent<Bullet>().Damage;
            Instantiate(bulletHitEff, pos, Quaternion.identity);
        }
    }

}
