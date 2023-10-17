using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GunData gunData;
    [SerializeField] Transform gunTransform;
    [SerializeField] Text currentAmmo;
    [SerializeField] Text textScore;
    int score;
    float timeSinceLastShot;
    RaycastHit hitInfo;
    private void Start()
    {

        PlayerShoot.shotInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
        currentAmmo.text = gunData.currentAmmo.ToString() + "/" + gunData.magSize.ToString();
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
                Camera mainCamera = Camera.main;
                Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                Ray ray = mainCamera.ScreenPointToRay(screenCenter); 

                if (Physics.Raycast(ray, out hitInfo, gunData.maxDistance))
                {
                    //Debug.Log(hitInfo.collider.name);


                    if (hitInfo.collider.CompareTag("Target"))
                    {
                        score++;
                        textScore.text = score.ToString();
                        Target target = hitInfo.transform.parent.GetComponent<Target>();
                        IDamageable damageable = hitInfo.transform.parent.GetComponent<IDamageable>();
                        if (target.health > 0)
                        {
                            Debug.Log("Damage");

                            damageable?.Damage(gunData.damage);
                        }
                      
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
        Debug.DrawRay(gunTransform.position, transform.forward * gunData.maxDistance, Color.green);

        currentAmmo.text = gunData.currentAmmo.ToString() + "/" + gunData.magSize.ToString();
    }
    private void OnGunShot()
    {

        //throw new NotImplementedException();
    }


}
