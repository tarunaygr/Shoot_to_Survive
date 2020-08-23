using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun_script : MonoBehaviour
{
    public float rof;
    public int mag_size=8,fired_count;
    public GameObject bullet_prefab;
    public Transform bullet_spawnpoint;
    public Animator Gun_anim;
    public bool canFire;
    float bullet_speed = 1000f;
    public Text BulletCount;

   /* private void LateUpdate()
    {
        transform.position = Camera.main.transform.position + new Vector3(0.673f, -0.4700003f, 1.443f);
    }*/
    // Start is called before the first frame update
    void Start()
    {
        Gun_anim = GetComponent<Animator>();
        fired_count = 0;
        canFire = true;
        UpdateCount(mag_size - fired_count);
    }
  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)&&fired_count!=0)
        {
            Reload();
        }
    }
    void UpdateCount(int n)
    {
        BulletCount.text = n.ToString();
    }

    public void Fire()
    {
        GameObject FiredBullet = Instantiate(bullet_prefab, bullet_spawnpoint.position, Quaternion.identity);
        /*GameObject FiredBullet = PoolManager._instance.RequestBullet();
        FiredBullet.transform.position = Camera.main.transform.position;*/
        // FiredBullet.transform.forward = Camera.main.transform.forward;
        Gun_anim.SetTrigger("Fired");
        FiredBullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * bullet_speed);
        fired_count++;
        if (fired_count>=Mathf.Round(0.8f*mag_size))
        {
            UI_Manager.Instance.ReloadText();
        }
        UpdateCount(mag_size - fired_count);
        if (fired_count >= mag_size)
        {
            canFire = false;
        }
        else
        {
            canFire = true;
        }

    }
    void Reload()
    {
        canFire = false;
        UI_Manager.Instance.RemovePromptText();
        Gun_anim.SetTrigger("Reload");
        Invoke("GunReloaded", 0.2f);
        //Debug.Break();
        //canFire = true;
        fired_count = 0;
        UpdateCount(mag_size - fired_count);
    }
    void GunReloaded()
    {
        canFire = true;
    }    
}
