using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI ammoText;

    [Header("Equip check")]
    public Item gunItem;
    public int selectedHotbarIndex = 0;
    public bool blockWhenInventoryOpen = true;

    public Camera cam;
    public float maxDistance = 200f;
    public ParticleSystem shootingFlash;

    private void Start()
    {
        currentAmmo = maxAmmo;
        cam = Camera.main;

        ammoText.enabled = false;
        ammoText.text = maxAmmo.ToString();
    }
    void Update()
    {
        ammoText.enabled = false;

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

        ammoText.text = currentAmmo.ToString();
        ammoText.enabled = true;

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

        if(Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            Reload();
        }
    }

    void TryShoot()
    {
        if(currentAmmo <= 0)
        {
            return;
        }

        shootingFlash.Emit(2);

        Shoot();
        currentAmmo--;
        ammoText.text = currentAmmo.ToString();
        lastShootTime = Time.time;
    }
    void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 dir = ray.direction;

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.LookRotation(dir));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(dir * shootForce);


    }
    public void Reload()
    {
        currentAmmo = maxAmmo;
        ammoText.text = currentAmmo.ToString();
    }
}
