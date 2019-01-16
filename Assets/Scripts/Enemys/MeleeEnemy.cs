using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PUNCH;
  //  public Transform startPointPush;
    public Transform endPointPush;
    public float PunchForce = 70;
    public int Damage = 1;

   // Vector2 start;
   // Vector2 end;

    Vector2 start
    {
        get { return gameObject.transform.position; }
    }
    Vector2 end
    {
        get { return endPointPush.position; }
    }

    Animator _animator;

    void Start()
    {
        PUNCH.GetComponent<Rigidbody2D>().mass = PunchForce;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // start = gameObject.transform.position;
      //  end = endPointPush.position;
    }

    public void Atack()
    {
        _animator.Play("Melee");
    }

    public void Punch()
    {
        
       // punchPoint.transform.position = S;
        PUNCH.GetComponent<CircleCollider2D>().enabled = true;
        PUNCH.transform.position = Vector3.Lerp(start,end,1f);

    }

    public void StopPunch()
    {
        PUNCH.GetComponent<CircleCollider2D>().enabled = false;
        PUNCH.transform.position = start;
        //  punchPoint.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            PUNCH.transform.position = start;
        }
    }
}
