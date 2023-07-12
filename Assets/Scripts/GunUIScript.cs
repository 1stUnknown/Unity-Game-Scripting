using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunUIScript : MonoBehaviour
{
    TextMeshProUGUI text;
    public GunScript gun;

    // Start is called before the first frame update
    void Start()
    {
        if (gun == null)
        {
            Debug.Log("gun not linked to GunUIScript");
        }
        text = GetComponent<TextMeshProUGUI>();
        GunScript.OnShootingBullet += UpdateAmmoLeft;
        GunScript.OnDoneReloading += UpdateAmmoLeft;
        GunScript.OnChangeWeaponToPistol += UpdateAmmoLeft;
        GunScript.OnChangeWeaponToRifle += UpdateAmmoLeft;
    }
    void UpdateAmmoLeft()
    {
        text.text = gun.GetAmmo() + "/" + gun.GetMaxAmmo();
    }
    private void OnDestroy()
    {
        GunScript.OnShootingBullet -= UpdateAmmoLeft;
        GunScript.OnDoneReloading -= UpdateAmmoLeft;
        GunScript.OnChangeWeaponToPistol -= UpdateAmmoLeft;
        GunScript.OnChangeWeaponToRifle -= UpdateAmmoLeft;
    }
}
