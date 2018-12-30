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
        }
        else
        {
            _directionGun = Quaternion.Euler(transform.rotation.x, 180, 0);
        }
        transform.rotation = _directionGun;
    }

    public void Fire()
    {
        animator.Play("Fire");
    }
    public void StopFire()
    {
        animator.Play("StopFire");
    }
}
