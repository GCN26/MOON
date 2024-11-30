using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PistolAnimationScript : MonoBehaviour
{
    //dear god help me
    public Animator gun;
    public bool empty, fire, reload;
    public float timer;
    //add shell like shotgun
    public void Update()
    {
        if (!reload && !fire)
        {
            //if (empty)
            //{
            //    gun.SetBool("MEmpty", true);
            //}
            //else
            //{
            //    gun.SetBool("MEmpty", false);
            //}
            //make this code work later on im too tired
            //link this to pistol actions in gun script
        }
        else if (reload && !fire)
        {
            timer += Time.deltaTime;
            gun.SetBool("Reload", true);
            empty = false;
            if(timer > .4f)
            {
                gun.SetBool("Reload", false);
                timer = 0;
                reload = false;
            }
            //other reloading stuff
        }
        else if (fire && !reload && !empty)
        {
            //lose ammo
            timer += Time.deltaTime;
            gun.SetBool("Fire", true);
            //if ammo - 1 = 0, set empty as well
            if (timer > .4f)
            {
                gun.SetBool("Fire", false);
                timer = 0;
                fire = false;
            }
            
        }
    }
}
