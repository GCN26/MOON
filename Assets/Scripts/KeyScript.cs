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
    public GameObject keyModel;
    public Material redMat, blueMat, yellowMat;

    public void Start()
    {
        if(keyColor == key.red) keyModel.GetComponent<MeshRenderer>().materials[0] = redMat;
        else if (keyColor == key.blue) keyModel.GetComponent<MeshRenderer>().materials[0] = redMat;
        else if (keyColor == key.yellow) keyModel.GetComponent<MeshRenderer>().materials[0] = yellowMat;
    }

    public override void CollectScript(Collider other)
    {
        other.GetComponent<PlayerScript>().keyCollect((int)keyColor);
    }
}
