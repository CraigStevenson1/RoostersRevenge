using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUtils : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int startHP;
    [SerializeField] Controller controller;
    [SerializeField] GameObject shieldPrefab;
    private bool shield;
    private int atkPowerIncrease;
    private float powerUpTimer;
    private float shieldPowerUpTimer;
    private GameObject currentActiveShieldModel;

    private void Start()
    {
        controller.updatePlayerHP("PlayerHP: " + startHP.ToString());
        shield = false;
        atkPowerIncrease = 0;
        powerUpTimer = 0;
        shieldPowerUpTimer = 0;
    }

    public void takeProjectileDmg(int damage)
    {
        
        startHP -= damage;
        controller.updatePlayerHP("PlayerHP: " + startHP.ToString());
    }

    public void damageEnemy()
    {
        controller.reduceEnemyHP(1 + atkPowerIncrease);
    }

    public void enableDamagePowerUp()
    {
        Debug.Log("dmg powerup activated");
        atkPowerIncrease = 5;
        powerUpTimer = 5f;
    }

    public void enableShieldPowerUp()
    {
        Debug.Log("shield powerup activated");
        shield = true;
        currentActiveShieldModel = Instantiate(shieldPrefab, gameObject.transform.position, Quaternion.identity);
        shieldPowerUpTimer = 5f;
    }

    private void decrementDmgPowerUpTimer()
    {
        if (powerUpTimer > 0)
        {
            powerUpTimer -= Time.deltaTime;
        }

        else
        {
            powerUpTimer = 0;
            atkPowerIncrease = 0;
        }
    }

    private void decrementShieldTimer()
    {
        if (shieldPowerUpTimer > 0)
        {
            shieldPowerUpTimer -= Time.deltaTime;
        }

        else
        {
            shieldPowerUpTimer = 0;
            shield = false;
            currentActiveShieldModel.SetActive(false);
        }
    }

    private void Update()
    {
        if (atkPowerIncrease > 0)
        {
            decrementDmgPowerUpTimer();
        }

        if (shield)
        {
            decrementShieldTimer();
        }
        
    }
}
