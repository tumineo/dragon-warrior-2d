using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            collision.GetComponent<Health>().TakeDamage(damage);
    }
}
