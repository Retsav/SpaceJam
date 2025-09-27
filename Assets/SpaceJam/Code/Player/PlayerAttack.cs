using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;


[RequireComponent(typeof(PlayerInventory))]
public class PlayerAttack : MonoBehaviour
{
    private PlayerInventory _playerInventory;
    private bool _isReloading = false;
    public float reloadTime = 2f;


    public float shotCooldown = 0.25f;
    private float lastShotTime = -1f;

    [SerializeField] private UIManager uiManager;


    [SerializeField] private Transform weaponModel;
    [SerializeField] private float recoilDistance;
    
    
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 50f;
    
    
    [SerializeField] private GameObject muzzleFlashPrefab;
    
    
    
    private void Awake()
    {
        _playerInventory = GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        if (_isReloading || Time.time < lastShotTime + shotCooldown) return;
        if (Input.GetMouseButtonDown(0)) Attack();
        if (Input.GetKeyDown(KeyCode.R)) ReloadWeapon();
    }
    

    private void Attack()
    {
        WeaponInstance currentWeapon = _playerInventory.EquippedWeapon;
        if (currentWeapon == null) return;
        if (!currentWeapon.TryToUseAmmo())
        {
            ReloadWeapon();
        }
        else
        {
            lastShotTime = Time.time;
            
            GameObject flash = Instantiate(muzzleFlashPrefab, firePoint);
            Destroy(flash, 0.05f);
            TriggerRecoil();
            
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(90, 0, 0));
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.linearVelocity = firePoint.forward * bulletSpeed;
            }
            
        }

        //Debug.Log("");
    }

    private void TriggerRecoil()
    {
        if (weaponModel == null) return;
        Sequence recoilSequence = DOTween.Sequence();
        float halfDuration = shotCooldown / 2f;
        recoilSequence.Append(weaponModel.DOLocalMoveX(-recoilDistance, halfDuration));
        recoilSequence.Append(weaponModel.DOLocalMoveX(0, halfDuration));
    }

    private void ReloadWeapon()
    {
        if (_playerInventory.EquippedWeapon != null)
            StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        _isReloading = true;
        Debug.Log("Reloading.");
        uiManager.ShowReloadIndicator(true);
        float elapsedTime = 0f;
        while (elapsedTime < reloadTime)
        {
            if (uiManager != null)
            {
                float progress = elapsedTime / reloadTime;
                uiManager.UpdateReloadIndicator(progress);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _playerInventory.EquippedWeapon.Reload();
        _isReloading = false;
        uiManager.ShowReloadIndicator(false);
    }
}
