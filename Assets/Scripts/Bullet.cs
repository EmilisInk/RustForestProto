using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 5;
    public float lifeTime = 3f;

    [HideInInspector] public GameObject owner;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // ignore shooter
        if (owner != null && other.transform.IsChildOf(owner.transform))
            return;

        // Player
        PlayerHealth ph = other.GetComponentInParent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // Enemy
        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // walls/ground/etc
        Destroy(gameObject);
    }
}