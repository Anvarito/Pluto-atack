using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ai_Enemy))]
public class MeleeEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PUNCH;
    //  public Transform startPointPush;
    //public float PunchForce = 70;
    public int Damage = 1;

    bool PunchStarted = false;
    float time = 0;
    // Vector2 start;
    // Vector2 end;

    Vector2 start
    {
        get { return PUNCH.transform.position; }
    }

    Animator _animator;

    void Start()
    {
       // PUNCH.GetComponent<Rigidbody2D>().mass = PunchForce;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (PunchStarted)
            time += Time.deltaTime;

        if (time >= 0.2f)
        {
            PUNCH.GetComponent<CapsuleCollider2D>().enabled = false;
            PunchStarted = false;
            time = 0;
        }
        // start = gameObject.transform.position;
        //  end = endPointPush.position;
    }

    public void Atack()
    {
        _animator.Play("Melee");
    }

    public void Punch()
    {

        PUNCH.transform.position = start;
        PUNCH.GetComponent<CapsuleCollider2D>().enabled = true;
        PunchStarted = true;
        //PUNCH.transform.position = Vector3.Lerp(start, end, 1f);
       
    }

    //public void StopPunch()
    //{
    //    PUNCH.GetComponent<CapsuleCollider2D>().enabled = false;
    //   // PUNCH.transform.position = start;
    //    //  punchPoint.SetActive(false);
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //if (collision.transform.tag == "Player")
    //    //{
    //    PUNCH.GetComponent<CapsuleCollider2D>().enabled = false;
    //    //PUNCH.transform.position = start;
    //    //}
    //}
}
