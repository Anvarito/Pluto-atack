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
                int enemyNumber=0;

                if (point.tag == "shootingEnemy")
                    enemyNumber = 0;
                if (point.tag == "meleeEnemy")
                    enemyNumber = 1;


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
