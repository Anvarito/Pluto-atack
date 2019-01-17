using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D Rb;
    private Animator _animation;

    public float runSpeed = 7;
    public float jumpForce = 7;
    public int HP = 100;
    public Transform ground_trigger;
    public Transform second_ground_trigger;
    public Transform forwardTrigger;
    public Transform secondforwardTrigger;
    public bool needDrawLine = true;
    public float distanceLine = 0.1f;
    public float FrontCollisionDistance = 0.3f;

    private const float _rightRotation = 0; //направление вправо и одновременно начальное направление
    private const float _leftRotation = 180;//направление влево
    private Quaternion _directionPlayer;
    // private bool _contact_in_air = false;
    private enum Do
    {
        Stop, Run, Jump, Fire
    }

    float position_x_groundTriger
    {
        get { return ground_trigger.position.x; }
    }

    float position_y_groundTriger
    {
        get { return ground_trigger.position.y; }
    }

    float position_x_secondGroundTriger
    {
        get { return second_ground_trigger.position.x; }
    }//два  триггера для обнаружения земли
    float position_y_secondGroundTriger
    {
        get { return second_ground_trigger.position.y; }
    }

    float position_x_forwardTrigger
    {
        get { return forwardTrigger.position.x; }
    } //икс и игрик позиция переднего тригера для обнаружения препятствий
    float position_y_forwardTrigger
    {
        get { return forwardTrigger.position.y; }
    }

    float position_x_secondforwardTrigger
    {
        get { return secondforwardTrigger.position.x; }
    } //икс и игрик позиция переднего тригера для обнаружения препятствий
    float position_y_secondforwardTrigger
    {
        get { return secondforwardTrigger.position.y; }
    }

    [System.NonSerialized]
    public bool isPlayerRight;

    bool moving;

    private void Start()
    {
        isPlayerRight = true;
        Rb = GetComponent<Rigidbody2D>();
        _animation = GetComponent<Animator>();
    }

    private void Update()
    {
        if (moving && Is_Ground_Collision() && !Is_Front_Collision())//если двигается, есть земля, нет препятствий
        {
            _animation.Play("Run");
        }
        else if (moving && !Is_Ground_Collision())//если двигается и нет земли
        {
            _animation.Play("Jump");
        }
        else if (!moving && !Is_Ground_Collision())//если не двигается и  нет земли
        {
            _animation.Play("Jump");
        }
        else
            _animation.Play("Idle");

    }

    //бег
    public void Move(float ax)
    {

        if (ax > 0)
        {
            Turn_Player(true);
            moving = true;
        }
        else if (ax < 0)
        {
            Turn_Player(false);
            moving = true;
        }
        else
        {
            moving = false;
            return;
        }

        if (!Is_Front_Collision())
        {
            Vector2 vector = Vector2.right * ax;
            float direction = vector.x;
            Rb.velocity = new Vector2(direction * runSpeed, Rb.velocity.y);
        }
    }

    //поворот
    void Turn_Player(bool right)
    {
        if (right)
        {
            _directionPlayer = Quaternion.Euler(transform.rotation.x, _rightRotation, 0);
        }
        else
        {
            _directionPlayer = Quaternion.Euler(transform.rotation.x, _leftRotation, 0);
        }
        transform.rotation = _directionPlayer;
        isPlayerRight = right;
    }

    //прыгок
    public void Jump()
    {
        if (Is_Ground_Collision())
        {
            Rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            // _animation.Play("Jump");
        }
    }


    bool Is_Front_Collision()
    {
        bool hitFront = false;
        var origin = new Vector2(position_x_groundTriger, position_y_groundTriger);
        var second_origin = new Vector2(position_x_forwardTrigger, position_y_forwardTrigger);
        var third_origin = new Vector2(position_x_secondforwardTrigger, position_y_secondforwardTrigger);
        var direction = isPlayerRight ? Vector2.right : Vector2.left; //new Vector2(directionX, position_y_groundTriger);

        var defaultMask = LayerMask.GetMask("Default");
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, FrontCollisionDistance, defaultMask);
        RaycastHit2D second_hit = Physics2D.Raycast(second_origin, direction, FrontCollisionDistance, defaultMask);
        RaycastHit2D third_hit = Physics2D.Raycast(third_origin, direction, FrontCollisionDistance, defaultMask);

        if (needDrawLine == true)
        {
            Debug.DrawRay(origin, direction * FrontCollisionDistance, Color.red, 0.25f);
            Debug.DrawRay(second_origin, direction * FrontCollisionDistance, Color.yellow, 0.25f);
            Debug.DrawRay(third_origin, direction * FrontCollisionDistance, Color.grey, 0.25f);
        }


        if (hit.collider != null || second_hit.collider != null || third_hit.collider != null)
        {
            hitFront = true;
          //  print(hit.collider.name);
        }
        else
        {
            hitFront = false;
          //  print("null obj of forward");
        }
        return hitFront;
    }


    bool Is_Ground_Collision()
    {
        bool hitGround = false;
        var origin = new Vector2(position_x_groundTriger, position_y_groundTriger);
        var second_origin = new Vector2(position_x_secondGroundTriger, position_y_secondGroundTriger);
        var direction = Vector2.down;
       // var distance = 0.2f;

        var defaultMask = LayerMask.GetMask("Default");
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distanceLine, defaultMask);
        RaycastHit2D second_hit = Physics2D.Raycast(second_origin, direction, distanceLine, defaultMask);
        if (needDrawLine == true)
        {
            Debug.DrawRay(origin, direction * distanceLine, Color.blue, 0.25f);
            Debug.DrawRay(second_origin, direction * distanceLine, Color.green, 0.25f);
        }

        if (hit.collider != null || second_hit.collider != null)
        {
            hitGround = true;
           // print(hit.collider != null ?  hit.collider.name : second_hit.collider.name);
        }
        else
        {
            hitGround = false;
           // print("null obj of down");
        }
        return hitGround;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //   print("!!!!");
        if (collision.transform.tag == "punch")
        {
           // print(HP);
            HP -= collision.transform.parent.GetComponent<MeleeEnemy>().Damage;
        }
    }
}
