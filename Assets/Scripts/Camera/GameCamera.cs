using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{

    // Use this for initialization
    public Transform player;
    float right_limit = -31.97f; //предел слежения за игроком справа
    float left_limit = 24.88f;//слева
    public float Y_correct = 0.2f; //корректировка камеры по игрику относительно игрока
    public float time_for_lerp = 4; //время для интерполяции камеры за игроком

    Vector3 PlayerPosition
    {
        get { return new Vector3(player.position.x, player.position.y + Y_correct, transform.position.z); }
    }

    Vector3 LimitCamera {
        get {  return new Vector3(transform.position.x, player.position.y + Y_correct, transform.position.z); }
    }

    void Start()
    {
        transform.position = PlayerPosition; //камера на игрока вначале
    }
    


    void LateUpdate()
    {
        //если игрок за пределами то камера уже не летает за ним по иксу, только по игрику
        if (player.position.x > right_limit && player.position.x < left_limit)
                transform.position = Vector3.Lerp(transform.position, PlayerPosition, Time.deltaTime * time_for_lerp);
        else
            transform.position = Vector3.Lerp(transform.position, LimitCamera, Time.deltaTime * time_for_lerp);
    }
}
