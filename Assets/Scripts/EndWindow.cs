using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndWindow : MonoBehaviour
{

    Controller controller;
    [SerializeField] TMP_Text scoreSection;
    private string scoreKey;
    // Start is called before the first frame update
    void Start()
    {
        scoreKey = "score";
        controller = GameObject.Find("LevelController").GetComponent<Controller>();
        scoreSection.text = "Score Accumulated: " + PlayerPrefs.GetInt(scoreKey);
       
    }   
    
    public void MainMenuButton()
    {
        controller.returnToMainMenu();
    }

   
}
