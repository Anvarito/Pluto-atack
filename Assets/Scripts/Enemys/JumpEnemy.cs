using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ai_Enemy))]
public class JumpEnemy : MonoBehaviour
{

    Ai_Enemy _Ai;
    public float jumpForce = 100;
    // Start is called before the first frame update
    void Start()
    {
        _Ai = GetComponent<Ai_Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_Ai._PlayerInZoneVisible == true && _Ai.CollisionToPlayer != true)
            Jump();
    }

    void Jump()
    {

        if ( _Ai.Is_Front_Collision())
        {
            if (_Ai.Is_Ground_Collision() == true)
                _Ai.Jump(jumpForce);
        }

    }
}
