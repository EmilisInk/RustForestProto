using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [Header("Health")]
    public int maxHealth = 20;
    public int health;

    [Header("Target")]
    public Transform player;
    public float detectionRange = 20f;
    public float rotateSpeed = 6f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float shootSpeed = 40f;
    public float shootCooldown = 0.8f;
    public float aimHeight = 0.5f;
    public AudioSource shootAudio;

    float nextShootTime;

    void Start()
    {
        health = maxHealth;

        GameObject p = GameObject.FindWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist > detectionRange) return;

        AimAtPlayer();

        if (Time.time >= nextShootTime)
        {
            ShootAtPlayer();
            nextShootTime = Time.time + shootCooldown;
        }
    }

    void AimAtPlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.001f) return;

        Quaternion targetRot = Quaternion.LookRotation(dir.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
    }

    void ShootAtPlayer()
    {
        if (bulletPrefab == null || shootPoint == null) return;

        Vector3 aimPoint = player.position + Vector3.up * aimHeight;
        Vector3 dir = (aimPoint - shootPoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(dir));

        Bullet b = bullet.GetComponent<Bullet>();
        if (b != null) b.owner = gameObject;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = false;
            rb.velocity = dir * shootSpeed;
        }

        if (shootAudio != null)
            shootAudio.Play();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Turret HP: " + health);

        if (health <= 0)
            Destroy(gameObject);
    }
}