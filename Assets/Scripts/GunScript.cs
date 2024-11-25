using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

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

    public Material PistolMaterial,ShotgunMaterial;

    public float gunDamage;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectedGun = Gun.Pistol;
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectedGun = Gun.Shotgun;

        if(SelectedGun == Gun.Pistol)
        {
            GetComponent<MeshRenderer>().material = PistolMaterial;
            gunDamage = 2;
        }
        else if (SelectedGun == Gun.Shotgun)
        {
            GetComponent<MeshRenderer>().material = ShotgunMaterial;
            gunDamage = 1;
        }
    }

    public void TriggerGun()
    {
        if (SelectedGun == Gun.Pistol)
        {
            PistolShoot();
        }
        else if(SelectedGun == Gun.Shotgun)
        {
            ShotgunShoot();
        }
    }

    public void PistolShoot()
    {
        //this will become its own script at a later point
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
        //this will become its own script at a later point
        for (int i = 0; i < 9; i++)
        {
            calcShotgunPellets(i);

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
}
