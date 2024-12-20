using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class GunScript : MonoBehaviour
{
    
    public enum Gun
    {
        Pistol,
        Shotgun,
        Knife
    }

    [Header("Gun Type")]
    public Gun SelectedGun;

    public GameObject hitMarker;
    [Header("Gun Variables")]
    public float gunDamage;
    public float pistolRange, shotgunRange,knifeRange;
    public Vector3 shotgunOffset;
    public float shotgunSpread;

    [Header("Gun Models")]
    public GameObject shotgunModel;
    public GameObject shotgunMesh, pumpMesh;
    public GameObject pistolModel, pistolMesh, slideMesh, magMesh;
    public GameObject knifeRotate, knifeModel;
    public GameObject pistolRayPoint, shotgunRayPoint;
    public LineRenderer lineR;
    public GameObject lightFromFire;
    public bool isDark;

    [Header("Rotations")]
    private Quaternion startingRot;
    public float gunSpinSpeed;
    private Quaternion shotgunReloadRot = Quaternion.Euler(new Vector3(16f, -65.62f, -10f));
    private Quaternion shotgunShootRot = Quaternion.Euler(new Vector3(-8.139f, -4.286f, -0.416f));

    [Header("Booleans")]
    public bool switchTo;
    public bool reloading, pumpNoEject, shootRotBool, shootRotBool2,reloadCheck,reloadAmmoBool;

    [Header("Ammo")]
    public float reloadTimer = 0;
    public float pistolMag, pistolReserve, shotgunLoad, shotgunReserve;
    private float maxPistolMag = 12;
    private float maxPistolReserve = 36;
    private float maxShotgunLoad = 6;
    private float maxShotgunReserve = 12;
    

    [Header("UI")]
    public TextMeshProUGUI AmmoClip;
    public TextMeshProUGUI AmmoReserve;
    public GameObject pistolRet, shotgunRet;

    [Header("Sounds")]
    public AudioSource ShotSource;
    public AudioSource ReloadSource,PumpSource;
    public AudioClip shotgunShot,pistolShot,knifeSwingSFX;
    public AudioClip shotgunReload, pistolReload,pumpSFX;

    public void Start()
    {
        startingRot = transform.localRotation;

        pistolMag = maxPistolMag;
        pistolReserve = maxPistolReserve;
        shotgunLoad = maxShotgunLoad;
        shotgunReserve = maxShotgunReserve;
    }
    public void Update()
    {
        if (Time.timeScale > 0)
        {
            ShotSource.UnPause();
            ReloadSource.UnPause();
            PumpSource.UnPause();

            ShotSource.volume = PlayerSaveSettings.SFXVolume * PlayerSaveSettings.masterVolume;
            ReloadSource.volume = PlayerSaveSettings.SFXVolume * PlayerSaveSettings.masterVolume;
            PumpSource.volume = PlayerSaveSettings.SFXVolume * PlayerSaveSettings.masterVolume;

            if (Input.GetButtonDown("BumperR"))
            {
                if (SelectedGun == Gun.Pistol)
                {
                    SwitchShotgun();
                }
                else if (SelectedGun == Gun.Shotgun)
                {
                    SwitchKnife();
                }
                else if (SelectedGun == Gun.Knife)
                {
                    SwitchPistol();
                }
            }
            if (Input.GetButtonDown("BumperL"))
            {
                if (SelectedGun == Gun.Pistol)
                {
                    SwitchKnife();
                }
                else if (SelectedGun == Gun.Shotgun)
                {
                    SwitchPistol();
                }
                else if (SelectedGun == Gun.Knife)
                {
                    SwitchShotgun();
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && !reloadAmmoBool && !reloading && !shootRotBool)
            {
                SwitchPistol();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && !reloadAmmoBool && !reloading && !shootRotBool)
            {
                SwitchShotgun();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && !reloadAmmoBool && !reloading && !shootRotBool)
            {
                SwitchKnife();
            }

            if(pistolReserve > maxPistolReserve)
            {
                pistolReserve = maxPistolReserve;
            }
            if(shotgunReserve > maxShotgunReserve)
            {
                shotgunReserve = maxShotgunReserve;
            }

            if (Input.GetButtonDown("Reload") && !shootRotBool)
            {
                if (SelectedGun == Gun.Pistol)
                {
                    if (pistolReserve > 0)
                    {
                        reloadAmmoBool = true;
                    }
                }
                else if (SelectedGun == Gun.Shotgun)
                {
                    if (shotgunReserve > 0)
                    {
                        reloadAmmoBool = true;
                    }
                }
                else if(SelectedGun == Gun.Knife) { }
            }

            if (SelectedGun == Gun.Pistol)
            {
                pistolRet.SetActive(true);
                shotgunRet.SetActive(false);
                if (switchTo)
                {
                    ReloadSource.Stop();
                    PumpSource.Stop();
                    switchTo = false;
                }
                AmmoClip.text = string.Format("{0}", pistolMag);
                AmmoReserve.text = string.Format("{0}", pistolReserve);
                pistolMesh.GetComponent<SkinnedMeshRenderer>().enabled = true;
                slideMesh.GetComponent<SkinnedMeshRenderer>().enabled = true;
                magMesh.GetComponent<SkinnedMeshRenderer>().enabled = true;
                shotgunMesh.GetComponent<MeshRenderer>().enabled = false;
                pumpMesh.GetComponent<MeshRenderer>().enabled = false;
                knifeModel.GetComponent<MeshRenderer>().enabled = false;
                ShotSource.clip = pistolShot;
                ReloadSource.clip = pistolReload;
                gunDamage = 2;

                if (reloadAmmoBool && pistolReserve > 0)
                {
                    for (int i = 0; i < maxPistolMag; i++)
                    {
                        
                        if (pistolMag == maxPistolMag)
                        {
                            reloadAmmoBool = false;
                            break;
                        }
                        else
                        {
                            ReloadSource.Play();
                            reloading = true;
                            if (pistolReserve > 0)
                            {
                                pistolReserve -= 1;
                                pistolMag += 1;
                            }
                            else
                            {
                                reloadAmmoBool = false;
                                break;
                            }
                        }
                    }
                }
                if (reloading)
                {
                    if (reloadCheck == false)
                    {
                        pistolModel.GetComponent<PistolAnimationScript>().reload = true;
                        reloadCheck = true;
                    }
                    else if (pistolModel.GetComponent<PistolAnimationScript>().reload == false)
                    {
                        reloadCheck = false;
                        reloading = false;
                    }
                }
                if (pistolModel.GetComponent<PistolAnimationScript>().fire == true)
                {
                    reloadCheck = false;
                    //ReloadSource.Stop();
                    pistolModel.GetComponent<PistolAnimationScript>().reload = false;
                }
            }
            else if (SelectedGun == Gun.Shotgun)
            {
                pistolRet.SetActive(false);
                shotgunRet.SetActive(true);
                AmmoClip.text = string.Format("{0}", shotgunLoad);
                AmmoReserve.text = string.Format("{0}", shotgunReserve);
                if (switchTo)
                {
                    shotgunModel.GetComponent<ShotgunAnimationScript>().pump = true;
                    shotgunModel.GetComponent<ShotgunAnimationScript>().eject = false;
                    ReloadSource.Stop();
                    switchTo = false;
                }
                pistolMesh.GetComponent<SkinnedMeshRenderer>().enabled = false;
                slideMesh.GetComponent<SkinnedMeshRenderer>().enabled = false;
                magMesh.GetComponent<SkinnedMeshRenderer>().enabled = false;
                shotgunMesh.GetComponent<MeshRenderer>().enabled = true;
                pumpMesh.GetComponent<MeshRenderer>().enabled = true;
                knifeModel.GetComponent<MeshRenderer>().enabled = false;
                ShotSource.clip = shotgunShot;
                ReloadSource.clip = shotgunReload;
                PumpSource.clip = pumpSFX;
                gunDamage = 1;

                if (reloadAmmoBool && shotgunReserve > 0)
                {
                    for (int i = 0; i < maxShotgunLoad; i++)
                    {
                        if (shotgunLoad == maxShotgunLoad)
                        {
                            reloadAmmoBool = false;
                            break;
                        }
                        else
                        {
                            reloading = true;
                            if (shotgunReserve > 0)
                            {
                                shotgunReserve -= 1;
                                shotgunLoad += 1;
                            }
                            else
                            {
                                reloadAmmoBool = false;
                                break;
                            }
                        }
                    }
                    reloadAmmoBool = false;
                }
                if (reloading && !shootRotBool)
                {
                    ReloadSource.Play();
                    transform.localRotation = Quaternion.RotateTowards(transform.localRotation, shotgunReloadRot, gunSpinSpeed * 100 * Time.deltaTime);
                    pumpNoEject = true;
                    reloadTimer += Time.deltaTime;
                    if (reloadTimer > 1f)
                    {
                        reloadTimer = 0;
                        reloading = false;
                    }
                }
                else if (!reloading && shootRotBool)
                {
                    if (SelectedGun == Gun.Shotgun)
                    {
                        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, shotgunShootRot, gunSpinSpeed * 250 * Time.deltaTime);
                        if (shotgunModel.GetComponent<ShotgunAnimationScript>().pumpTimer > .15)
                        {
                            shootRotBool2 = true;
                        }
                        if (shootRotBool2)
                        {
                            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, startingRot, .05f * Time.deltaTime);
                            shootRotBool = false;
                            shootRotBool2 = false;
                        }
                    }
                }
            }
            else if(SelectedGun == Gun.Knife)
            {
                pistolRet.SetActive(true);
                shotgunRet.SetActive(false);
                if (switchTo)
                {
                    ReloadSource.Stop();
                    PumpSource.Stop();
                    switchTo = false;
                }
                AmmoClip.text = string.Format("");
                AmmoReserve.text = string.Format("");
                pistolMesh.GetComponent<SkinnedMeshRenderer>().enabled = false;
                slideMesh.GetComponent<SkinnedMeshRenderer>().enabled = false;
                magMesh.GetComponent<SkinnedMeshRenderer>().enabled = false;
                shotgunMesh.GetComponent<MeshRenderer>().enabled = false;
                pumpMesh.GetComponent<MeshRenderer>().enabled = false;
                knifeModel.GetComponent<MeshRenderer>().enabled = true;
                gunDamage = 10;
                ShotSource.clip = knifeSwingSFX;
                ReloadSource.clip = null;
            }
            if (!reloading && !shootRotBool)
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, startingRot, gunSpinSpeed * 100 * Time.deltaTime);
                if (pumpNoEject)
                {
                    pumpNoEject = false;
                    shotgunModel.GetComponent<ShotgunAnimationScript>().pump = true;
                    shotgunModel.GetComponent<ShotgunAnimationScript>().eject = false;
                }
            }
        }
        else
        {
            ShotSource.Pause();
            ReloadSource.Pause();
            PumpSource.Pause();
        }
            
    }

    public void TriggerGun()
    {
        if (SelectedGun == Gun.Pistol && !reloading && !reloadAmmoBool && !reloadCheck)
        {
            if (pistolModel.GetComponent<PistolAnimationScript>().fire == false)
            {
                PistolShoot();
             }
        }
        else if(SelectedGun == Gun.Shotgun && !reloading && !reloadAmmoBool && !reloadCheck)
        {
            if (shotgunModel.GetComponent<ShotgunAnimationScript>().pump == false)
            {
                ShotgunShoot();
            }
        }
        else if(SelectedGun == Gun.Knife)
        {
            KnifeSwing();
        }
    }

    public void PistolShoot()
    {
        if (pistolMag > 0)
        {
            ShotSource.Play();
            pistolMag -= 1;
            pistolModel.GetComponent<PistolAnimationScript>().fire = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, pistolRange))
            {
                var mark = Instantiate(hitMarker);
                mark.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);

                calcHitEnemy(hit);
            }
            if (isDark)
            {
                GameObject lightFromGun = Instantiate(lightFromFire);
                lightFromGun.transform.rotation = this.transform.rotation;
                lightFromGun.transform.position = pistolRayPoint.transform.position;
            }
            LineRenderer lineE = Instantiate(lineR);
            lineE.positionCount = 2;
            lineE.SetPosition(0, pistolRayPoint.transform.position);
            lineE.SetPosition(1, ray.GetPoint(pistolRange));
        }
        else if(pistolMag == 0 && pistolReserve > 0)
        {
            reloadAmmoBool = true;
        }
    }
    public void ShotgunShoot()
    {
        if (shotgunLoad > 0)
        {
            
            ShotSource.Play();
            shotgunLoad -= 1;
            shootRotBool = true;
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
                if (isDark && i == 0)
                {
                    GameObject lightFromGun = Instantiate(lightFromFire);
                    lightFromGun.transform.rotation = this.transform.rotation;
                    lightFromGun.transform.position = pistolRayPoint.transform.position;
                }
                LineRenderer lineE = Instantiate(lineR);
                lineE.positionCount = 2;
                lineE.SetPosition(0, shotgunRayPoint.transform.position);
                lineE.SetPosition(1, ray.GetPoint(shotgunRange));

            }
        }
        else if(shotgunLoad == 0 && shotgunReserve > 0)
        {
            reloadAmmoBool = true;
        }
        
    }
    public void KnifeSwing()
    {
        if (knifeModel.GetComponent<KnifeAnimationScript>().fire == false)
        {
            ShotSource.Play();
            knifeModel.GetComponent<KnifeAnimationScript>().fire = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, knifeRange))
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
        else if (hit.collider.CompareTag("Box"))
        {
            hit.collider.GetComponent<BoxScript>().OwIGotHitSoNowIBreakOpenAndGoAway = true;
        }
    }
    public void ammoPickup(float multiplier)
    {
        pistolReserve += Mathf.Floor(maxPistolReserve * multiplier);
        shotgunReserve += Mathf.Floor(maxShotgunReserve * multiplier);
    }
    
    public void SwitchPistol()
    {
        switchTo = true;
        SelectedGun = Gun.Pistol;
        transform.localRotation = new Quaternion(35.728f, -5.076f, -2.969f, 1);
        pumpNoEject = false;
        reloadCheck = false;
        reloadAmmoBool = false;
        reloadTimer = 0;
        pistolRet.SetActive(true);
        shotgunRet.SetActive(false);
    }
    public void SwitchShotgun()
    {
        switchTo = true;
        SelectedGun = Gun.Shotgun;
        transform.localRotation = new Quaternion(35.728f, -5.076f, -2.969f, 1);
        reloadCheck = false;
        reloadAmmoBool = false;
        reloadTimer = 0;
        pistolRet.SetActive(false);
        shotgunRet.SetActive(true);
    }
    public void SwitchKnife()
    {
        switchTo = true;
        SelectedGun = Gun.Knife;
        transform.localRotation = new Quaternion(35.728f, -5.076f, -2.969f, 1);
        reloadCheck = false;
        reloadAmmoBool = false;
        reloadTimer = 0;
        pistolRet.SetActive(true);
        shotgunRet.SetActive(false);
    }
}
