using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : MonoBehaviour
{
    public GameObject flashLight;
    ParticleSystem muzzleFlash;
    void Awake()
    {
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        flashLight.SetActive(false);
        GunScript.OnChangeWeaponToPistol += ActivatePistol;
        GunScript.OnChangeWeaponToRifle += DeactivatePistol;
        GunScript.OnFlashlightChange += switchFlashLightMode;
        GunScript.OnShootingBullet += MuzzleFlash;
    }
    void MuzzleFlash()
    {
        if (muzzleFlash != null && gameObject.activeSelf)
            muzzleFlash.Play();
    }
    void switchFlashLightMode()
    {
        if(gameObject.activeSelf)
            flashLight.SetActive(!flashLight.activeSelf);
    }
    void ActivatePistol()
    {
        gameObject.SetActive(true);
    }

    void DeactivatePistol()
    {
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        GunScript.OnChangeWeaponToPistol -= ActivatePistol;
        GunScript.OnChangeWeaponToRifle -= DeactivatePistol;
        GunScript.OnFlashlightChange -= switchFlashLightMode;
        GunScript.OnShootingBullet -= MuzzleFlash;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
