using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeFromGunDies : MonoBehaviour
{
    public float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 30f)
        {
            Destroy(gameObject);
        }
    }
}
