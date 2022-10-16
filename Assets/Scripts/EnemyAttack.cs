using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> projectiles;
    private float timeBetweenAtk;
    void Start()
    {
        timeBetweenAtk = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenAtk -= Time.deltaTime;

        if (timeBetweenAtk < 0)
        {
            spawnProjectile();
            timeBetweenAtk = 2f;
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
                spawnCords.y = Random.Range(0.5f, 1f);
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
        Vector3 direction = GameObject.Find("Player").transform.position - worldSpawnCords;

        GameObject proj = Instantiate(projectiles[chooseProjectile()], worldSpawnCords, Quaternion.identity);

        Rigidbody projRb = proj.GetComponent<Rigidbody>();

        projRb.velocity = direction;
       // Debug.Log(worldSpawnCords);


    }

    private int chooseProjectile()
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
    }
}
