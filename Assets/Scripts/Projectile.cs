using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    [SerializeField] int projectileDamage;
    [SerializeField] int projectileClickCount;
    [SerializeField] int projectileRank;
    private GameObject hitSFX;

    TempPowerUpSpawner spawner;

    private void Start()
    {
        spawner = GameObject.Find("TempPowerUpSpawner").GetComponent<TempPowerUpSpawner>();
        hitSFX = GameObject.Find("hitSfx");
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerUtils playerUt = other.GetComponent<PlayerUtils>();
        Enemy enemy = other.GetComponent<Enemy>();
        //Debug.Log("trigger");
        Debug.Log(other.tag);
        if (playerUt != null)
        {
            playerUt.takeProjectileDmg(projectileDamage);
            gameObject.SetActive(false);
            hitSFX.GetComponent<hitSFX>().playerHitSound();
        }
        
        else if(other.tag == "shield")
        {
            
            gameObject.SetActive(false);
        }

        else if(enemy != null)
        {
            enemy.reduceHP(projectileDamage);
            gameObject.SetActive(false);
        }
    }

    public void reduceClickCount()
    {
        projectileClickCount -= 1;

        if(projectileClickCount <= 0)
        {
            Debug.Log(gameObject.transform.position);
            spawner.spawnPowerUp(projectileRank, (gameObject.transform.position));
            gameObject.SetActive(false);
        }
    }
}
