using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PistolAnimationScript : MonoBehaviour
{
    //dear god help me
    public Animator gun;
    public bool empty, fire, reload,eject;
    public float timer;
    public GameObject gunParent,shell;
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
            if(timer > 2.35f)
            {
                gun.SetBool("Reload", false);
                gun.SetBool("MEmpty", false);
                timer = 0;
                reload = false;
            }
            //other reloading stuff
        }
        else if (fire && !reload && !empty)
        {
            if(gunParent.GetComponent<GunScript>().pistolMag == 0)
            {
                gun.SetBool("MEmpty", true);
            }
            timer += Time.deltaTime;
            gun.SetBool("Fire", true);
            if(eject == true)
            {
                eject = false;
                var ejectedShell = Instantiate(shell);
                ejectedShell.transform.position = this.transform.position;
                ejectedShell.transform.rotation = this.transform.rotation;
                Vector3 rotShell = new Vector3(1, 3, 0);
                ejectedShell.GetComponent<Shell>().spawnRot = rotShell;
            }
            if (timer > .5f)
            {
                gun.SetBool("Fire", false);
                timer = 0;
                fire = false;
                eject = true;
            }
            
        }
    }
}
