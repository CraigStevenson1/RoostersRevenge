using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField] int min;
    [SerializeField] float sec;
    [SerializeField] TMP_Text timer;
    [SerializeField] Controller controller;
    private float totalSeconds;
    // Start is called before the first frame update
    void Start()
    {
        totalSeconds = min * 60 + sec;
      /*  Debug.Log(totalSeconds);
        Debug.Log((int)(totalSeconds) / 60);
        Debug.Log(totalSeconds % 60);*/
    }

    // Update is called once per frame
    void Update()
    {
        if (totalSeconds > 0)
        {
            totalSeconds -= Time.deltaTime;

            controller.updateTime(((int)totalSeconds / 60).ToString() + ":" + (Mathf.Round(totalSeconds % 60)).ToString());
        }

        else
        {
            controller.endLevel();
        }
    }

    public void addTime(float time)
    {
        totalSeconds += time;
    }
}
