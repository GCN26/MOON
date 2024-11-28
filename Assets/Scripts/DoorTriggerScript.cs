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

    public void Start()
    {
        startYPos = this.transform.position.y;
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
        }
    }

    public void Update()
    {
        if (playerTouchedTrigger) {
            Door.transform.position = Vector3.Lerp(Door.transform.position,new Vector3(Door.transform.position.x, startYPos + 3, Door.transform.position.z),Time.deltaTime);
        }
    }
}
