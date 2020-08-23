using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_script : MonoBehaviour
{
    // Start is called before the first frame update
    public bool FiredbyEnemy;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag=="Environment")
        {
            Destroy(gameObject);
           // gameObject.SetActive(false);
        }
        
    }
     /*void Hide()
      {
          gameObject.SetActive(false);
      }
      private void OnEnable()
      {
          Invoke("Hide", 2f);
      }*/
    private void Start()
    {
        Destroy(gameObject, 2f);
    }
    public void EnemyFired()
    {
        FiredbyEnemy = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag=="Enemy"&&!FiredbyEnemy)
        {
            other.GetComponent<EnemyAI>().Damage();
        }
        else if(other.transform.tag=="Player"&&FiredbyEnemy)
        {
            other.GetComponent<Player>().Damage(20);
        }
    }
}
