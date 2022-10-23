using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLive : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float timeAlive;
    private GameObject sfx;
    void Start()
    {
        sfx = GameObject.Find("hitSfx");
        sfx.GetComponent<hitSFX>().playPowerUp();
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive -= Time.deltaTime;

        if(timeAlive < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
