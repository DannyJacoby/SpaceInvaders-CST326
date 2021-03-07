using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public GameObject bullet;
  public Transform shottingOffset;
  public float bulletDeath;
  
  private Transform player;
  public float speed;
  public float maxBound, minBound;

  void Start()
  {
    player = GetComponent<Transform>();
    
  }
  
    // Update is called once per frame
    void Update()
    {

      // Bounds Binding
      float h = Input.GetAxis("Horizontal");
      if (player.position.x < minBound && h < 0)
      {
        h = 0;
      } else if (player.position.x > maxBound && h > 0)
      {
        h = 0;
      }

      player.position += (h * speed) * Vector3.right;

      if (Input.GetKeyDown(KeyCode.Space))
      {
        GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
        shot.GetComponent<Bullet>().amPlayerBullet = true;
        // Debug.Log("Bang!");

        Destroy(shot, bulletDeath);

      }
    }

    private void OnCollisionEnter(Collision other)
    {
      if (other.gameObject.CompareTag("Enemy"))
      {
        // bad things happen
      }
    }
}
