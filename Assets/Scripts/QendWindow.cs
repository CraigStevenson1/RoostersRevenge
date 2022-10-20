using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class QendWindow : MonoBehaviour
{

    [SerializeField] TMP_Text totalScoreAccumulated;
    private string scoreKey;

    private void Start()
    {
        scoreKey = "score";
    }
    // Start is called before the first frame update

    private void OnEnable()
    {
        scoreKey = "score";
        totalScoreAccumulated.text = "Total Spoils Accumulated: " + PlayerPrefs.GetInt(scoreKey);
    }
    public void returnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
