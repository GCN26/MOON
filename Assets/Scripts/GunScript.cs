using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using static UnityEngine.GraphicsBuffer;

public class GunScript : MonoBehaviour
{
    public enum Gun
    {
        Pistol,
        Shotgun
    }

    public Gun SelectedGun;

    public GameObject hitMarker;

    public float pistolRange, shotgunRange;

    public Vector3 shotgunOffset;
    public float shotgunSpread;

    public Material PistolMaterial;

    public float gunDamage;

    public GameObject shotgunModel,shotgunMesh, pumpMesh;
    public bool switchTo;
    private Quaternion startingRot;
    public float gunSpinSpeed;
    private Quaternion reloadRot;
    public bool reloading,pumpNoEject;

    public float pistolMag, pistolReserve, shotgunLoad, shotgunReserve;
    private float maxPistolMag = 12;
    private float maxPistolReserve = 36;
    private float maxShotgunLoad = 6;
    private float maxShotgunReserve = 24;

    public void Start()
    {
        startingRot = transform.localRotation;
        reloadRot = Quaternion.Euler(new Vector3(16f, -65.62f, -10f));

        pistolMag = maxPistolMag;
        pistolReserve = maxPistolReserve;
        shotgunLoad = maxShotgunLoad;
        shotgunReserve = maxShotgunReserve;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchTo = true;
            SelectedGun = Gun.Pistol;
            transform.localRotation = new Quaternion(35.728f, -5.076f, -2.969f, 1);
            pumpNoEject = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchTo = true;
            SelectedGun = Gun.Shotgun;
            transform.localRotation = new Quaternion(35.728f, -5.076f, -2.969f, 1);
            

        }

        if(SelectedGun == Gun.Pistol)
        {
            GetComponent<MeshRenderer>().enabled = true;
            shotgunMesh.GetComponent<MeshRenderer>().enabled = false;
            pumpMesh.GetComponent<MeshRenderer>().enabled = false;
            gunDamage = 2;
        }
        else if (SelectedGun == Gun.Shotgun)
        {
            if (switchTo)
            {
                shotgunModel.GetComponent<ShotgunAnimationScript>().pump = true;
                shotgunModel.GetComponent<ShotgunAnimationScript>().eject = false;
                switchTo = false;
            }
            GetComponent<MeshRenderer>().enabled = false;
            shotgunMesh.GetComponent<MeshRenderer>().enabled = true;
            pumpMesh.GetComponent<MeshRenderer>().enabled = true;
            gunDamage = 1;
            
        }
        if (!reloading)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, startingRot, gunSpinSpeed * 100 * Time.deltaTime);
            if (pumpNoEject)
            {
                pumpNoEject = false;
                shotgunModel.GetComponent<ShotgunAnimationScript>().pump = true;
                shotgunModel.GetComponent<ShotgunAnimationScript>().eject = false;
            }
        }
        else
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, reloadRot, gunSpinSpeed * 100 * Time.deltaTime);
            pumpNoEject = true;
        }
    }

    public void TriggerGun()
    {
        if (SelectedGun == Gun.Pistol && !reloading && pistolMag > 0)
        {
            PistolShoot();
        }
        else if(SelectedGun == Gun.Shotgun && !reloading && shotgunLoad > 0)
        {
            if (shotgunModel.GetComponent<ShotgunAnimationScript>().pump == false)
            {
                ShotgunShoot();
            }
        }
    }

    public void PistolShoot()
    {
        pistolMag -= 1;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pistolRange))
        {
            var mark = Instantiate(hitMarker);
            mark.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);

            calcHitEnemy(hit);
        }
    }
    public void ShotgunShoot()
    {
        shotgunLoad -= 1;
        for (int i = 0; i < 9; i++)
        {
            calcShotgunPellets(i);
            shotgunModel.GetComponent<ShotgunAnimationScript>().pump = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition + shotgunOffset);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, shotgunRange))
            {
                var mark = Instantiate(hitMarker);
                mark.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);

                calcHitEnemy(hit);
            }
        }
    }

    public void calcShotgunPellets(int i)
    {
        if (i == 0)
        {
            shotgunOffset.x = 0;
            shotgunOffset.y = 0;
        }
        if (i == 1)
        {
            shotgunOffset.x = shotgunSpread;
            shotgunOffset.y = 0;
        }
        if (i == 2)
        {
            shotgunOffset.x = -shotgunSpread;
            shotgunOffset.y = 0;
        }
        if (i == 3)
        {
            shotgunOffset.x = shotgunSpread;
            shotgunOffset.y = shotgunSpread;
        }
        if (i == 4)
        {
            shotgunOffset.x = -shotgunSpread;
            shotgunOffset.y = shotgunSpread;
        }
        if (i == 5)
        {
            shotgunOffset.x = shotgunSpread;
            shotgunOffset.y = -shotgunSpread;
        }
        if (i == 6)
        {
            shotgunOffset.x = -shotgunSpread;
            shotgunOffset.y = -shotgunSpread;
        }
        if (i == 7)
        {
            shotgunOffset.x = 0;
            shotgunOffset.y = shotgunSpread;
        }
        if (i == 8)
        {
            shotgunOffset.x = 0;
            shotgunOffset.y = -shotgunSpread;
        }
    }

    public void calcHitEnemy(RaycastHit hit)
    {
        if (hit.collider.CompareTag("Enemy"))
        {
            hit.collider.GetComponent<EnemyMovement>().HP -= gunDamage;
        }
    }

    public void reloadGun()
    {
        //alternatively, set a bool to make this done in update
        if(SelectedGun == Gun.Pistol)
        {
            //-1 from reserve, making sure not to pass 0
            //+1 to mag, making sure not to pass 12
        }
        else if (SelectedGun == Gun.Shotgun)
        {
            //-1 from reserve, making sure not to pass 0
            //+1 to load, making sure not to pass 6
        }
    }

    public void ammoPickup()
    {
        //if small, 1/4 of all reserves
        //if medium, 1/2 of all reserves
        //if large, fill all reserves
    }
}
