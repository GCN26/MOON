using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxSpinButMuchSlowerThanAmmoAndHealth : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, .01f, 0);
    }
}
