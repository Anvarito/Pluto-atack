using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float bullet_speed = 3;
   // Rigidbody2D rb;

    void Start()
    {
      //  rb = GetComponent<Rigidbody2D>();

       // rb.velocity = new Vector2(Vector2.right.x * bullet_speed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
