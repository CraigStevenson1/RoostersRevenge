using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitSFX : MonoBehaviour
{
    [SerializeField] AudioSource src;
    [SerializeField] AudioSource powerUpSound;
    // Start is called before the first frame update
    public void playerHitSound()
    {
        src.Play();
    }

    public void playPowerUp()
    {
        powerUpSound.Play();
    }
}
