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

    void Start()
    {
        
    }
    public void Update()
    {
        DespawnTimer += Time.deltaTime;
        if(DespawnTimer >= DespawnTarget)
        {
            Destroy(gameObject);
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
