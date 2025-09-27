using UnityEngine;

public class WeaponInstance
{
    public WeaponData WeaponData { get; private set; }
    public int CurrentAmmo { get; private set; }
    public int MaxAmmo { get; private set; }
    
    
    
    

    public WeaponInstance(WeaponData weaponData)
    {
        WeaponData = weaponData;
        CurrentAmmo = 0;
        MaxAmmo = 0;
    }

    public bool TryToUseAmmo()
    {
        if (CurrentAmmo > 0)
        {
            CurrentAmmo--;
            Debug.Log($"Shot! Ammo {CurrentAmmo}/{MaxAmmo}");
            return true;
        }
        else
        {
            Debug.Log("No ammo shit");
            return false;
        }
    }

    public void Reload()
    {
        MaxAmmo = Dice.Roll(WeaponData.magazineSizeDice);
        CurrentAmmo = MaxAmmo;
        Debug.Log($"Reloaded {WeaponData.weaponName}! New magazine {CurrentAmmo}/{MaxAmmo}");
    }
}
