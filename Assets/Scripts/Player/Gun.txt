using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    // Use this for initialization
    private Animator animator;
    public Player_Controller pc;
    private Quaternion _directionGun;
    public Transform point_for_gun;
    public Rigidbody2D bullet;
   // public float speed_bullet = 1;
    public GameObject point_fire;
    Vector2 directionBullet;
    
   // bool fire = true;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float newY = Mathf.Lerp(transform.position.y, point_for_gun.position.y, 0.5f);
        transform.position = new Vector3(point_for_gun.position.x, newY, point_for_gun.position.z);
        Turn();
    }

    void Turn()
    {
        if (pc.isPlayerRight)
        {
            _directionGun = Quaternion.Euler(transform.rotation.x, 0, 0);
            directionBullet = new Vector2(Vector2.right.x, 0);
        }
        else
        {
            _directionGun = Quaternion.Euler(transform.rotation.x, 180, 0);
            directionBullet = new Vector2(Vector2.left.x, 0);
        }
        transform.rotation = _directionGun;
    }

    public void Fire()
    {
       // print(fire);
        animator.Play("Fire");
    }
    //public void StopFire()
    //{
    //    animator.Play("StopFire");
    //}

        //стрельба управляется анимацией
    void InstantiateBUllet()
    {
        //print(transform.rotation.y);
        var buletObj = Instantiate(bullet, point_fire.transform.position, Quaternion.identity);
        buletObj.AddForce(directionBullet * buletObj.GetComponent<Bullet>().bullet_speed, ForceMode2D.Impulse);
    }
}
