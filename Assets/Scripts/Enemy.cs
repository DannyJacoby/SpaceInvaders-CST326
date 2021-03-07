﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{

    public int myValue;
    private GameObject gm;
    private UI_Manager m_UIManager;
    
    private void Awake()
    {
        gm = GameObject.FindWithTag("UI_Manager");
        m_UIManager = gm.GetComponent<UI_Manager>();
    }

    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Debug.Log("Ouch!");
            // m_UIManager.UpdateCurrentScore(myValue);
            Destroy(gameObject);
        }
    }
}
