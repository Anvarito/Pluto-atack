using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Ai_Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform groundTrigger;
    public bool needDrawLine = true;
    public float GroundCollisionDistance = 6f;
    public float FrontCollisionDistance = 2f;
    public float numberDistanceForPlayer = 7;
    public float numberDistanceForAtack = 3;

    [System.NonSerialized]
    public GameObject player;
    [System.NonSerialized]
    public RaycastHit2D poitForRay;
    [System.NonSerialized]
    public bool CollisionToPlayer = false;
    [System.NonSerialized]
    public bool isEnemyRight;

    Animator _animation;
    Rigidbody2D _rigidbody;
    Quaternion _directionEnemy;

    Rigidbody2D bullet;// для анимации стрельбы
    Transform point;//для анимации стрельбы



    private const float _rightRotation = 0; //направление вправо и одновременно начальное направление
    private const float _leftRotation = 180;//направление влево


    public bool _PlayerInZoneVisible { get; private set; } = false;//игрок в зоне видимости или нет
    public bool _PlayerInZoneAtack { get; private set; } = false;//игрок в зоне видимости или нет



    float position_x_groundTriger
    {
        get { return groundTrigger.position.x; }
    } //икс и игрик позиция тригера для обнаружения препятствий
    float position_y_groundTriger
    {
        get { return groundTrigger.position.y; }
    }

    void Start()
    {
        player = GameObject.Find("Player");
        _animation = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public Vector2 _positionPlayer
    {
        get { return player.transform.position; }
    } //позиция игрока
    public Vector2 _positionEnemy
    {
        get { return gameObject.transform.position; }
    } //позиция противника

    void Update()
    {
        poitForRay = RayToPlayer();
        Is_Ground_Collision();

        ZoneVisible();
        ZoneAtack();
    }

    void ZoneVisible()
    {
        if (DistanceToPlayer() < numberDistanceForPlayer)
            _PlayerInZoneVisible = true;
        else
            _PlayerInZoneVisible = false;
    }
    void ZoneAtack()
    {
        if (DistanceToPlayer() < numberDistanceForAtack)
            _PlayerInZoneAtack = true;
        else
            _PlayerInZoneAtack = false;
    }

    public float Dot()
    {
        //тут у нас скалярное произведение
        var d = Vector2.Dot(Vector2.left, DirectionToPlayer());
        return d;
    }//вектор по направлению к игроку

    public float DistanceToPlayer() //дистанция до игрока
    {
        var distance = Vector2.Distance(_positionEnemy, _positionPlayer);
        return distance;
    }

    public Vector2 DirectionToPlayer() //направление к игроку
    {
        var dir = _positionEnemy - _positionPlayer;
        var result = Vector3.Normalize(dir);
        return -result;
    }

    public RaycastHit2D RayToPlayer()
    {
        var defaultMask = LayerMask.GetMask("Default");
        //  Debug.DrawRay(_positionEnemy, -DirectionToPlayer(), Color.red);
        RaycastHit2D hit = Physics2D.Raycast(_positionEnemy, DirectionToPlayer());
        return hit;
    }//луч который всегда направлен к игроку

    public bool Is_Ground_Collision()
    {
        bool hitGround = false;
        // var origin = new Vector2(position_x_groundTriger, position_y_groundTriger);
        var origin = gameObject.transform.position;
        // var second_origin = new Vector2(position_x_secondGroundTriger, position_y_secondGroundTriger);
        var direction = Vector2.down;
        // var distance = 0.2f;

        var defaultMask = LayerMask.GetMask("Default");
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, GroundCollisionDistance, defaultMask);
        // RaycastHit2D second_hit = Physics2D.Raycast(second_origin, direction, distanceLine, defaultMask);
        // print(hit.collider.name);
        if (needDrawLine == true)
        {
            Debug.DrawRay(origin, direction * GroundCollisionDistance, Color.blue, 0.25f);
            //  Debug.DrawRay(second_origin, direction * distanceLine, Color.green, 0.25f);
        }

        try
        {
            //if (hit.collider != null || second_hit.collider != null)
            if (hit.collider != null)
            {
                hitGround = true;
              //  print("ground coll =" + hit.collider.name);
            }
            else
            {
                hitGround = false;
              //  print("null obj of down");
            }
            return hitGround;
        }
        catch
        {
            return hitGround = false;
        }
    }//проверка на наличие земли под ногами
    public bool Is_Front_Collision()
    {
        // print("hi from front col");
        bool hitFront = false;
        var origin = new Vector2(position_x_groundTriger, position_y_groundTriger);
        var direction = isEnemyRight ? Vector2.right : Vector2.left; //new Vector2(directionX, position_y_groundTriger);

        var defaultMask = LayerMask.GetMask("Default");
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, FrontCollisionDistance, defaultMask);

        if (needDrawLine == true)
        {
            Debug.DrawRay(origin, direction * FrontCollisionDistance, Color.white, 0.25f);
        }

        try
        {
            if (hit.collider != null)
            {
                hitFront = true;
              //  print("forward coll =" + hit.collider.name);
            }
            else
            {
                hitFront = false;
              //  print("null obj of forward");
            }//
            return hitFront;
        }
        catch
        {
            return hitFront = false;
        }
    }//проверка на наличие препятствия впереди


    public void Move(float speed)
    {
        Animation("Run");

        Vector2 dir = DirectionToPlayer();
        float direction = dir.x;
        _rigidbody.velocity = new Vector2(direction * speed, 0);

        if (Dot() > 0)
        {
            Turn_Enemy(false);
        }
        else
        {
            Turn_Enemy(true);
        }

    }//метод движения

    void Turn_Enemy(bool right)
    {
        if (right)
        {
            _directionEnemy = Quaternion.Euler(0, _leftRotation, 0);
        }
        else
        {
            _directionEnemy = Quaternion.Euler(0, _rightRotation, 0);
        }
        transform.rotation = _directionEnemy;
        isEnemyRight = right;
    }//метод поворота

    public void Jump(float jumpForce)
    {
        // print(jumpForce);
        _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }//метод прыжка

    public void Shoot(Rigidbody2D bullet, Transform point)
    {
        _animation.Play("Shoot");
        this.bullet = bullet;
        this.point = point;
    }//стрельба
    void BulletShoot()
    {
        Rigidbody2D buletObj = Instantiate(bullet, point.transform.position, Quaternion.identity) as Rigidbody2D;
        buletObj.AddForce(DirectionToPlayer() * buletObj.GetComponent<Bullet>().bullet_speed, ForceMode2D.Impulse);
    }

    public void Stay()
    {
        Animation("Stay");
    }//метод что бы просто стоять

    void Animation(string Do)
    {
        switch (Do)
        {
            case "Run":
                _animation.Play(Do);
                break;
            case "Shoot":
                _animation.Play(Do);
                break;
            case "Stay":
                _animation.Play(Do);
                break;
        }
    }//здесь вся анимация


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            CollisionToPlayer = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            CollisionToPlayer = false;
        }
    }
}
