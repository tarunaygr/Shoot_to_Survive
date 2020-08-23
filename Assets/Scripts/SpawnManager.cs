using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    public Transform[] SpawnPoints;
    // Start is called before the first frame update
    int point;
    public GameObject enemyPrefab;
    void Start()
    {
        StartCoroutine(SpawnEnemies());
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemies()
    {

        while (!GameManager.isGameOver)
        {
            yield return new WaitForSeconds(1f);
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < 6&&!GameManager.isPaused)
            {
                point = Random.Range(0, 6);
                //  GameObject Spawned = Instantiate(enemyPrefab, SpawnPoints[point].position, Quaternion.identity);
                GameObject Spawned = PoolManager._instance.RequestEnemy();
                Spawned.transform.position = SpawnPoints[point].position;
                Spawned.GetComponent<NavMeshAgent>().speed = Random.Range(2, 5);
            }


        }
    }
}
