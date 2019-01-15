using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject punchPoint;
  //  public Transform startPointPush;
    public Transform endPointPush;
    public float PunchForce = 70;
    public int Damage = 1;

    Vector2 S;
    Vector2 E;
   // Rigidbody2D _rigidbody;
    Animator _animator;

    void Start()
    {
        punchPoint.GetComponent<Rigidbody2D>().mass = PunchForce;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        S = gameObject.transform.position;
        E = endPointPush.position;
    }

    public void Atack()
    {
        _animator.Play("Melee");
    }

    public void Punch()
    {
        
       // punchPoint.transform.position = S;
        punchPoint.GetComponent<CircleCollider2D>().enabled = true;
        punchPoint.transform.position = Vector3.Lerp(S,E,1f);

    }

    public void StopPunch()
    {
        punchPoint.GetComponent<CircleCollider2D>().enabled = false;
        punchPoint.transform.position = S;
        //  punchPoint.SetActive(false);
    }
}
