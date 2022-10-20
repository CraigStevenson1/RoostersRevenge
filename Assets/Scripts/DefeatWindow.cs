using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatWindow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void playAgain()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
