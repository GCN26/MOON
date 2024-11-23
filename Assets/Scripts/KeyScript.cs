using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : ItemPickupScript
{
    public int keyColor;
    public override void CollectScript(Collider other)
    {
        other.GetComponent<PlayerScript>().keyCollect(keyColor);
    }
}
