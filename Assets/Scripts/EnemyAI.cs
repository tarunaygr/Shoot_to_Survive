using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform target;
    int EnemyStrength=20;
    GameObject gun;
    public Player _player;
    ParticleSystem ps;
    int health = 6;

    // Start is called before the first frame update
    void Start()
    {
        gun = gameObject.transform.Find("Enemy_Weapon").gameObject;
        ps = GetComponent<ParticleSystem>();
        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        _player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void OnEnable()
    {
        health = 3;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        agent.SetDestination(target.position);
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance<=agent.stoppingDistance)
        {
            FaceTarget();
            gun.GetComponent<Enemy_Shoot>().Aim();
            gun.GetComponent<Enemy_Shoot>().Shoot();    
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookdirection = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookdirection, Time.deltaTime * 10f);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag=="Player")
        {
            other.transform.GetComponent<Player>().Damage(EnemyStrength);
            //  StartCoroutine(DamagePlayer(EnemyStrength, other.GetComponent<Player>()));
        }
        if (other.transform.tag=="Bullet"&&!other.GetComponent<Bullet_script>().FiredbyEnemy)
        {
            Damage();
            other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
            
            
        }
    }*/
    public void Damage()
    {
        ps.Play();  
        health--;
        if (health<=0)
        {
            // Destroy(gameObject);
            gameObject.SetActive(false);
            _player.Kill();
        }
    }

}

