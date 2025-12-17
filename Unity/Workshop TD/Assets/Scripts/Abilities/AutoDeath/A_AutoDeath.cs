using UnityEngine;

public class A_AutoDeath : MonoBehaviour
{
    [Header("AutoDeath Settings")]
    public float damage = 5000f;
    public float radius = 5f;

    private void Start()
    {
        Explode();
        Destroy(gameObject, 0.2f); // La zone disparaît toute seule
    }

    private void Explode()
    {
        // ✅ Détection forcée de tous les ennemis dans la zone
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Ground_Fast fast))
            {
                fast.TakeDamage(damage);
            }
            else if (hit.TryGetComponent(out Ground_Tank tank))
            {
                tank.TakeDamage(damage);
            }
            else if (hit.TryGetComponent(out Ground_Basic basic))
            {
                basic.TakeDamage(damage);
            }
            if (hit.TryGetComponent(out Flying_Fast fasts))
            {
                fasts.TakeDamage(damage);
            }
            else if (hit.TryGetComponent(out Flying_Tank tanks))
            {
                tanks.TakeDamage(damage);
            }
            else if (hit.TryGetComponent(out Flying_Basic basics))
            {
                basics.TakeDamage(damage);
            }
            /*else if (hit.TryGetComponent(out Ground_Boss boss))
            {
                boss.TakeDamage(damage);
            }*/
        }

        
    }

    // visualiser le rayon dans l’éditeur
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
