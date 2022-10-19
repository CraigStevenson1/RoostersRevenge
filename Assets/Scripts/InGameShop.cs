using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InGameShop : MonoBehaviour
{
    Controller controller;
    [SerializeField] int dmgPowerUpPrice;
    [SerializeField] int lackeyPrice;
    [SerializeField] int otherUpradgePrice;
    [SerializeField] int permanentDmgBoostNumber;
    [SerializeField] int featherDmgUpgrdadeNumber;
    [SerializeField] float lacketAtkInterval;
    [SerializeField] float intervalDecrement;
    [SerializeField] TMP_Text spoilsTotal;
    private string scoreKey;
    private string permanentAtkBoostKey;
    private string levelScoreKey;
    private string intervalKey;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("LevelController").GetComponent<Controller>();
        scoreKey = "score";
        permanentAtkBoostKey = "dmg";
        levelScoreKey = "lscore";
        int totalSpoils = PlayerPrefs.GetInt(scoreKey) + PlayerPrefs.GetInt(levelScoreKey);
        spoilsTotal.text = "Total Spoils: " + totalSpoils;
        intervalKey = "intKey";
        Debug.Log("called");
    }

    private void OnEnable()
    {
        int totalSpoils = PlayerPrefs.GetInt(scoreKey) + PlayerPrefs.GetInt(levelScoreKey);
        spoilsTotal.text = "Total Spoils: " + totalSpoils;
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
            
        }
    }

    public void tabTwoButton()
    {
        if (hasEnoughFunds(lackeyPrice))
        {

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
        int currentLevelVal = PlayerPrefs.GetInt(levelScoreKey);

        if (currentVal + currentLevelVal  - amount < 0)
        {
            return false;
        }

        return true;
    }

    private void charge(int amount)
    {
        int currentVal = PlayerPrefs.GetInt(scoreKey);
        int currentLevelVal = PlayerPrefs.GetInt(levelScoreKey);

        if (currentVal - amount < 0)
        {
            int surplus = currentVal - amount;
            currentLevelVal += surplus;
            PlayerPrefs.SetInt(scoreKey, 0);
            PlayerPrefs.SetInt(levelScoreKey, currentLevelVal);
            int totalSpoils = PlayerPrefs.GetInt(scoreKey) + PlayerPrefs.GetInt(levelScoreKey);
            spoilsTotal.text = "Total Spoils: " + totalSpoils;
            controller.updateScoreAfterPurchase();

        }

        else
        {
            int newVal = currentVal - amount;
            PlayerPrefs.SetInt(scoreKey, newVal);
            int totalSpoils = PlayerPrefs.GetInt(scoreKey) + PlayerPrefs.GetInt(levelScoreKey);
            spoilsTotal.text = "Total Spoils: " + totalSpoils;
        }

    }
}
