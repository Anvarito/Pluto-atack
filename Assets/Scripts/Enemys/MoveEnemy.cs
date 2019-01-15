using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ai_Enemy))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class MoveEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    Ai_Enemy _Ai;
    Rigidbody2D _rigidbody;
    Animator _animator;
    // Rigidbody2D _rigidbody;
    // Animator _animation;
    public Transform groundTrigger;
    public Transform groundTriggerSecond;
    public Transform forwardTrigger;
    public float GroundCollisionDistance = 0.2f;
    public float FrontCollisionDistance = 0.2f;
    public bool needDebugDrawLine = false;

    public float speed = 3;
    public float jumpForce;

    [System.NonSerialized]
    public bool isEnemyRight;

    private const float _rightRotation = 0; //направление вправо и одновременно начальное направление
    private const float _leftRotation = 180;//направление влево
    private Quaternion _directionEnemy;
    private float timer = 0;
    private bool timerstarted = false;

    private float JumpTime = 0.5f; //время через которое во время прыжка может снова идти вперёд AI

    bool contactToPlayer = false;

    void Start()
    {
        _Ai = GetComponent<Ai_Enemy>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        //  _rigidbody = GetComponent<Rigidbody2D>();
        // _animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerstarted)
            timer += Time.deltaTime;


        // if (_PlayerInZoneVisible)
        // {
        //  if (!_PlayerInZoneAtack && !Is_Front_Collision() && (Is_Ground_Collision() || (timer <= JumpTime) && timerstarted))
        //    Move(speed);

        //Turn_Enemy(!(Dot() > 0));
        // }
        //else
        //{
        //    Stay();
        //}

        if (timer >= JumpTime)
        {
            timerstarted = false;
            timer = 0;
        }

        //Debug.Log(gameObject.name +" "+ timer);
        // poitForRay = RayToPlayer();
        //if (_Ai._PlayerInZoneVisible)
        //{
        //    if (!_Ai._PlayerInZoneAtack && !_Ai.Is_Front_Collision() && _Ai.Is_Ground_Collision())
        //        _Ai.Move(speed);

        //    _Ai.Turn_Enemy(!(_Ai.Dot() > 0));
        //}
        //else
        //{
        //    _Ai.Stay();
        //}
    }


    float position_x_FrontgroundTriger
    {
        get { return groundTrigger.position.x; }
    } //икс и игрик позиция первого тригера для обнаружения препятствий
    float position_y_FrontgroundTriger
    {
        get { return groundTrigger.position.y; }
    }

    float position_x_BackGroundTriger
    {
        get { return groundTriggerSecond.position.x; }
    } //икс и игрик позиция второго тригера для обнаружения препятствий
    float position_y_BackGroundTriger
    {
        get { return groundTriggerSecond.position.y; }
    }

    float position_x_forwardTrigger
    {
        get { return forwardTrigger.position.x; }
    } //икс и игрик позиция переднего тригера для обнаружения препятствий
    float position_y_forwardTrigger
    {
        get { return forwardTrigger.position.y; }
    }

    public bool Is_Ground_Collision()
    {
        bool hitGround = false;
        //var origin = gameObject.transform.position;
        var origin = new Vector2(position_x_FrontgroundTriger, position_y_FrontgroundTriger);
        var second_origin = new Vector2(position_x_BackGroundTriger, position_y_BackGroundTriger);
        var direction = Vector2.down;
        // var distance = 0.2f;

        var defaultMask = LayerMask.GetMask("Default");
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, GroundCollisionDistance, defaultMask);
        RaycastHit2D second_hit = Physics2D.Raycast(second_origin, direction, GroundCollisionDistance, defaultMask);
        // print(hit.collider.name);
        if (needDebugDrawLine == true)
        {
            Debug.DrawRay(origin, direction * GroundCollisionDistance, Color.blue, 0.25f);
            Debug.DrawRay(second_origin, direction * GroundCollisionDistance, Color.green, 0.25f);
        }

        try
        {
            if (hit.collider != null || second_hit.collider != null)
            //if (hit.collider != null)
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
        var origin = new Vector2(position_x_FrontgroundTriger, position_y_FrontgroundTriger);
        var second_origin = new Vector2(position_x_forwardTrigger, position_y_forwardTrigger);
        var direction = isEnemyRight ? Vector2.right : Vector2.left; //new Vector2(directionX, position_y_groundTriger);

        var defaultMask = LayerMask.GetMask("Default");
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, FrontCollisionDistance, defaultMask);
        RaycastHit2D second_hit = Physics2D.Raycast(second_origin, direction, FrontCollisionDistance, defaultMask);

        if (needDebugDrawLine == true)
        {
            Debug.DrawRay(origin, direction * FrontCollisionDistance, Color.white, 0.25f);
            Debug.DrawRay(second_origin, direction * FrontCollisionDistance, Color.yellow, 0.25f);
        }

        try
        {
            if (hit.collider != null || second_hit.collider != null)
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
   

    public void Run(Vector2 DirectionToPlayer)
    {
        if (!Is_Front_Collision() && (Is_Ground_Collision() || (timer <= JumpTime) && timerstarted))
        {
            _animator.Play("Run");

            Vector2 dir = DirectionToPlayer;
            float direction = dir.x;
            _rigidbody.velocity = new Vector2(direction * speed, 0);
        }

        if(Is_Front_Collision() && Is_Ground_Collision() )
        {
            Jump();
        }

    }//метод движения

    public void Turn(float Dot)
    {
        var right = (!(Dot > 0));

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

    public void Jump()
    {
        timerstarted = true;
       // Debug.Log("I before jumping");
        // print(jumpForce);
        _rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
       // Debug.Log("I jumped already");
    }//метод прыжка

    public void Stay()
    {
        _animator.Play("Stay");
    }//метод что бы просто стоять

   


}

