using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ai_Enemy))]
public class JumpEnemy : MonoBehaviour
{

    Ai_Enemy _Ai;
    public float jumpForce = 300;
    // Start is called before the first frame update
    void Start()
    {
        _Ai = GetComponent<Ai_Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_Ai._PlayerInZoneVisible == true && _Ai.CollisionToPlayer != true)
        {
           // Debug.Log("I can jump");
            Jump();
        }
    }

    void Jump()
    {

        if ( _Ai.Is_Front_Collision())
        {
           // Debug.Log("Front colision");
            if (_Ai.Is_Ground_Collision())
            {
            //    Debug.Log("Ground colision");
                _Ai.Jump(jumpForce);
            }
        }

    }
}
