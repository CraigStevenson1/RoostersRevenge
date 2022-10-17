using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Campaign : MonoBehaviour
{
    public void levelOne()
    {
        Debug.Log("LevelOne pushed");
        SceneManager.LoadScene(1);
    }

    public void levelTwo()
    {
        if (PlayerPrefs.HasKey("leveltwo"))
        {
            Debug.Log("Level Two pushed");
        }
    }

    public void bonusLevelOne()
    {
        if (PlayerPrefs.HasKey("levelthree"))
        {
            Debug.Log("Bonus Level One pushed");
        }
    }

    public void levelThree()
    {
        if (PlayerPrefs.HasKey("levelthree"))
        {
            Debug.Log("Level three pushed");
        }
    }
}
