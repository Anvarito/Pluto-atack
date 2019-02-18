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
    // Rigidbody2D body;
    // Animator _animation;
    public Transform front_ground_trigger;
    public Transform back_ground_trigger;
    public Transform front_trigger;
    public float GroundCollisionDistance = 0.2f;
    public float FrontCollisionDistance = 0.2f;
    public bool needDebugDrawLine = false;
    public bool needSpawnAnimation = true; //данная переменная нужна если есть анимация спавна
    public float speed = 3;
    public float jumpForce = 50;

    [System.NonSerialized]
    public bool isEnemyRight;

    private const float _rightRotation = 0; //направление вправо и одновременно начальное направление
    private const float _leftRotation = 180;//направление влево
    private Quaternion _directionEnemy;
    private float timer = 0;
    private bool timerstarted = false;

    private bool StartAllow = false;//данная переменная нужна если есть анимация спавна

    private float JumpTime = 0.5f; //время через которое во время прыжка может снова идти вперёд AI

    //bool contactToPlayer = false;

    void Start()
    {
        StartAllow = false;
        _Ai = GetComponent<Ai_Enemy>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        if (needSpawnAnimation) //данная конструкция нужна если есть анимация спавна
        {
            _animator.Play("Spawn");

          //  StartAllow = false;
            //needSpawnAnimation = false;
            //StartAllow = true;
        }
      


        //  body = GetComponent<Rigidbody2D>();
        // _animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerstarted)
            timer += Time.deltaTime;

        if (timer >= JumpTime)
        {
            timerstarted = false;
            timer = 0;
        }
        //if (Input.GetKey(KeyCode.J))
        //    Jump();
    }


    float position_x_FrontgroundTriger
    {
        get { return front_ground_trigger.position.x; }
    } //икс и игрик позиция первого тригера для обнаружения препятствий
    float position_y_FrontgroundTriger
    {
        get { return front_ground_trigger.position.y; }
    }

    float position_x_BackGroundTriger
    {
        get { return back_ground_trigger.position.x; }
    } //икс и игрик позиция второго тригера для обнаружения препятствий
    float position_y_BackGroundTriger
    {
        get { return back_ground_trigger.position.y; }
    }

    float position_x_forwardTrigger
    {
        get { return front_trigger.position.x; }
    } //икс и игрик позиция переднего тригера для обнаружения препятствий
    float position_y_forwardTrigger
    {
        get { return front_trigger.position.y; }
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
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, GroundCollisionDistance);
        RaycastHit2D second_hit = Physics2D.Raycast(second_origin, direction, GroundCollisionDistance);
        // print(hit.collider.name);
        if (needDebugDrawLine == true)
        {
            Debug.DrawRay(origin, direction * GroundCollisionDistance, Color.blue, 0.25f);
            Debug.DrawRay(second_origin, direction * GroundCollisionDistance, Color.green, 0.25f);
        }

        //try
        //{
        if (hit.collider != null || second_hit.collider != null)
        //if (hit.collider != null)
        {
            hitGround = true;
            //if (hit.collider != null)
            //    print(gameObject.name + " ground coll = " + hit.collider.name);
            //if (second_hit.collider != null)
            //    print(gameObject.name + " second ground coll = " + second_hit.collider.name);
        }
        else
        {
            hitGround = false;
            //  print("null obj of down");
        }
        return hitGround;
        //}
        //catch
        //{
        //    return hitGround = false;
        //}
    }//проверка на наличие земли под ногами
    public bool Is_Front_Collision()
    {
        // print("hi from front col");
        bool hitFront = false;
        var origin = new Vector2(position_x_FrontgroundTriger, position_y_FrontgroundTriger);
        var second_origin = new Vector2(position_x_forwardTrigger, position_y_forwardTrigger);
        var direction = isEnemyRight ? Vector2.right : Vector2.left; //new Vector2(directionX, position_y_groundTriger);

        var defaultMask = LayerMask.GetMask("Default");
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, FrontCollisionDistance);
        RaycastHit2D second_hit = Physics2D.Raycast(second_origin, direction, FrontCollisionDistance);

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
                //if (hit.collider != null)
                //    print(gameObject.name + " front coll = " + hit.collider.name);
                //if (second_hit.collider != null)
                //    print(gameObject.name + " top front coll = " + hit.collider.name);
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
        //Debug.Log("StartAllow " + StartAllow);
        if (StartAllow)//если старт разрешен, после того как прошла анимация спавна
        {
            //Debug.Log("front collision " + Is_Front_Collision());
            if (!Is_Front_Collision())
            {
                //   Debug.Log("I start");
                _animator.Play("Run");
                //  Debug.Log("Animation start");
                Vector2 dir = DirectionToPlayer;
                float direction = dir.x;
                transform.position += new Vector3(direction, 0, 0) * Time.deltaTime * speed;
                //  Debug.Log("I end");
                // body.velocity = new Vector2(direction * speed, 0);
            }
            else
            {
                //  print(gameObject.name + " i cant run");
                //print(gameObject.name + " - " +Is_Ground_Collision());
                // Jump();
                _animator.Play("Stay");
            }

            if (Is_Front_Collision() && Is_Ground_Collision())
            {
                Jump();
            }

            if (_rigidbody.IsSleeping())
            {
                //  print("sleep rb");
                Jump();
            }
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

    private void OnCollisionStay2D(Collision2D collision) //костыль
    {
        if (collision.collider.tag == "meleeEnemy" && Is_Front_Collision())
        {
            //  Jump();

        }
    }

    public bool Visible()
    {
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), GetComponent<CapsuleCollider2D>().bounds);
    }

    void LetsStartAllow()//данный метод нужен если есть анимация спавна
    {
        StartAllow = true;
      //  needSpawnAnimation = false;
    }
}

