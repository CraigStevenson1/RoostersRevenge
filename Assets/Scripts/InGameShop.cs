using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InGameShop : MonoBehaviour
{
    Controller controller;
    [SerializeField] int dmgPowerUpPrice;
    [SerializeField] int lackeyPrice;
    [SerializeField] int permanentDmgBoostNumber;
    [SerializeField] float lacketAtkInterval;
    [SerializeField] float intervalDecrement;
    [SerializeField] TMP_Text spoilsTotal;
    [SerializeField] int healthUpgradePrice;
    [SerializeField] int healthIncrease;
    [SerializeField] int totalDmgUpgrades;
    [SerializeField] int totalLackeyUpgrades;
    [SerializeField] int totalHealthUpgrades;
    [SerializeField] TMP_Text dmgProg;
    [SerializeField] TMP_Text lackeyProg;
    [SerializeField] TMP_Text hpProg;
    private string scoreKey;
    private string permanentAtkBoostKey;
    private string levelScoreKey;
    private string intervalKey;
    private string hpKey;
    private string currentHPUpgradesKey;
    private string currentLackeyUpgradesKey;
    private string currentDmgUpgradesKey;

    // Start is called before the first frame update
    void Start()
    {
        hpKey = "hp";
        controller = GameObject.Find("LevelController").GetComponent<Controller>();
        scoreKey = "score";
        permanentAtkBoostKey = "dmg";
        levelScoreKey = "lscore";
        currentDmgUpgradesKey = "dmgUpgrades";
        currentHPUpgradesKey = "hpUpgrades";
        currentLackeyUpgradesKey = "lackeyUpgrades";
        float totalSpoils = PlayerPrefs.GetFloat(scoreKey) + PlayerPrefs.GetFloat(levelScoreKey);
        spoilsTotal.text = "Total Spoils: " + totalSpoils;
        intervalKey = "intKey";
        Debug.Log("called");
        
        updateDmgUI();
        updateHpUI();
        updateLackeyUI();

    }

    private void OnEnable()
    {
        float totalSpoils = PlayerPrefs.GetFloat(scoreKey) + PlayerPrefs.GetFloat(levelScoreKey);
        spoilsTotal.text = "Total Spoils: " + totalSpoils;
        updateDmgUI();
        updateHpUI();
        updateLackeyUI();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void tabOneButton()
    {
        if (hasEnoughFunds(dmgPowerUpPrice) && PlayerPrefs.GetInt(currentDmgUpgradesKey) < totalDmgUpgrades)
        {
            int currentUpgrade = PlayerPrefs.GetInt(currentDmgUpgradesKey);
            int newUpgradeVal = currentUpgrade + 1;
            PlayerPrefs.SetInt(currentDmgUpgradesKey, newUpgradeVal);

            charge(dmgPowerUpPrice);

            if (PlayerPrefs.HasKey(permanentAtkBoostKey))
            {
                int currentVal = PlayerPrefs.GetInt(permanentAtkBoostKey);
                int newVal = currentVal + permanentDmgBoostNumber;
                PlayerPrefs.SetInt(permanentAtkBoostKey, newVal);
            }

            else
            {
                PlayerPrefs.SetInt(permanentAtkBoostKey, 1);
            }

            updateDmgUI();
            
        }

    
    }

    public void tabTwoButton()
    {
        if (hasEnoughFunds(lackeyPrice) && PlayerPrefs.GetInt(currentLackeyUpgradesKey) < totalLackeyUpgrades)
        {
            int currentUpgrade = PlayerPrefs.GetInt(currentLackeyUpgradesKey);
            int newVal = currentUpgrade + 1;
            Debug.Log(newVal);
            PlayerPrefs.SetInt(currentLackeyUpgradesKey, newVal);

            if (!PlayerPrefs.HasKey(intervalKey))
            {
                controller.createLackey();
                PlayerPrefs.SetFloat(intervalKey, lacketAtkInterval);
                charge(lackeyPrice);
            }
            else
            {
                charge(lackeyPrice);
                float currentVal = PlayerPrefs.GetFloat(intervalKey);
                currentVal -= intervalDecrement;

                if (currentVal < 0)
                {
                    PlayerPrefs.SetFloat(intervalKey, 0f);
                }
                else
                {
                    PlayerPrefs.SetFloat(intervalKey, currentVal);

                }
                }

            updateLackeyUI();
            
        }
    }

    public void tabThreeButton()
    {
        if (hasEnoughFunds(healthUpgradePrice) && PlayerPrefs.GetInt(currentHPUpgradesKey) < totalHealthUpgrades)
        {
            charge(healthUpgradePrice);
            updateHP();

            int currentUpgrade = PlayerPrefs.GetInt(currentHPUpgradesKey);
            int newVal = currentUpgrade + 1;
            PlayerPrefs.SetInt(currentHPUpgradesKey, newVal);
            updateHpUI();
        }
    }

    public void closeButton()
    {
        gameObject.SetActive(false);
        controller.unpause();
    }

    private void updateHP()
    {
        if (!PlayerPrefs.HasKey(hpKey))
        {
            PlayerPrefs.SetInt(hpKey, 0);
        }
        int currentHP = PlayerPrefs.GetInt(hpKey);
        int newHP = currentHP + healthIncrease;
        PlayerPrefs.SetInt(hpKey, newHP);
        controller.healPlayer(healthIncrease);
    }

    private bool hasEnoughFunds(int amount)
    {
        float currentVal = PlayerPrefs.GetFloat(scoreKey);
        float currentLevelVal = PlayerPrefs.GetFloat(levelScoreKey);

        if (currentVal + currentLevelVal  - amount < 0)
        {
            return false;
        }

        return true;
    }

    private void charge(int amount)
    {
        float currentVal = PlayerPrefs.GetFloat(scoreKey);
        float currentLevelVal = PlayerPrefs.GetFloat(levelScoreKey);

        if (currentVal - amount < 0)
        {
            float surplus = currentVal - amount;
            currentLevelVal += surplus;
            PlayerPrefs.SetFloat(scoreKey, 0);
            PlayerPrefs.SetFloat(levelScoreKey, currentLevelVal);
            float totalSpoils = PlayerPrefs.GetFloat(scoreKey) + PlayerPrefs.GetFloat(levelScoreKey);
            spoilsTotal.text = "Total Spoils: " + totalSpoils;
            controller.updateScoreAfterPurchase();

        }

        else
        {
            float newVal = currentVal - amount;
            PlayerPrefs.SetFloat(scoreKey, newVal);
            float totalSpoils = PlayerPrefs.GetFloat(scoreKey) + PlayerPrefs.GetFloat(levelScoreKey);
            spoilsTotal.text = "Total Spoils: " + totalSpoils;
        }

    }

    private void updateLackeyUI()
    {

        if (PlayerPrefs.HasKey(currentLackeyUpgradesKey))
        {
            int currentVal = PlayerPrefs.GetInt(currentLackeyUpgradesKey);
            lackeyProg.text = currentVal + "/" + totalLackeyUpgrades;
        }

        else
        {
            PlayerPrefs.SetInt(currentLackeyUpgradesKey, 0);
        }
    }

    private void updateHpUI()
    {
        if (PlayerPrefs.HasKey(currentHPUpgradesKey))
        {
            int currentVal = PlayerPrefs.GetInt(currentHPUpgradesKey);
            hpProg.text = currentVal + "/" + totalHealthUpgrades;
        }
        else
        {
            PlayerPrefs.SetInt(currentHPUpgradesKey, 0);

        }
    }

    private void updateDmgUI()
    {

        if (PlayerPrefs.HasKey(currentDmgUpgradesKey)){
            int currentVal = PlayerPrefs.GetInt(currentDmgUpgradesKey);
            dmgProg.text = currentVal + "/" + totalDmgUpgrades;
        }
        else
        {
            PlayerPrefs.SetInt(currentDmgUpgradesKey, 0);

        }
    }
}
