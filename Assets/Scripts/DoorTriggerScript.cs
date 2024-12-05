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

    public void Start()
    {
        startYPos = this.transform.position.y;
        if (Direction) directionMultiplier = 1;
        else directionMultiplier = -1;
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
        if (Time.timeScale > 0)
        {
            if (playerTouchedTrigger)
            {
                Door.transform.position = Vector3.Lerp(Door.transform.position, new Vector3(Door.transform.position.x, startYPos + (3*directionMultiplier), Door.transform.position.z), Time.deltaTime);
            }
        }
    }
}
