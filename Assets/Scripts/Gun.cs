using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GunData gunData;
    [SerializeField] Transform gunTransform;
    float timeSinceLastShot;
    RaycastHit hitInfo;
    private void Start()
    {
        PlayerShoot.shotInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    public void StartReload()
    {
        if (!gunData.reloading)
        {
            StartCoroutine(Reload());   
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    public void Shoot()
    {
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                if (Physics.Raycast(gunTransform.position, transform.forward, out hitInfo, gunData.maxDistance))
                {

                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    if (damageable is Target target && target.health > 0)
                    {
                        Debug.Log("Damage");
                        damageable?.Damage(gunData.damage);
                    }


                }
                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
        //Debug.Log("Shoot");
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
       
    }
    private void OnGunShot()
    {

        //throw new NotImplementedException();
    }


}
