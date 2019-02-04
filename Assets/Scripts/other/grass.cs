using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grass : MonoBehaviour
{

    Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "ground" && collision.tag != "bullet") 
        {
            print("enter");
            if (collision.transform.position.x > gameObject.transform.position.x)
            {
                print("!!!");
                _anim.Play("grassLeft");
            }
            else
            {
                print("!!!");
                _anim.Play("grassRight");
            }
        }
        
    }
}
