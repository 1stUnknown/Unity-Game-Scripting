using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GunScript : MonoBehaviour
{
    public static event Action OnShootingBullet;
    public static event Action OnDoneReloading;
    public static event Action OnChangeWeaponToPistol;
    public static event Action OnChangeWeaponToRifle;
    public static event Action OnFlashlightChange;

    int currentPistolAmmo, currentRifleAmmo;
    readonly int maxPistolAmmo = 7, maxRifleAmmo = 30;
    bool shotBullet, isReloading;
    public Transform gun;
    public float reloadTime;
    Animator gunAnimation;
    public Transform rayCastPointOfOrigin;
    bool changingWeapons;
    bool usingPistol;
    public float changeTime; //how long it takes to change weapons

    public AudioSource reloadSound;
    public AudioSource shootingSound;

    /*float elapsedTime;
    Vector3 initialPosition;
    Vector3 initialPlayerPosition;
    Quaternion initialPlayerRotation;
    Vector3 offscreenPosition;*/
    // Start is called before the first frame update
    void Start()
    {
        OnShootingBullet += ShotBullet;
        if (gun != null)
        {
            gunAnimation = gun.GetComponent<Animator>();
        }
        if (rayCastPointOfOrigin == null)
        {
            rayCastPointOfOrigin = transform;
            Debug.Log("rayCastPointOfOrigin is null");
        }
        usingPistol = true;
        currentPistolAmmo = maxPistolAmmo;
        currentRifleAmmo = maxRifleAmmo;
        OnChangeWeaponToPistol?.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            OnFlashlightChange?.Invoke();
        }
        if (isReloading || changingWeapons)
            return;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            changingWeapons = true;
            gunAnimation.SetBool("isChangingWeapons", true);
            Invoke("StopChangingWeapons", changeTime);
            return;
        }
        if (usingPistol && currentPistolAmmo > 0 && Input.GetMouseButtonDown(0))
        {
            shotBullet = true;
            currentPistolAmmo--;
            shootingSound.Play();
            return;
        }
        if (!usingPistol && currentRifleAmmo > 0 && Input.GetMouseButtonDown(0))
        {
            shotBullet = true;
            currentRifleAmmo--;
            shootingSound.Play();
            return;
        }
        if ((usingPistol && currentPistolAmmo < maxPistolAmmo || (currentRifleAmmo < maxRifleAmmo && !usingPistol)) && Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
            reloadSound.Play();
            gunAnimation.SetBool("isReloading", true);
            Invoke("StopReloading", reloadTime);
            /*initialPosition = gun.transform.position;
            initialPlayerPosition = transform.position;
            initialPlayerRotation = rayCastPointOfOrigin.rotation;
            offscreenPosition = gun.transform.position;
            offscreenPosition.y -= 2;*/
            return;
        }
    }
    void StopChangingWeapons()
    {
        gunAnimation.SetBool("isChangingWeapons", false);
        changingWeapons = false;
        if (usingPistol)
        {
            usingPistol = false;
            OnChangeWeaponToRifle?.Invoke();
        }
        else
        {
            usingPistol = true;
            OnChangeWeaponToPistol?.Invoke();
        }
    }
    void StopReloading()
    {
        gunAnimation.SetBool("isReloading", false);
        isReloading = false;
        if (usingPistol)
        {
            currentPistolAmmo = maxPistolAmmo;
        }
        else
        {
            currentRifleAmmo = maxRifleAmmo;
        }
        OnDoneReloading?.Invoke();
    }
    void ShotBullet()
    {
        Vector3 origin = rayCastPointOfOrigin.position;
        Vector3 direction = rayCastPointOfOrigin.forward;
        RaycastHit hitInfo;
        Physics.Raycast(origin, direction, out hitInfo);
        if (hitInfo.collider != null && hitInfo.collider.gameObject.CompareTag("Shootable"))
        {
            GameManager.GetManager().TargetHit(gameObject, direction, hitInfo);
        }
        else if (hitInfo.collider != null)
        {
            Debug.Log("rayCastHit:" + hitInfo.collider.gameObject);
        }
    }
    public int GetAmmo()
    {
        if (usingPistol)
        {
            return currentPistolAmmo;
        }
        else return currentRifleAmmo;
    }
    public int GetMaxAmmo()
    {
        if (usingPistol)
        {
            return maxPistolAmmo;
        }
        else return maxRifleAmmo;
    }
    private void OnDestroy()
    {
        OnShootingBullet -= ShotBullet;
    }
    private void FixedUpdate()
    {
        if (shotBullet)
        {
            OnShootingBullet?.Invoke();
        }
        shotBullet = false;
        /*if (isReloading)
        {
            Vector3 playerMovement = transform.position - initialPlayerPosition;
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / reloadTime);
            if (t == 1)
            {
                gun.position = initialPosition + playerMovement;
                //gun.position = initialPosition + playerMovement + (Quaternion.Inverse(rayCastPointOfOrigin.rotation) * initialPlayerRotation) * Vector3;
                currentAmmo = maxAmmo;
                isReloading = false;
                elapsedTime = 0;

            }
            else if (t < 0.5)
            {
                gun.position = Vector3.Lerp(initialPosition + playerMovement, offscreenPosition + playerMovement, t);
            }
            else
            {
                gun.position = Vector3.Lerp(offscreenPosition + playerMovement, initialPosition + playerMovement, t);
            }
        }*/
    }
}
