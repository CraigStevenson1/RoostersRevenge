using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class storyController : MonoBehaviour
{
    [SerializeField] AudioSource narration;
    [SerializeField] int nextLevelSceneNum;
    // Start is called before the first frame update
    

    public void skip()
    {
        //narration.Stop();
        SceneManager.LoadScene(nextLevelSceneNum);

    }

    private void Update()
    {
        if (!narration.isPlaying)
        {
            SceneManager.LoadScene(nextLevelSceneNum);
        }
    }

}
