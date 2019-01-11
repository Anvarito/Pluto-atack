using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Player_Controller player;
    Animator _animation;


    public Vector2 _positionPlayer
    {
        get { return player.transform.position; }
    }
    public Vector2 _positionEnemy
    {
        get { return gameObject.transform.position; }
    }




    void Start()
    {
        _animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
       // positionEnemy = gameObject.transform.position;
        

        var defaultMask = LayerMask.GetMask("Default");
        RaycastHit2D hit = Physics2D.Raycast(_positionEnemy, _positionPlayer, defaultMask);
        // print(hit.transform.name);




    }

    public float Dot()
    {
        //тут у нас скалярное произведение
       // var vector = positionEnemy - positionPlayer;
        var d = Vector2.Dot(Vector2.left, DirectionToPlayer());
       // print(d);
        return d;
    }

    public float DistaceToPlayer() //дистанция до игрока
    {
        var distance = Vector2.Distance(_positionEnemy, _positionPlayer);
        //  print(distance);
        return distance;
    }

    public Vector2 DirectionToPlayer() //направление к игроку
    {
        var dir = _positionEnemy - _positionPlayer;
        //dir = Vector3.Normalize(dir);
        return dir;
    }

    public void Shoot()
    {
        _animation.Play("Shoot");
    }

    public void Stay()
    {
        _animation.Play("Stay");
    }
    public void Run()
    {
        _animation.Play("Run");
    }

    public void Jump()
    {

    }

    void BulletShoot()
    {

    }



}
