using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    // Use this for initialization
    public GameObject gun;
    public Animator animator;
    private Player_Controller pc;
    private Quaternion _directionGun;
    public Transform point_for_gun;
    void Start()
    {
        pc = GetComponent<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        float newY = Mathf.Lerp(gun.transform.position.y, point_for_gun.position.y, 0.5f);
        gun.transform.position = new Vector3(point_for_gun.position.x, newY, point_for_gun.position.z);
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
        gun.transform.rotation = _directionGun;
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
