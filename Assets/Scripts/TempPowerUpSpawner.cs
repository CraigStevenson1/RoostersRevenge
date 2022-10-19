using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPowerUpSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> powerUps;
    private Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("LevelController").GetComponent<Controller>();
    }

    public void spawnPowerUp(int projectileRanking, Vector2 worldSpawnLocCords)
    {
        int lowerBound = 0;
        switch (projectileRanking)
        {
            case 1:
                lowerBound = 0;
                break;
            case 2:
                lowerBound = 10;
                break;

            case 3:
                lowerBound = 20;
                break;
        }

        int roll = Random.Range(lowerBound, 100);

        if(roll < 70)
        {
            Instantiate(powerUps[0], worldSpawnLocCords, Quaternion.identity);
            controller.enableDamagePowerUp();
            //controller.unoReverse();
            //powerUpOne
        }

        else if(roll > 70 && roll < 90)
        {
            Instantiate(powerUps[1], worldSpawnLocCords, Quaternion.identity);
            controller.enableShieldPowerUp();
            //powerup2
        }

        else
        {
            Instantiate(powerUps[2], worldSpawnLocCords, Quaternion.identity);
            controller.unoReverse();
            //powerup3
        }
    }
    
}
