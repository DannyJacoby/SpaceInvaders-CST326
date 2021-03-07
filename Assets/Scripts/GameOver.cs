using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static bool isPlayerDead = false;
    public static bool allEnemiesDead = false;
    public TextMeshProUGUI gameOverNeg;
    public TextMeshProUGUI gameOverPos;
    public TextMeshProUGUI restartMesg;

    public GameObject gm;
    private UI_Manager UIManager;
    // Start is called before the first frame update
    void Start()
    {
        gameOverNeg.enabled = false;
        gameOverPos.enabled = false;
        restartMesg.enabled = false;
        
        gm = GameObject.FindWithTag("UI_Manager");
        UIManager = gm.GetComponent<UI_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerDead)
        {
            Time.timeScale = 0;
            gameOverNeg.color = Color.red;
            gameOverNeg.enabled = true;
            restartMesg.color = Color.red;
            restartMesg.enabled = true;
            UIManager.UpdateHighScoresFile();
        }
        if (allEnemiesDead)
        {
            Time.timeScale = 0;
            gameOverPos.color = Color.green;
            gameOverPos.enabled = true;
            restartMesg.color = Color.green;
            restartMesg.enabled = true;
            UIManager.UpdateHighScoresFile();
        }

    }
    
}
