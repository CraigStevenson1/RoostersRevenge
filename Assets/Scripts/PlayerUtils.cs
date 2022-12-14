using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.HealthSystemCM;

public class PlayerUtils : MonoBehaviour, IGetHealthSystem
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
    private int permanentAtkBoost;
    private string permanentAtkBoostKey;
    private HealthSystem playerHpSys;
    private string hpKey;
    

    private void Start()
    {
        hpKey = "hp";
        
        shield = false;
        atkPowerIncrease = 0;
        powerUpTimer = 0;
        shieldPowerUpTimer = 0;
        permanentAtkBoostKey = "dmg";
        hpKey = "hp";

        if (PlayerPrefs.HasKey(permanentAtkBoostKey))
        {
            permanentAtkBoost = PlayerPrefs.GetInt(permanentAtkBoostKey);
        }

    }

    private void Awake()
    {
        playerHpSys = new HealthSystem(startHP);
    }

    private void calculateActualHp()
    {
        int addedHP = PlayerPrefs.GetInt(hpKey);
        int finalHP = addedHP + startHP;
        controller.updatePlayerHP("PlayerHP: " + finalHP.ToString());

    }

    public void takeProjectileDmg(int damage)
    {

        //startHP -= damage;
        playerHpSys.Damage(damage);

        if (playerHpSys.IsDead())
        {
            controller.playerDead();
        }
        //controller.updatePlayerHP("PlayerHP: " + startHP.ToString());
    }

    public void healHP(int amount)
    {
        
        //float currentHeath = playerHpSys.GetHealth();
        //int maxHPIncrease = PlayerPrefs.GetInt(hpKey);
        //Debug.Log(startHP + PlayerPrefs.GetInt(hpKey));
        if (playerHpSys.GetHealth() == playerHpSys.GetHealthMax())
        {
            playerHpSys.SetHealthMax(playerHpSys.GetHealthMax() + PlayerPrefs.GetInt(hpKey), true);
        }

        else
        {
            playerHpSys.SetHealthMax(playerHpSys.GetHealthMax() + PlayerPrefs.GetInt(hpKey), false);
            playerHpSys.Heal(amount);

        }
       // Debug.Log(playerHpSys.GetHealthMax());
    }

    public void damageEnemy()
    {
        if (PlayerPrefs.HasKey(permanentAtkBoostKey))
        {
            permanentAtkBoost = PlayerPrefs.GetInt(permanentAtkBoostKey);
        }
        controller.reduceEnemyHP(1 + atkPowerIncrease + permanentAtkBoost);
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
        if (!shield)
        {
            shield = true;
            //Vector2 shieldSpawnCords = gameObject.transform.position;
            currentActiveShieldModel = Instantiate(shieldPrefab, gameObject.transform.position, Quaternion.identity);
            shieldPowerUpTimer = 5f;
        }

        else
        {
            shieldPowerUpTimer = 5f;
        }
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

    public Vector3 getPlayerPos()
    {
        return gameObject.transform.position; 
    }

    public HealthSystem GetHealthSystem()
    {
        return playerHpSys;
    }
}
