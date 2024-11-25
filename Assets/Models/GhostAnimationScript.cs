using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimationScript : MonoBehaviour
{
    public Animator animator;
    public GameObject parent,model;
    public Material normalEyes,deadEyes,madEyes;

    
    void Update()
    {
        Material[] mats = model.GetComponent<SkinnedMeshRenderer>().materials;

        if (parent.GetComponent<EnemyMovement>().currentState == EnemyMovement.EnemyStates.Chase)
        {
            animator.SetBool("Moving", true);
            mats[1] = madEyes;
            model.GetComponent<SkinnedMeshRenderer>().materials = mats;
        }
        else if(parent.GetComponent<EnemyMovement>().currentState == EnemyMovement.EnemyStates.Idle || parent.GetComponent<EnemyMovement>().currentState == EnemyMovement.EnemyStates.Patrol)
        {
            animator.SetBool("Moving", false);
            mats[1] = normalEyes;
            model.GetComponent<SkinnedMeshRenderer>().materials = mats;
        }
        else if(parent.GetComponent<EnemyMovement>().currentState == EnemyMovement.EnemyStates.Dead)
        {
            animator.SetBool("Dead", true);
            mats[1] = deadEyes;
            model.GetComponent<SkinnedMeshRenderer>().materials = mats;
        }
    }
}
