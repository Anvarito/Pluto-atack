using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player_Controller))]
public class User_Controller : MonoBehaviour
{

    private Player_Controller pc;
    public Gun gun;
    // Use this for initialization
    void Start()
    {
        pc = GetComponent<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        pc.Move(Input.GetAxis("Horizontal"));

        if (Input.GetButtonDown("Jump"))
            pc.Jump();

        if (Input.GetButton("Fire1"))
            gun.Fire();
        //else if (Input.GetButtonUp("Fire1"))
        //    gun.StopFire();
    }
}
