using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public float timeRemaining;
    public float totalTime = 5f;
    public static bool _timerIsRunning;
    
    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = totalTime;
        _timerIsRunning = true;
    }

    void Awake()
    {
        timeRemaining = totalTime;
        _timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timeRemaining + " - " + Time.deltaTime);
        if (timeRemaining > 0 && _timerIsRunning)
        {
            timeRemaining -= (Time.deltaTime + 0.1f);
            Debug.Log("TIME " + Math.Floor(timeRemaining));
            
        }
        else if(_timerIsRunning)
        {
            timeRemaining = 0;
            _timerIsRunning = false;
        }

        if (!_timerIsRunning)
        {
            SceneManager.LoadScene("SpaceInvadersUI");
        }
    }
    
    
}
