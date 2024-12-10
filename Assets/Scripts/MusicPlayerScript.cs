using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{
    public AudioSource MusicSource;
    public AudioClip MusicToBePlayed;
    public GameObject player;
    void Start()
    {
        MusicSource.clip = MusicToBePlayed;
        MusicSource.Play();
    }
    void Update()
    {
        MusicSource.volume = PlayerSaveSettings.musicVolume * PlayerSaveSettings.masterVolume;
        if (player != null)
        {
            if (player.GetComponent<PlayerScript>().dead == true)
            {
                MusicSource.Stop();
            }
        }
    }
}
