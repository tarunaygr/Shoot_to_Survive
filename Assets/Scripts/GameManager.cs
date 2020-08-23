using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("UI Manager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }
    public static bool isPaused { get; private set; }
    public static bool isGameOver { get; private set; }
    [SerializeField]
    private UI_Manager UIHandle;

    // Start is called before the first frame update

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        Cursor.visible = false;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

    }
    public void Pause()
    {
            isPaused = !isPaused;
            if (isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject _enemy in enemyList)
            {
                _enemy.GetComponent<NavMeshAgent>().isStopped = true;
            }
            }
            else
            {
            GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject _enemy in enemyList)
            {
                _enemy.GetComponent<NavMeshAgent>().isStopped = false;
            }
            Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            //UIHandle.PauseControl(isPaused);
        UI_Manager.Instance.PauseControl(isPaused);
           // UIHandle.CrosshairControl(!isPaused);
        UI_Manager.Instance.CrosshairControl(!isPaused);
        

    }

    public void GameisOver(int score)
    {
        isGameOver = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
        //UIHandle.GameOverUI(score);
        UI_Manager.Instance.GameOverUI(score);
        Debug.Log("GAME OVER");
    }
    public void Restart()
    {
        Player.KilledEnemy = null;
        Player.PlayerDeath = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
