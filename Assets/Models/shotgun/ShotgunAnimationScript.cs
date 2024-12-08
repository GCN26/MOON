using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAnimationScript : MonoBehaviour
{
    public Animator animator;
    public bool pump,eject;

    public GameObject parentGun;

    public float pumpTimer;
    public GameObject shell;

    private void Update()
    {
        if (pump)
        {
            animator.SetBool("Pump", true);
            pumpTimer += Time.deltaTime;
            if(pumpTimer >= .45 && eject == true)
            {
                eject = false;
                var ejectedShell = Instantiate(shell);
                ejectedShell.transform.position = this.transform.position;
                ejectedShell.transform.rotation = this.transform.rotation;
                Vector3 rotShell = new Vector3(1, 3, 0);
                ejectedShell.GetComponent<Shell>().spawnRot = rotShell;
                
            }
            if (pumpTimer > .75)
            {
                animator.SetBool("Pump", false);
                parentGun.GetComponent<GunScript>().PumpSource.Play();
                pump = false;
                pumpTimer = 0;
                eject = true;
            }
        }
    }
}
