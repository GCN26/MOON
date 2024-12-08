using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public Rigidbody rb;
    public bool spawned;
    public Vector3 spawnRot;
    public GameObject child;

    public float DespawnTimer;
    public float DespawnTarget;

    public AudioSource source;
    public AudioClip drop;
    public AudioClip brass1, brass2, brass3;
    public bool isBrass;
    

    void Start()
    {
        if (isBrass)
        {
            int soundNo = Random.Range(1, 4);
            if (soundNo == 1)
            {
                source.clip = brass1;
            }
            else if (soundNo == 2)
            {
                source.clip = brass2;
            }
            else
            {
                source.clip = brass3;
            }
        }
        else
        {
            source.clip = drop;
        }
        source.Play();
    }
    public void Update()
    {
        if (Time.timeScale == 0)
        {
            source.Pause();
        }
        else
        {
            source.UnPause();
            source.volume = PlayerSaveSettings.SFXVolume * PlayerSaveSettings.masterVolume;
            DespawnTimer += Time.deltaTime;
            if (DespawnTimer >= DespawnTarget)
            {
                Destroy(gameObject);
            }
        }
    }
    void FixedUpdate()
    {
        if (spawned)
        {
            for (int i = 0; i < 10; i++) {
                rb.AddForce(-10f*transform.forward);
                rb.AddForce(20f * transform.up);
            }
            //spin child
            
            spawned = false;
        }
    }
}
