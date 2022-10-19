using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.HealthSystemCM;

public class Enemy : MonoBehaviour, IGetHealthSystem
{
    [SerializeField] int enemyHP;
    [SerializeField] Controller controller;
    private HealthSystem hpSys;
    // Start is called before the first frame update
    void Start()
    {
        controller.updateEnemyHP("EnemyHP: " + enemyHP.ToString());
    }

    public void Awake()
    {
        hpSys = new HealthSystem(enemyHP);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHP <= 0)
        {
            death();
        }
    }

    public void reduceHP(int amount)
    {
        
        enemyHP -= amount;

        if(enemyHP <= 0)
        {
            controller.updateEnemyHP("EnemyHP: " + enemyHP.ToString());
            controller.updateEnemyHP("defeated");
        }

        else
        {
            hpSys.Damage(amount);
            controller.updateEnemyHP("EnemyHP: " + enemyHP.ToString());
        }
        

    }

    private void death()
    {
        gameObject.SetActive(false);
    }

    public HealthSystem GetHealthSystem()
    {
        return hpSys;
    }
}
