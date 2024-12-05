using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadUI : MonoBehaviour
{
    public GameObject fadetoblack;
    public float timer = 1.1f;
    public bool start = true;
    void Start()
    {
        fadetoblack.SetActive(true);
    }

    void Update()
    {
        if (start)
        {
            timer -= Time.deltaTime;
            fadetoblack.GetComponent<Image>().color = new Color(0, 0, 0, timer);
            if(timer <= 0)
            {
                start = false;
            }
        }
        
    }
}
