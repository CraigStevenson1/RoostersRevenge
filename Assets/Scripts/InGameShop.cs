using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameShop : MonoBehaviour
{
    Controller controller;
    [SerializeField] int dmgPowerUpPrice;
    [SerializeField] int fanOfFeathersPowerUpPrice;
    [SerializeField] int otherUpradgePrice;
    string scoreKey;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("LevelController").GetComponent<Controller>();
        scoreKey = "score";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tabOneButton()
    {
        if (hasEnoughFunds(dmgPowerUpPrice))
        {
            charge(dmgPowerUpPrice);
        }
    }

    public void tabTwoButton()
    {
        if (hasEnoughFunds(fanOfFeathersPowerUpPrice))
        {
            charge(dmgPowerUpPrice);
        }
    }

    public void tabThreeButton()
    {
        if (hasEnoughFunds(otherUpradgePrice))
        {
            charge(dmgPowerUpPrice);
        }
    }

    public void closeButton()
    {
        gameObject.SetActive(false);
        controller.unpause();
    }

    private bool hasEnoughFunds(int amount)
    {
        int currentVal = PlayerPrefs.GetInt(scoreKey);

        if(currentVal - amount < 0)
        {
            return false;
        }

        return true;
    }

    private void charge(int amount)
    {
        int currentVal = PlayerPrefs.GetInt(scoreKey);
        int newVal = currentVal - amount;
        PlayerPrefs.SetInt(scoreKey, newVal);
    }
}
