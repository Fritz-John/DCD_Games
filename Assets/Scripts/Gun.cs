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
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    public void Shoot()
    {
        if(gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                if (Physics.Raycast(gunTransform.position, transform.forward, out hitInfo, gunData.maxDistance))
                {
                   
                    Debug.Log(hitInfo.transform.name);
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
