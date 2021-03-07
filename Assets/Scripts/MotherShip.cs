using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class MotherShip : MonoBehaviour
{
    public GameObject bullet;
    public Transform shottingOffset;
    public float bulletDeath;
    private Transform enemyHolder;
    
    public float speed;
    public float fireRate = 0.997f;

    public float maxBound, minBound;
    
    public GameObject m_Enemy_1;
    public GameObject m_Enemy_2;
    public GameObject m_Enemy_3;
    public GameObject m_Enemy_4;

    public int numberOfAliens = 4;
    private int numberOfAliensLeft;
    // Start is called before the first frame update
    void Start()
    {
        numberOfAliensLeft = numberOfAliens;
        InvokeRepeating("MoveEnemy", 0.1f, 0.3f);
        enemyHolder = GetComponent<Transform>();
        InstantiateEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveEnemy()
    {
        enemyHolder.position += Vector3.right * speed;
        foreach (Transform enemy in enemyHolder)
        {
            if (enemy.position.x < -10.5f || enemy.position.x > 10.5f)
            {
                speed = -speed;
                enemyHolder.position += Vector3.down * 0.5f;
                return;
            }
            
            // EnemyBulletController ie Fire Bullet goes here?

            if (enemy.position.y <= -4)
            {
                
            }
        }
        
    }
    
    // TODO Expand this function
    public void InstantiateEnemies()
    {
        GameObject ToSpawnEnemy1 = GameObject.Instantiate(m_Enemy_1, transform);
        ToSpawnEnemy1.transform.localPosition = new Vector3(-6f, 2f, 0f);
        GameObject ToSpawnEnemy2 = GameObject.Instantiate(m_Enemy_2, transform);
        ToSpawnEnemy2.transform.localPosition = new Vector3(-2f, 2f, 0f);
        GameObject ToSpawnEnemy3 = GameObject.Instantiate(m_Enemy_3, transform);
        ToSpawnEnemy3.transform.localPosition = new Vector3(2f, 2f, 0f);
        GameObject ToSpawnEnemy4 = GameObject.Instantiate(m_Enemy_4, transform);
        ToSpawnEnemy4.transform.localPosition = new Vector3(6f, 2f, 0f);
    }
}
