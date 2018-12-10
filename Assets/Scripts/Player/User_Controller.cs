using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player_Controller))]
public class User_Controller : MonoBehaviour {

    private Player_Controller pc;
	// Use this for initialization
	void Start () {
        pc = GetComponent<Player_Controller>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump"))
            pc.Jump();

        pc.Move(Input.GetAxis("Horizontal"));
    }
}
