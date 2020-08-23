using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    float mouseX,VInP,HInP;
    
    public Transform bullet_spawnpoint;
    [SerializeField]
    float sens = 5f,MovementSpeed;
    [SerializeField]
    float gravity = -9.81f, cachedYval,jumpHeight;
    [SerializeField]
    LookY _verticalLook;
    private bool isPaused;
    public GameObject Gun;
    Gun_script _gunscript;
    [SerializeField]
    CharacterController cc;
    [SerializeField]
    bool canRun = false, canJump = true,canMove,isCrouching,isRunning,isSliding,FinishedSliding;
    private bool canSlide;
    [SerializeField]
    private float runSpeed,crouchSpeed;
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    Vector3 temp;
    [SerializeField]
    Animator anim;
    public UI_Manager CanvasHandler;
    int count = 0, _lives = 3;
    public GameManager gm;
    public Vector3 SpawnPoint;
    private float maxHealth = 100f, currentHealth;
    public static Action<float,float> PlayerDamage;
    public static Action<int> KilledEnemy;
    public static Action<int> PlayerDeath;
    public GameObject bullet_prefab;
    public float rof = 0.5f,canfire=-1;
  

    void Start()
    {

        
        _gunscript = Gun.GetComponent<Gun_script>();
        isPaused = false;
        MovementSpeed = 10f;
        canJump = true;
        isCrouching = false;
        isSliding = false;
        currentHealth = 100f;
        CanvasHandler.UpdateKillCounter(count);
        UI_Manager.Instance.UpdateKillCounter(count);
        Spawn();

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isPaused)
        {
            totalLook();
            crouch();
            Movement();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Damage(20);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Kill();
        }


    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&&!GameManager.isPaused&&canfire<Time.time&&_gunscript.canFire)
        {
            canfire = Time.time + _gunscript.rof;
            _gunscript.Fire();
        }
    }



    //Controls The looking of the player
    void totalLook()
    {
        mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sens;
        transform.Rotate(Vector3.up * mouseX);
        _verticalLook.VerticalLook();
    }
    //Controls player movement
    private void Movement()
    {
 
            GetInput();
       
        if (VInP >= 0&&!isCrouching)
        {
            canRun = true;
        }
        else
        {
            canRun = false;
        }
        if (canRun&&Input.GetKey(KeyCode.LeftShift)&&VInP>0)
        {
            VInP *= runSpeed;
            isRunning = true;
            canSlide = true;
        }
        else
        {
            isRunning = false;
        }

        Vector3 move = new Vector3(HInP, 0, VInP);
        if (isCrouching)
        {
            move *= crouchSpeed;
        }
        Vector3 moveDirection=transform.transform.TransformDirection(move);
        if (Input.GetKeyDown(KeyCode.Space)&&cc.isGrounded&&canJump&&!isCrouching)
        {
            cachedYval = Mathf.Sqrt(-2f*jumpHeight*gravity);
        }
        else if(cc.isGrounded)
        {
                cachedYval = -2f;
        }
        else
        {
            cachedYval += 2*gravity * Time.deltaTime;
        }
        moveDirection.y = cachedYval;

            cc.Move(moveDirection * MovementSpeed * Time.deltaTime);
        
    }
    //Player crouching
        void crouch()
        {
           if (Input.GetKeyDown(KeyCode.C))
           {

            Debug.Log("pressed c");
            if (!isCrouching)
            {
                isCrouching = true;
                anim.SetBool("crouching", true);
                cc.center = new Vector3(cc.center.x, cc.center.y - .47f, cc.center.z);
                cc.height /= 3;
            }
            else
            {
                Debug.Log(isCrouching);
                 isCrouching = false;
                anim.SetBool("crouching", false);
                cc.center = new Vector3(cc.center.x,cc.center.y+.47f,cc.center.z);
                cc.height *= 3;
            }
           }
        
    }
    
    IEnumerator sliding(Vector3 direction)
    {

        Debug.Log("Is sliding");
        isSliding = true;
        rb.AddForce(direction * 400);
        yield return new WaitForSeconds(Time.deltaTime*50);
        rb.velocity = new Vector3(0, 0, 0);
        isSliding = false;
        canSlide = false;
        Debug.Log("Done sliding");
    }
    //Player Spawning mechanism
    void Spawn()
    {
        cc.enabled = false;
        transform.localPosition = SpawnPoint;
        currentHealth = maxHealth;
        PlayerDamage?.Invoke(currentHealth, maxHealth);
        cc.enabled = true;
        PlayerDeath?.Invoke(_lives);
    }

   /* private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null)
        {
            body.AddForce(new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z) * 20);
        }
    }*/
    //Get the Input for movement
    void GetInput()
    {
        VInP = Input.GetAxis("Vertical");
        HInP = Input.GetAxis("Horizontal");

    }
    public void playerkilled()
    {
        _lives--;
        Spawn();
        if (_lives <= 0)
        {
            // gm.GameisOver(count);
            GameManager.Instance.GameisOver(count);
        }

    }
 //Deal Damage to player
   public void Damage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth<=0)
        {
            playerkilled(); 
        }
        PlayerDamage?.Invoke(currentHealth, maxHealth);

    }
    //Player death
    public void Kill()
    {
        count++;
        if (KilledEnemy!=null)
        {
            KilledEnemy(count);
        }
    }


}
