using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] int enemyHP;
    [SerializeField] Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        controller.updateEnemyHP("EnemyHP: " + enemyHP.ToString());
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
            controller.updateEnemyHP("EnemyHP: " + enemyHP.ToString());
        }
        

    }

    private void death()
    {
        gameObject.SetActive(false);
    }
}
