using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject weaponHitbox;
  

    public void EnableWeaponHitbox()
    {
        weaponHitbox.SetActive(true);
    }
    public void DisableWeaponHitbox()
    {
        weaponHitbox.SetActive(false);
    }
}
