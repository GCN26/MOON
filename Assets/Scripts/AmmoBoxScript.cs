using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxScript : ItemPickupScript

{
    public enum size
    {
        small,
        medium,
        large
    }
    public size ammoBoxSize;
    public float multiplier;
    public GameObject ammoModel;

    public void Start()
    {
        if (ammoBoxSize == size.small)
        {
            multiplier = .25f;
        }
        else if (ammoBoxSize == size.medium)
        {
            multiplier = .5f;
        }
        else if (ammoBoxSize == size.large)
        {
            multiplier = 1f;
        }
    }

    public override void CollectScript(Collider other)
    {
        other.GetComponent<PlayerScript>().gunObject.GetComponent<GunScript>().ammoPickup(multiplier);
        other.GetComponent<PlayerScript>().AmmoSource.Play();
    }
}
