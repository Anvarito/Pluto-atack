using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AImanager : MonoBehaviour
{
    // Start is called before the first frame update

    //public bool _PlayerInZoneVisible { get; private set; } = false;//игрок в зоне видимости или нет
    //public bool _PlayerInZoneAtack { get; private set; } = false;//игрок в зоне видимости или нет
    [System.NonSerialized]
    public GameObject player;
    [System.NonSerialized]
    public Ai_Enemy curentAI;


    public Vector2 _positionPlayer
    {
        get { return player.transform.position; }
    } //позиция игрока
    public Vector2 _positionAI
    {
        get { return curentAI.transform.position; }
    } //позиция противника

    [System.NonSerialized]
    public static List<Ai_Enemy> AIlist = new List<Ai_Enemy>();//сюда собираются все АИ которые в данный момент есть в игре

    public static List<GameObject> StaticEnemyList = new List<GameObject>();
    public List<GameObject> EnemyList = new List<GameObject>();//тут список всех типо врагов

    void Start()
    {
        player = GameObject.Find("Player");
        StaticEnemyList = EnemyList;
    }

    // Update is called once per frame
    void Update()
    {
        //if (AIlist.Count != 0)
        //    print(AIlist.Count);
        //else
        //    print("AI list is clear");


        foreach (Ai_Enemy AI in AIlist)
        {
            curentAI = AI;
            AI.DistanceToPlayer = DistanceToPlayer();
            AI.Dot = Dot();
            AI.DirectionToPlayer = DirectionToPlayer();


            if (DistanceToPlayer() <= AI.numberDistanceForMove &&
                DistanceToPlayer() >= AI.numberDistanceForShoot)
            {
                AI.moveAllow = true;
                AI.shootAllow = false;
                AI.meleeAllow = false;
            }

            if (DistanceToPlayer() <= AI.numberDistanceForShoot &&
                DistanceToPlayer() >= AI.numberDistanceForMelee &&
                RayToPlayer() == "Player")
            {
                AI.moveAllow = false;
                AI.shootAllow = true;
                AI.meleeAllow = false;
            }
            else
            {
                AI.moveAllow = true;
                AI.shootAllow = false;
                AI.meleeAllow = false;
            }

            if (DistanceToPlayer() <= AI.numberDistanceForMelee)
            {
                AI.moveAllow = false;
                AI.shootAllow = false;
                AI.meleeAllow = true;
            }
        }
    }


    public float Dot()
    {
        //тут у нас скалярное произведение
        var d = Vector2.Dot(Vector2.left, DirectionToPlayer());
        return d;
    }//скаляр до игрока
    public Vector2 DirectionToPlayer() //направление к игроку
    {
        var dir = _positionAI - _positionPlayer;
        var result = Vector3.Normalize(dir);
        return -result;
    }
    public float DistanceToPlayer() //дистанция до игрока
    {
        var distance = Vector2.Distance(_positionAI, _positionPlayer);
        return distance;
    }
    public string RayToPlayer()
    {
        var defaultMask = LayerMask.GetMask("Default");
        RaycastHit2D hit = Physics2D.Raycast(_positionAI, DirectionToPlayer());

       // print(hit.transform.name);
        if (hit.transform.tag != "Player")
        {
            return hit.collider.tag;
        }
        else
        {
           // Debug.DrawLine(_positionAI, _positionPlayer, Color.blue);
            return "Player";
        }
    }//луч к игроку
}
