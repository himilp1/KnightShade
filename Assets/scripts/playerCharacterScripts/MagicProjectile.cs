using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject); // Destroy the projectile after inflicting damage
        }
    }
}
