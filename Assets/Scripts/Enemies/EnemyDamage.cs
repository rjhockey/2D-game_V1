using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // give access in Unity to assign amount of damage
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        // detect if player is damaged
        if (collision.tag == "Player")
            // get player health, then decrease health, TakeDamage f/ Health.cs
            collision.GetComponent<Health>().TakeDamage(damage);
    }
}
