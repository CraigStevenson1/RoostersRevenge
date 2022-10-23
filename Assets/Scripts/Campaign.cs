using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Campaign : MonoBehaviour
{
    public void levelOne()
    {
        Debug.Log("LevelOne pushed");
        SceneManager.LoadScene(7);
        //SceneManager.LoadScene(1);
    }

    public void BonusLevelOne()
    {
        //PlayerPrefs.SetInt("leveltwo", 0);
        if (PlayerPrefs.HasKey("leveltwo"))
        {
            SceneManager.LoadScene(3);
            Debug.Log("Level Two pushed");
        }
    }

    public void LevelTwo()
    {

        SceneManager.LoadScene(8);
        Debug.Log("Bonus Level One pushed");
        
    }

    public void BonusLevelTwo()
    {
        //PlayerPrefs.SetInt("levelthree", 0);

        if (PlayerPrefs.HasKey("levelthree"))
        {
            SceneManager.LoadScene(5);
        }
    }

    public void LevelThree()
    {
        
        SceneManager.LoadScene(9);
        Debug.Log("Level three pushed");
        
    }
}
