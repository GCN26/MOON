using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectScript(other);
            Destroy(gameObject);
        }
    }
    public virtual void CollectScript(Collider other)
    {
        //Insert collect code
    }
}
