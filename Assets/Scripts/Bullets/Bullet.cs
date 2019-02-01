using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float bullet_speed = 3;
    public int Damage = 1;
   // public GameObject bulletHitEffect_1;
    Rigidbody2D _rigidbody;
    // Rigidbody2D rb;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.mass = 10;
        _rigidbody.gravityScale = 0.1f;
        //  rb = GetComponent<Rigidbody2D>();

        // rb.velocity = new Vector2(Vector2.right.x * bullet_speed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.transform.tag == "enemy")
        //{
           // var buletObj = Instantiate(bulletHitEffect_1, gameObject.transform.position, Quaternion.identity);
        //}
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
