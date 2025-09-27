using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private WeaponData startingWeapon;
    public WeaponInstance EquippedWeapon { get; private set; }


    
    

    private void Awake()
    {
        EquipWeapon(startingWeapon);
    }

    public void EquipWeapon(WeaponData newWeapon)
    {
        EquippedWeapon = new WeaponInstance(newWeapon);
        Debug.Log($"Equipped {startingWeapon.name}");
        EquippedWeapon.Reload();
    }
}
