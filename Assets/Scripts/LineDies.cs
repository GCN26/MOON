using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDies : MonoBehaviour
{
    public float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > .1f)
        {
            Destroy(gameObject);
        }
    }
}
