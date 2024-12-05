using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static GunScript;

public class KnifeAnimationScript : MonoBehaviour
{
    public Animator animator;
    public GameObject gunParent;
    public float cooldownTimer;
    public bool fire;
    

    void Update()
    {
        if (fire)
        {
            cooldownTimer += Time.deltaTime;
            animator.SetBool("Fire", true);
            if (cooldownTimer > .45f)
            {
                animator.SetBool("Fire", false);
                cooldownTimer = 0;
                fire = false;
            }

        }
    }
}
