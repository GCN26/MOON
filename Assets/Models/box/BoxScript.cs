using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public Animator boxAnimator;
    public bool OwIGotHitSoNowIBreakOpenAndGoAway;
    public float timer;
    public AudioSource source;
    public AudioClip boxSound;
    public bool playsound = true;

    void Start()
    {
        source.clip = boxSound;
    }

    // Update is called once per frame
    void Update()
    {
        source.volume = PlayerSaveSettings.SFXVolume * PlayerSaveSettings.masterVolume;
        if (Time.timeScale > 0)
        {
            source.UnPause();
            if (OwIGotHitSoNowIBreakOpenAndGoAway)
            {
                if (playsound)
                {
                    source.Play();
                    playsound = false;
                }
                boxAnimator.SetBool("Shot", true);
                timer += Time.deltaTime;
                if (timer > 1.5f)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        else
        {
            source.Pause();
        }
    }
}
