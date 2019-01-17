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
    public float HP = 3;
    public float mass = 50;
    public SpriteRenderer deathEff;

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
        //player = GameObject.Find("Player");
        //_animation = GetComponent<Animator>();
        //_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //print("run " + moveAllow + " shoot " + shootAllow + " mele " + meleeAllow);
        if (HP <= 0)
        {
            AImanager.AIlist.Remove(this);
            death();
        }

        if (moveAllow && !shootAllow && !meleeAllow)
        {
            try
            {
              //  print(gameObject.name + "i run");
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

    void death()
    {
        Instantiate(deathEff, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // print("hit");
        if (collision.transform.tag == "bullet")
        {
            HP -= collision.gameObject.GetComponent<Bullet>().Damage;
        }
    }


    //public Transform groundTrigger;
    //public Transform groundTriggerSecond;
    //public Transform forwardTrigger;

    //public bool needDrawLine = true;
    //public float GroundCollisionDistance = 6f;
    //public float FrontCollisionDistance = 2f;


    //public float speed = 3;
    //public float timer = 0;
    //public bool timerstarted = false;

    //[System.NonSerialized]
    //public GameObject player;
    //[System.NonSerialized]
    //public RaycastHit2D poitForRay;
    //[System.NonSerialized]
    //public bool CollisionToPlayer = false;
    //[System.NonSerialized]
    //public bool isEnemyRight;

    //Animator _animation;
    //Rigidbody2D _rigidbody;
    //Quaternion _directionEnemy;

    //Rigidbody2D bullet;// для анимации стрельбы
    //Transform point;//для анимации стрельбы

    //private float JumpTime = 0.5f;

    //private const float _rightRotation = 0; //направление вправо и одновременно начальное направление
    //private const float _leftRotation = 180;//направление влево


    //public bool _PlayerInZoneVisible { get; private set; } = false;//игрок в зоне видимости или нет
    //public bool _PlayerInZoneAtack { get; private set; } = false;//игрок в зоне видимости или нет



    //float position_x_FrontgroundTriger
    //{
    //    get { return groundTrigger.position.x; }
    //} //икс и игрик позиция первого тригера для обнаружения препятствий
    //float position_y_FrontgroundTriger
    //{
    //    get { return groundTrigger.position.y; }
    //}

    //float position_x_BackGroundTriger
    //{
    //    get { return groundTriggerSecond.position.x; }
    //} //икс и игрик позиция второго тригера для обнаружения препятствий
    //float position_y_BackGroundTriger
    //{
    //    get { return groundTriggerSecond.position.y; }
    //}

    //float position_x_forwardTrigger
    //{
    //    get { return forwardTrigger.position.x; }
    //} //икс и игрик позиция переднего тригера для обнаружения препятствий
    //float position_y_forwardTrigger
    //{
    //    get { return forwardTrigger.position.y; }
    //}


    //public Vector2 _positionPlayer
    //{
    //    get { return player.transform.position; }
    //} //позиция игрока
    //public Vector2 _positionEnemy
    //{
    //    get { return gameObject.transform.position; }
    //} //позиция противника


    //public float Dot()
    //{
    //    //тут у нас скалярное произведение
    //    var d = Vector2.Dot(Vector2.left, DirectionToPlayer());
    //    return d;
    //}//вектор по направлению к игроку

    //public float DistanceToPlayer() //дистанция до игрока
    //{
    //    var distance = Vector2.Distance(_positionEnemy, _positionPlayer);
    //    return distance;
    //}

    //public Vector2 DirectionToPlayer() //направление к игроку
    //{
    //    var dir = _positionEnemy - _positionPlayer;
    //    var result = Vector3.Normalize(dir);
    //    return -result;
    //}

    //public RaycastHit2D RayToPlayer()
    //{
    //    var defaultMask = LayerMask.GetMask("Default");
    //    //  Debug.DrawRay(_positionEnemy, -DirectionToPlayer(), Color.red);
    //    RaycastHit2D hit = Physics2D.Raycast(_positionEnemy, DirectionToPlayer());
    //    return hit;
    //}//луч который всегда направлен к игроку

    //public bool Is_Ground_Collision()
    //{
    //    bool hitGround = false;
    //    //var origin = gameObject.transform.position;
    //    var origin = new Vector2(position_x_FrontgroundTriger, position_y_FrontgroundTriger);
    //    var second_origin = new Vector2(position_x_BackGroundTriger, position_y_BackGroundTriger);
    //    var direction = Vector2.down;
    //    // var distance = 0.2f;

    //    var defaultMask = LayerMask.GetMask("Default");
    //    RaycastHit2D hit = Physics2D.Raycast(origin, direction, GroundCollisionDistance, defaultMask);
    //    RaycastHit2D second_hit = Physics2D.Raycast(second_origin, direction, GroundCollisionDistance, defaultMask);
    //    // print(hit.collider.name);
    //    if (needDrawLine == true)
    //    {
    //        Debug.DrawRay(origin, direction * GroundCollisionDistance, Color.blue, 0.25f);
    //        Debug.DrawRay(second_origin, direction * GroundCollisionDistance, Color.green, 0.25f);
    //    }

    //    try
    //    {
    //        if (hit.collider != null || second_hit.collider != null)
    //        //if (hit.collider != null)
    //        {
    //            hitGround = true;
    //            //  print("ground coll =" + hit.collider.name);
    //        }
    //        else
    //        {
    //            hitGround = false;
    //            //  print("null obj of down");
    //        }
    //        return hitGround;
    //    }
    //    catch
    //    {
    //        return hitGround = false;
    //    }
    //}//проверка на наличие земли под ногами
    //public bool Is_Front_Collision()
    //{
    //    // print("hi from front col");
    //    bool hitFront = false;
    //    var origin = new Vector2(position_x_FrontgroundTriger, position_y_FrontgroundTriger);
    //    var second_origin = new Vector2(position_x_forwardTrigger, position_y_forwardTrigger);
    //    var direction = isEnemyRight ? Vector2.right : Vector2.left; //new Vector2(directionX, position_y_groundTriger);

    //    var defaultMask = LayerMask.GetMask("Default");
    //    RaycastHit2D hit = Physics2D.Raycast(origin, direction, FrontCollisionDistance, defaultMask);
    //    RaycastHit2D second_hit = Physics2D.Raycast(second_origin, direction, FrontCollisionDistance, defaultMask);

    //    if (needDrawLine == true)
    //    {
    //        Debug.DrawRay(origin, direction * FrontCollisionDistance, Color.white, 0.25f);
    //        Debug.DrawRay(second_origin, direction * FrontCollisionDistance, Color.yellow, 0.25f);
    //    }

    //    try
    //    {
    //        if (hit.collider != null || second_hit.collider != null)
    //        {
    //            hitFront = true;
    //            //  print("forward coll =" + hit.collider.name);
    //        }
    //        else
    //        {
    //            hitFront = false;
    //            //  print("null obj of forward");
    //        }//
    //        return hitFront;
    //    }
    //    catch
    //    {
    //        return hitFront = false;
    //    }
    //}//проверка на наличие препятствия впереди


    //public void Move(float speed)
    //{

    //    Animation("Run");

    //    Vector2 dir = DirectionToPlayer();
    //    float direction = dir.x;
    //    _rigidbody.velocity = new Vector2(direction * speed, 0);
    //}//метод движения

    //public void Turn_Enemy(bool right)
    //{
    //    if (right)
    //    {
    //        _directionEnemy = Quaternion.Euler(0, _leftRotation, 0);
    //    }
    //    else
    //    {
    //        _directionEnemy = Quaternion.Euler(0, _rightRotation, 0);
    //    }
    //    transform.rotation = _directionEnemy;
    //    isEnemyRight = right;
    //}//метод поворота

    //public void Jump(float jumpForce)
    //{
    //    timerstarted = true;
    //    Debug.Log("I before jumping");
    //    // print(jumpForce);
    //    _rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    //    Debug.Log("I jumped already");
    //}//метод прыжка

    //public void Shoot(Rigidbody2D bullet, Transform point)
    //{
    //    _animation.Play("Shoot");
    //    this.bullet = bullet;
    //    this.point = point;
    //}//стрельба
    //void BulletShoot()
    //{
    //    Rigidbody2D buletObj = Instantiate(bullet, point.transform.position, Quaternion.identity) as Rigidbody2D;
    //    buletObj.AddForce(DirectionToPlayer() * buletObj.GetComponent<Bullet>().bullet_speed, ForceMode2D.Impulse);
    //}

    //public void Stay()
    //{
    //    Animation("Stay");
    //}//метод что бы просто стоять

    //void Animation(string Do)
    //{
    //    switch (Do)
    //    {
    //        case "Run":
    //            _animation.Play(Do);
    //            break;
    //        case "Shoot":
    //            _animation.Play(Do);
    //            break;
    //        case "Stay":
    //            _animation.Play(Do);
    //            break;
    //    }
    //}//здесь вся анимация


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.tag == "Player")
    //    {
    //        CollisionToPlayer = true;
    //    }
    //    if(collision.transform.tag == "bullet")
    //    {
    //        HP -= collision.transform.GetComponent<Bullet>().Damage;
    //    }

    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.transform.tag == "Player")
    //    {
    //        CollisionToPlayer = false;
    //    }
    //}
}
