using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    public int health = 10;
    public int maxHealth = 10;

    [Header("Ray / Attack Range")]
    public float rayDistance = 2f;
    public float rayHeight = 1.0f;

    [Header("Damage")]
    public float damage = 2f;
    public float knockbackForce = 2f;
    public float selfKnockbackMultiplier = 1f;

    [Header("Attack Cooldown")]
    public float attackCooldown = 1.5f;
    private float nextAttackTime = 0f;

    [Header("Speed")]
    public float speed = 3f;
    public float stoppingDistance = 1f;

    [Header("Stun")]
    public float stunTime = 0.3f;
    private bool concusioned = false;
    private Coroutine concussionRoutine;

    [Header("Particles")]
    public ParticleSystem getHiteffect;

    [Header("Animator Parameter Names")]
    public string animIsMoving = "isMoving";
    public string animAttack = "attack";
    public string animIsDead = "isDead";

    [HideInInspector] public bool isDead = false;

    private GameObject player;
    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");

        health = Mathf.Clamp(health, 0, maxHealth);
    }

    void Update()
    {
        if (isDead || player == null) return;

        float dist = Vector3.Distance(transform.position, player.transform.position);
        bool moving = dist > stoppingDistance && !concusioned;

        animator.SetBool(animIsMoving, moving);

        TryAttack();
    }

    void FixedUpdate()
    {
        if (isDead || player == null) return;
        GoToPlayer();
    }

    public void TakeDamage(int dmg, Vector3 knockbackDirection, float kbForce)
    {
        if (isDead) return;

        health -= dmg;

        // Enemy knockback
        rb.AddForce(knockbackDirection.normalized * kbForce * selfKnockbackMultiplier, ForceMode.Impulse);

        // Stun (stop moving for a moment)
        if (concussionRoutine != null) StopCoroutine(concussionRoutine);
        concussionRoutine = StartCoroutine(Stun());

        if (health <= 0)
            Die();
    }

    IEnumerator Stun()
    {
        concusioned = true;
        yield return new WaitForSeconds(stunTime);
        concusioned = false;
    }



    void Die()
    {
        isDead = true;

        animator.SetBool(animIsDead, true);

        if (getHiteffect != null)
        {
            var fx = Instantiate(getHiteffect, transform.position, Quaternion.identity);
            fx.Play();
            Destroy(fx.gameObject, fx.main.duration + fx.main.startLifetime.constantMax);
        }

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        animator.speed = 1f;

        StartCoroutine(FreezeAfterDeath(1.2f));
    }
    IEnumerator FreezeAfterDeath(float time)
    {
        yield return new WaitForSeconds(time);
        animator.speed = 0f;
    }

    void GoToPlayer()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist > stoppingDistance && !concusioned)
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
        }

        Vector3 lookDirection = player.transform.position - transform.position;
        lookDirection.y = 0f;

        if (lookDirection.sqrMagnitude > 0.0001f)
            rb.rotation = Quaternion.LookRotation(lookDirection);
    }
    void TryAttack()
    {
        if (Time.time < nextAttackTime) return;

        Vector3 origin = transform.position + Vector3.up * rayHeight;
        Ray ray = new Ray(origin, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                animator.SetTrigger(animAttack);

                DamagePlayer();

                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    public void DamagePlayer()
    {
        if (isDead || player == null) return;

        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist > rayDistance + 0.25f) return;

        var pm = player.GetComponent<PlayerMovement>();
        if (pm == null) return;

        Vector3 knockbackDir = (player.transform.position - transform.position).normalized;
        pm.TakeDamage(damage, knockbackDir, knockbackForce);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 origin = transform.position + Vector3.up * rayHeight;
        Gizmos.DrawRay(origin, transform.forward * rayDistance);
    }
}