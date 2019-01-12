using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ai_Enemy))]
public class MoveEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    Ai_Enemy _Ai;
    // Rigidbody2D _rigidbody;
    // Animator _animation;
    public float speed = 3;
    public float timer;



    private Quaternion _directionEnemy;
    bool contactToPlayer = false;

    void Start()
    {
        _Ai = GetComponent<Ai_Enemy>();
        //  _rigidbody = GetComponent<Rigidbody2D>();
        // _animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_Ai._PlayerInZoneVisible)
        {
            if (!_Ai._PlayerInZoneAtack && !_Ai.Is_Front_Collision() && _Ai.Is_Ground_Collision())
                _Ai.Move(speed);

            _Ai.Turn_Enemy(!(_Ai.Dot() > 0));
        }
        else
        {
            _Ai.Stay();
        }
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //   // print(collision.transform.tag);
    //    if (collision.transform.tag == "Player")
    //    {
    //        _Ai.Stay();
    //        contactToPlayer = true;
    //    }
    //    else
    //        contactToPlayer = false;
    //}


}

