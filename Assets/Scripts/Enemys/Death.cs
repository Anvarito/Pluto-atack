using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(Ai_Enemy))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Death : MonoBehaviour
{

    Rigidbody2D _rigidbody;
    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Die()
    {
        AImanager.AIlist.Remove(gameObject.GetComponent<Ai_Enemy>());//удаление данного аи из списка
        //размер и позиция коллайдера корректируется после смерти 
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.6f, 0.1f);
        gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, -1f);
        gameObject.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;

        _animator.Play("Death");
    }

    void EraseGameObject()//вызывается анимацией
    {
        Destroy(gameObject);
    }
}
