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
    public Transform GroundTrigger;


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
        get { return GroundTrigger.position.x; }
    }

    float position_y_groundTriger
    {
        get { return GroundTrigger.position.y; }
    }

    bool _isPlayerRight;

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
        _isPlayerRight = right;
    }

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        _animation = GetComponent<Animator>();
    }

    //бег
    public void Move(float ax)
    {
        if (ax > 0)
        {
            Turn_Player(true);
        }
        else if (ax < 0)
        {
            Turn_Player(false);
        }
        else
        {
            //if (Is_Ground())
                _animation.SetInteger("Move", (int)Do.Stop);
            return;
        }


        //  print(Is_Front_Collision());
        if (!Is_Front_Collision())
        {
            _animation.Play("Move");

            Vector2 vector = Vector2.right * ax;
            float direction = vector.x;
            Rb.velocity = new Vector2(direction * runSpeed, Rb.velocity.y);
        }
        else
        {
            //_animation.SetInteger("Move", (int)Do.Stop);
        }

        //print("position: " + GroundTrigger.position.x);
        //print("local position: " + GroundTrigger.localPosition.x);
        //  transform.position = Vector3.Lerp(transform.position, transform.position + direction, runSpeed * Time.deltaTime);
    }

    //void RotateChar(float directChar)
    //{
    //    transform.rotation = Quaternion.Euler(new Vector2(transform.rotation.x, directChar));
    //    _animation.SetInteger("Move", (int)Do.Run);
    //}

    //прыгок
    public void Jump()
    {
        if (Is_Ground())
        {
            Rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            _animation.SetInteger("Move", (int)Do.Jump);
            print("jump " + _animation.recorderMode);
        }


    }

    bool Is_Front_Collision()
    {
        bool hitGround = false;
        //var startOrigin = 0.5f;
        //var startDirection = 0.6f;
        //var originX = position_x_groundTriger;
        //var directionX = _isPlayerRight ? position_x_groundTriger + 1f : position_x_groundTriger - 1f;

        var origin = new Vector2(position_x_groundTriger, position_y_groundTriger);
        var direction = _isPlayerRight ? Vector2.right : Vector2.left; //new Vector2(directionX, position_y_groundTriger);
        var distance = 0.2f;

        var defaultMask = LayerMask.GetMask("Default");
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, defaultMask);
        Debug.DrawRay(origin, direction * distance, Color.blue);

        if (hit.collider != null)
        {
            hitGround = true;
            print(hit.collider.name);
        }
        else
        {
            hitGround = false;
            print("null obj of forward");
        }
        return hitGround;
    }


    bool Is_Ground()
    {
        //cмотрим задевает ли наш сёркл колайдер что то кроме нас самих
        //выявляем его позицию а затем радиус умноженный на два
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundTrigger.transform.position, GroundTrigger.radius * 2);
        //int temp = 0;
        //foreach (var col in colliders)
        //{
        //    if (col.gameObject != gameObject)
        //        temp++;
        //}
        //return temp > 0; //если больше нуля то тру
        bool hitGround = false;
        var origin = new Vector2(position_x_groundTriger, position_y_groundTriger/* - 0.4f*/);
        var direction = Vector2.down;
        var distance = 0.2f;

        var defaultMask = LayerMask.GetMask("Default");
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, defaultMask);
        Debug.DrawRay(origin, direction * distance, Color.blue, 0.25f);

        if (hit.collider != null)
        {
            // float check = (transform.position.y + transform.localScale.y) / 1.9f;
            // hitGround = hit.distance <= check;  // to be sure check slightly beyond bottom of capsule
            hitGround = true;
            print(hit.collider.name);
        }
        else
        {
            hitGround = false;
            print("null obj of down");
        }
        return hitGround;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //   print("!!!!");
        //if (!Is_Ground())
        //{
        //    Rb.AddForce(Vector2.down*2, ForceMode2D.Impulse);
        //}
    }
}
