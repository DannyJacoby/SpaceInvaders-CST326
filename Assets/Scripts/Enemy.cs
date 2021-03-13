using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public bool amINotInScene = false;
    public int myValue;
    private GameObject gm;
    private UI_Manager m_UIManager;

    private AudioSource _audioSource;
    
    public AudioClip deathSound;
    
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (!amINotInScene)
        {
            m_UIManager = gameObject.GetComponentInParent<UI_Manager>();
        }
    }

    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            PlaySound(deathSound);
            // Debug.Log("Ouch!");
            m_UIManager.UpdateCurrentScore(myValue);
            MotherShip.RepeatSpeed -= 0.1f;
            Destroy(gameObject);
        }
    }
    
    private void PlaySound(AudioClip soundClip)
    {
        _audioSource.clip = soundClip;
        _audioSource.Play();
        PauseGame(soundClip.length);
    }
    
    public void PauseGame(float pauseTime)
    {
        StartCoroutine(GamePauser(pauseTime));
    }
    public IEnumerator GamePauser(float pauseTime){
        yield return new WaitForSeconds (pauseTime);
    }
}
