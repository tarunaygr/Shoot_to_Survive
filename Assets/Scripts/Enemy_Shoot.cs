using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    public Transform playerHead;
    public Player _player;
    Transform barrel;
    public GameObject bullet_prefab;
    float bullet_speed = 800;
    float canfire = -1, rof;
    Vector3 aimdirection;
    public Transform bullet_spawnpoint;
    // Start is called before the first frame update
    void Awake()
    {
         rof = 4f;
        _player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player>();
        playerHead = GameObject.FindGameObjectWithTag("Player").transform;
        barrel = gameObject.transform.Find("Gun").Find("Gun_Bullet_Hole").transform;
    }
    public void Aim()
    {
        aimdirection = (playerHead.position - barrel.transform.position).normalized;
        Quaternion aimrotate = Quaternion.LookRotation(aimdirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, aimrotate, Time.deltaTime * 10);
    }
    public void Shoot()
    {
        if (canfire<Time.time&&!GameManager.isPaused)
        {
            aimdirection = (playerHead.position - barrel.transform.position).normalized;
            GameObject FiredBullet = Instantiate(bullet_prefab, bullet_spawnpoint.position, Quaternion.identity);
            FiredBullet.GetComponent<Bullet_script>().EnemyFired();
            FiredBullet.GetComponent<Rigidbody>().AddForce(aimdirection * bullet_speed);
            canfire =Time.time + rof;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
