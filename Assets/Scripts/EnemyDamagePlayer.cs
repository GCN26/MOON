using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagePlayer : MonoBehaviour
{
    public GameObject Parent;
    public float Damage;

    public void Start()
    {
        if (Parent == null)
        {
            
        }
        else
        {
            Damage = Parent.GetComponent<EnemyMovement>().damage;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        //if timescale is not 0
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerScript>().invulnTimer == 0)
            {
                other.GetComponent<PlayerScript>().invulnTimer = 2;
                other.GetComponent<PlayerScript>().HP -= Damage;
                other.GetComponent<PlayerScript>().HurtSource.Play();
            }
        }
    }
    
}
