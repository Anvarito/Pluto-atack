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
    Animator _animation;
    public float speed = 3;

    private const float _rightRotation = 0; //направление вправо и одновременно начальное направление
    private const float _leftRotation = 180;//направление влево
    private Quaternion _directionEnemy;

    void Start()
    {
        _Ai = GetComponent<Ai_Enemy>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_Ai.DistaceToPlayer() < 7f)
        {
            Move();
        }
        else
        {
            _Ai.Stay();
        }
        var defaultMask = LayerMask.GetMask("Default");
        Debug.DrawRay(_Ai._positionEnemy, -_Ai.DirectionToPlayer(), Color.red);
        RaycastHit2D hit = Physics2D.Raycast(_Ai._positionEnemy, -_Ai.DirectionToPlayer());
        print(hit.collider.name);
       // print(_Ai._positionPlayer);
        if (hit.collider.tag != "Player")
        {
            _rigidbody.AddForce(transform.up * 20, ForceMode2D.Impulse);
        }


    }

    void Move()
    {
        _Ai.Run();

        Vector2 dir = -_Ai.DirectionToPlayer();
        float direction = dir.x;
        _rigidbody.velocity = new Vector2(direction, 0);

        if (_Ai.Dot() > 0)
        {
            Turn_Enemy(false);
        }
        else
        {
            Turn_Enemy(true);
        }
    }


    void Turn_Enemy(bool right)
    {
        if (right)
        {
            _directionEnemy = Quaternion.Euler(0, _rightRotation, 0);
        }
        else
        {
            _directionEnemy = Quaternion.Euler(0, _leftRotation, 0);
        }
        transform.rotation = _directionEnemy;
        //  _directionEnemy = right;
    }
}

