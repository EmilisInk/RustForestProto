using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootForce = 700f;
    public float shootCooldown = 0.5f;
    private float lastShootTime;

    public int maxAmmo = 30;
    private int currentAmmo;

    [Header("Equip check")]
    public Item gunItem;
    public int selectedHotbarIndex = 0;
    public bool blockWhenInventoryOpen = true;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }
    void Update()
    {
        if (blockWhenInventoryOpen && ToggleInventory.isOpen)
        {
            return;
        }
        if (InventoryManager.Instance == null) return;

        Item selected = InventoryManager.Instance.GetHotbarItem(selectedHotbarIndex);
        if (selected == null || selected != gunItem)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            TryShoot();
        }

        else if (Input.GetMouseButton(0) && Time.time >= lastShootTime + shootCooldown)
        {
            if (Time.time >= lastShootTime + shootCooldown)
            {
                TryShoot();
            }
        }
    }

    void TryShoot()
    {
        if(currentAmmo <= 0)
        {
            return;
        }

        Shoot();
        currentAmmo--;
        lastShootTime = Time.time;
    }
    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(shootPoint.forward * shootForce);
    }
    public void Reload()
    {
        currentAmmo = maxAmmo;
    }
}
