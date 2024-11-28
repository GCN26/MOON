using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMarkDebug : MonoBehaviour
{
    public float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            Destroy(gameObject);
        }
    }
}
