using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKitScript : ItemPickupScript
{
    public enum size
    {
        small,
        medium,
        large
    }
    public size healthKitSize;
    public float health;
    public GameObject medpackModel;

    public void Start()
    {
        if (healthKitSize == size.small)
        {
            health = 25f;
        }
        else if (healthKitSize == size.medium)
        {
            health = 50f;
        }
        else if (healthKitSize == size.large)
        {
            health = 99f;
        }
    }

    public override void CollectScript(Collider other)
    {
        //add an overlay or smth to make this more interesting
        other.GetComponent<PlayerScript>().HP += health;

    }
}
