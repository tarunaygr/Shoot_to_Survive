using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private static UI_Manager _instance;
    public static UI_Manager Instance
    {
        get
        {
            if (_instance==null)
            {
                GameObject go = new GameObject("UI Manager");
                go.AddComponent<UI_Manager>();
            }
            return _instance;
        }
    }
    
    public TextMeshProUGUI kill_count,Lives_count, FinalScoreText;
    public SimpleHealthBar healthBar;
    public Animator crosshair,GameOverFadeOut;
    public GameObject PausePanel,Crosshair,GameOverPanel;
    public CharacterController cc;
    public GameObject Prompt_text;
    private void Start()
    {
        RemovePromptText();
    }
    private void Awake()
    {
        _instance = this;
        Player.PlayerDamage += UpdateHealth;
        Player.KilledEnemy += UpdateKillCounter;
        Player.PlayerDeath += UpdateLives;
    }
    // Update is called once per frame
    void Update()
    {
        AnimateCrossHair();
    }
    //Keeping track of the kill counter
    public void UpdateKillCounter(int count)
    {
        kill_count.text = "Kill Count : " + count;
    }
    //Keeping track of the player health
    public void UpdateHealth(float currentHealth,float maxHealth)
    {
        healthBar.UpdateBar(currentHealth, maxHealth);
    }
    //Keep Track of number of lives
    public void UpdateLives(int _lives)
    {
        Lives_count.text = "Lives : " + _lives;
    }
    //Animating the crosshair
    void AnimateCrossHair()
    {
        if (cc.velocity.x!=0 || cc.velocity.y != 0)
        {
            crosshair.SetBool("Walking", true);
        }
        else
        {
            crosshair.SetBool("Walking", false);

        }
    }
 public void PauseControl(bool state)
    {
        PausePanel.SetActive(state);
    }
    public void CrosshairControl(bool state)
    {
        Crosshair.SetActive(state);
    }
    public void GameOverUI(int finalscore)
    {
        GameOverPanel.SetActive(true);
        FinalScoreText.text = "Final Score: " + finalscore;
        GameOverFadeOut.SetTrigger("FadeOut");
    }
    public void ReloadText()
    {
        Prompt_text.SetActive(true);
        Prompt_text.GetComponent<TextMeshProUGUI>().text = "RELOAD";
    }
    public void RemovePromptText()
    {
        Prompt_text.SetActive(false);
    }
}
