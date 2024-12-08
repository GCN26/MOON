using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorTriggerScript : MonoBehaviour
{
    public GameObject Door;
    public bool playerTouchedTrigger;
    public float startYPos;
    public enum doorColor
    {
        neutral,red,blue,yellow
    }
    public doorColor colorOf;

    [Header("True is up, False is down")]
    public bool Direction;
    private float directionMultiplier;

    public AudioSource source,denyS;
    public AudioClip doorOpen,noKey;
    public bool playSound = true;

    public void Start()
    {
        startYPos = this.transform.position.y;
        if (Direction) directionMultiplier = 1;
        else directionMultiplier = -1;
        source.clip = doorOpen;
        denyS.clip = noKey;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (colorOf == doorColor.neutral)
            {
                playerTouchedTrigger = true;
            }
            else if(colorOf == doorColor.red && other.GetComponent<PlayerScript>().redKey)
            {
                playerTouchedTrigger = true;
            }
            else if (colorOf == doorColor.blue && other.GetComponent<PlayerScript>().blueKey)
            {
                playerTouchedTrigger = true;
            }
            else if (colorOf == doorColor.yellow && other.GetComponent<PlayerScript>().yellowKey)
            {
                playerTouchedTrigger = true;
            }
            else if (colorOf == doorColor.red && !other.GetComponent<PlayerScript>().redKey)
            {
                denyS.Play();
            }
            else if (colorOf == doorColor.blue && !other.GetComponent<PlayerScript>().blueKey)
            {
                denyS.Play();
            }
            else if (colorOf == doorColor.yellow && !other.GetComponent<PlayerScript>().yellowKey)
            {
                denyS.Play();
            }
        }
    }

    public void Update()
    {
        source.volume = PlayerSaveSettings.SFXVolume * PlayerSaveSettings.masterVolume*.65f;
        denyS.volume = PlayerSaveSettings.SFXVolume * PlayerSaveSettings.masterVolume;
        if (Time.timeScale > 0)
        {
            source.UnPause();
            denyS.UnPause();
            if (playerTouchedTrigger)
            {
                if(playSound)
                {
                    playSound = false;
                    source.Play();
                }
                Door.transform.position = Vector3.Lerp(Door.transform.position, new Vector3(Door.transform.position.x, startYPos + (3*directionMultiplier), Door.transform.position.z), Time.deltaTime);
            }
        }
        else
        {
            source.Pause();
            denyS.Pause();
        }
    }
}
