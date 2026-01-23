using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3;
    public int damage = 3;
    public float knockbackForce = 2f;

    void Start()
    {
        Invoke(nameof(SelfDestruct), lifeTime);
    }

    void SelfDestruct()
    {
        //Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();
        enemy = other.collider.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
            enemy.TakeDamage(damage, knockbackDirection, knockbackForce);
            SelfDestruct();
            return;
        }

        if (other.transform.CompareTag("Player"))
        {
            SelfDestruct();
            return;
        }

        SelfDestruct();
    }
}
