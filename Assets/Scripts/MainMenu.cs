using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMP_Text spoilsOfWar;
    private string scoreKey;
    void Start()
    {
        scoreKey = "score";
        spoilsOfWar.text = "Spoils of War: " + PlayerPrefs.GetInt(scoreKey);
    }

    public void campaignButton()
    {
        Debug.Log("Campaign Button Pushed");
        SceneManager.LoadScene(2);
    }

    public void shopButton()
    {
        PlayerPrefs.DeleteAll();
        spoilsOfWar.text = "Spoils of War: " + 0;
    }
}
