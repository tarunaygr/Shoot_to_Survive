using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;
    public GameObject _enemyPrefab, _bulletprefab,EnemyPool,BulletPool;
    private List<GameObject> enemypool, bulletpool;
    public static PoolManager _instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //bulletpool = new List<GameObject>();
        enemypool = new List<GameObject>();
       // bulletpool = GenerateBulletPool(10);
        enemypool = GenerateEnemyPool(10);
    }
    List<GameObject> GenerateBulletPool(int n)
    {

        for (int i = 0; i < n; i++)
        {
            GameObject newbullet = Instantiate(_bulletprefab);
            newbullet.transform.parent = BulletPool.transform;
            bulletpool.Add(newbullet);
            newbullet.SetActive(false);
        }
        return bulletpool;
    }
    List<GameObject> GenerateEnemyPool(int n)
    {
       
        for (int i = 0; i < n; i++)
        {
            GameObject newbullet = Instantiate(_enemyPrefab);
            newbullet.transform.parent = EnemyPool.transform;
            enemypool.Add(newbullet);
            newbullet.SetActive(false);
        }
        return enemypool;
    }
    public GameObject RequestBullet()
    {
        foreach(GameObject b in bulletpool)
        {
            if (b.activeSelf==false)
            {
                b.SetActive(true);
                return b;
            }
        }
        GameObject bullet = Instantiate(_bulletprefab);
        bullet.transform.parent =BulletPool.transform;
        bulletpool.Add(bullet);
        return bullet;
    }
    public GameObject RequestEnemy()
    {
        foreach (GameObject b in enemypool)
        {
            if (b.activeSelf == false)
            {
                b.SetActive(true);
                return b;
            }
        }
        GameObject bullet = Instantiate(_enemyPrefab);
        bullet.transform.parent = EnemyPool.transform;
        enemypool.Add(bullet);
        return bullet;
    }
}
