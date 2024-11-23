using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : ItemPickupScript
{
    public enum key
    {
        red = 0, blue = 1, yellow = 2
    }
    public key keyColor;
    public override void CollectScript(Collider other)
    {
        other.GetComponent<PlayerScript>().keyCollect((int)keyColor);
    }
}
