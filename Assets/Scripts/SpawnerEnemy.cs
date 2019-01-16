using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> spawnPoints = new List<Transform>();
    //public GameObject shootingEnemy;
    //public GameObject meleeEnemy;

    void Start()
    {
        int t = 0;
        while(t < transform.childCount)//находим все дочерние обьекты и помещаем их в список
        {
            spawnPoints.Add(transform.GetChild(t));
            t++;
        }
       // print(spawnPoints.Count);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            foreach (Transform point in spawnPoints)
            {
                int enemyNumber = 0;

                string tag = point.tag;
                switch (tag)
                {
                    case "shootingEnemySpawner":
                        enemyNumber = 0;
                        break;
                    case "meleeEnemySpawner":
                        enemyNumber = 1;
                        break;
                    default:
                        enemyNumber = 0;
                        break;
                }
                Spawn(AImanager.StaticEnemyList[enemyNumber], point.transform.position, point.transform.rotation);
            }
            Destroy(gameObject);
        }
        //print("ENTER");
    }

    void Spawn(GameObject enemy, Vector2 position, Quaternion rotation)
    {
        Instantiate(enemy, position, rotation);
    }
}
