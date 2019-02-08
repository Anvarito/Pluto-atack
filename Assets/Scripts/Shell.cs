using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Shell : MonoBehaviour
{
    public float speed = 3;
    public int damage = 1;
    protected Rigidbody2D body;
    public float mass;
    public float gravityScale;

    protected void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.mass = mass;
        body.gravityScale = gravityScale;
        Fly();
    }

    void Update()
    {

    }
    
    void Fly()
    {
        body.AddForce(Vector2.right * speed,ForceMode2D.Impulse);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
