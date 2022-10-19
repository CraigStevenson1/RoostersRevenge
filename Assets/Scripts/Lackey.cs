using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lackey : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int attackDmg;
    private float atkInterval;
    private string intervalKey;
    private Controller controller;
    void Start()
    {
        intervalKey = "intKey";
        atkInterval = PlayerPrefs.GetFloat(intervalKey);
        controller = GameObject.Find("LevelController").GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        updateInterval();
    }
    
    private void updateInterval()
    {
        atkInterval -= Time.deltaTime;
        if(atkInterval <= 0)
        {
            attack();
            atkInterval = PlayerPrefs.GetFloat(intervalKey);
        }
    }

    private void attack()
    {
        controller.reduceEnemyHP(attackDmg);
    }
}
