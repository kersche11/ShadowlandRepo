using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject weaponHitbox;
  

    //Wird vom Animator Attack Event an einem bestimmten Keyframe getriggert
    public void EnableWeaponHitbox()
    {
        weaponHitbox.SetActive(true);
    }
    public void DisableWeaponHitbox()
    {
        weaponHitbox.SetActive(false);
    }
}
