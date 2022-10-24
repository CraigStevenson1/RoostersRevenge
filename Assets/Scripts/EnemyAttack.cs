using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] List<GameObject> projectiles;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed;
    [SerializeField] float atkInterval;
    private float timeBetweenAtk;
    private bool UnoReverse;
    float UnoReverseTimer;
    void Start()
    {
        timeBetweenAtk = atkInterval;
        UnoReverse = false;
        UnoReverseTimer = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenAtk -= Time.deltaTime;

        if (timeBetweenAtk < 0)
        {
            if (!UnoReverse)
            {
                spawnProjectile();
                timeBetweenAtk = atkInterval;
            }

            else
            {
                spawnProjectileReverse();
                timeBetweenAtk = atkInterval;
            }
        }

        if (UnoReverse)
        {
            UnoReverseTimer -= Time.deltaTime;
            checkUnoReverseTimer();
        }
    }

    void spawnProjectile()
    {
        int side = Random.Range(0, 2);
       
        Vector2 spawnCords = Vector2.zero;

        switch (side)
        {
            case 0:
                spawnCords.x = 0;
                spawnCords.y = Random.Range(0.65f, 1f);
               // Debug.Log(spawnCords);
                break;

            case 1:
                spawnCords.x = Random.Range(0f, 1f);
                spawnCords.y = 1;
                //Debug.Log(spawnCords);
                break;
        }
        Vector3 worldSpawnCords = Camera.main.ViewportToWorldPoint(spawnCords);
        worldSpawnCords.z = 0f;

        Vector3 direction = Vector3.zero;
      
            direction = GameObject.Find("Player").transform.position - worldSpawnCords;
        

       

        GameObject proj = Instantiate(projectile, worldSpawnCords, Quaternion.identity);

        Rigidbody projRb = proj.GetComponent<Rigidbody>();

        projRb.velocity = direction*projectileSpeed;
       // Debug.Log(worldSpawnCords);


    }

    private void spawnProjectileReverse()
    {
        int side = Random.Range(0, 2);

        Vector2 spawnCords = Vector2.zero;

        switch (side)
        {
            case 0:
                spawnCords.x = 1;
                spawnCords.y = Random.Range(0.65f, 1f);
                // Debug.Log(spawnCords);
                break;

            case 1:
                spawnCords.x = Random.Range(0f, 1f);
                spawnCords.y = 1;
                //Debug.Log(spawnCords);
                break;
        }
        Vector3 worldSpawnCords = Camera.main.ViewportToWorldPoint(spawnCords);
        worldSpawnCords.z = 0f;

        Vector3 direction = Vector3.zero;

        direction = GameObject.Find("Enemy").transform.position - worldSpawnCords;




        GameObject proj = Instantiate(projectile, worldSpawnCords, Quaternion.identity);

        Rigidbody projRb = proj.GetComponent<Rigidbody>();

        projRb.velocity = direction*projectileSpeed;
        // Debug.Log(worldSpawnCords);

    }

 /*   private int chooseProjectile()
    {
        int val = Random.Range(0, 100);

        if(val < 50)
        {
            return 0;
        }

        else if(val > 50 && val < 80)
        {
            return 1;
        }


        else
        {
            return 2;
        }
    }*/

    public void UnoReverseActivate()
    {
        UnoReverse = true;
    }

    private void checkUnoReverseTimer()
    {
        if(UnoReverseTimer <= 0)
        {
            UnoReverse = false;
            UnoReverseTimer = 10f;
        }
    }
}
