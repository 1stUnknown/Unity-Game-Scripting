using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleScript : MonoBehaviour
{
    public GameObject flashLight;
    ParticleSystem muzzleFlash;

    private void Awake()
    {
        GunScript.OnChangeWeaponToPistol += DeactivateRifle;
        GunScript.OnChangeWeaponToRifle += ActivateRifle;
        GunScript.OnFlashlightChange += SwitchFlashlightMode;
        GunScript.OnShootingBullet += MuzzleFlash;
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        flashLight.SetActive(false);
    }

    void MuzzleFlash()
    {
        if (muzzleFlash != null && gameObject.activeSelf)
            muzzleFlash.Play();
    }

    void SwitchFlashlightMode()
    {
        if(gameObject.activeSelf)
            flashLight.SetActive(!flashLight.activeSelf);
    }
    void ActivateRifle()
    {
        gameObject.SetActive(true);
    }

    void DeactivateRifle()
    {
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        GunScript.OnChangeWeaponToPistol -= DeactivateRifle;
        GunScript.OnChangeWeaponToRifle -= ActivateRifle;
        GunScript.OnFlashlightChange -= SwitchFlashlightMode;
        GunScript.OnShootingBullet -= MuzzleFlash;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
