using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;

    [Header("Stats")]
    public float speed = 70f;
    public float damage = 5f; // dégâts réglables dans l'inspecteur

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        //Collision avec la cible
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        // --- FLYING TYPES ---
        Flying_Basic flyBasic = target.GetComponent<Flying_Basic>();
        if (flyBasic != null)
        {
            flyBasic.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        Flying_Fast flyFast = target.GetComponent<Flying_Fast>();
        if (flyFast != null)
        {
            flyFast.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        Flying_Tank flyTank = target.GetComponent<Flying_Tank>();
        if (flyTank != null)
        {
            flyTank.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // --- GROUND TYPES ---
        Ground_Basic groundBasic = target.GetComponent<Ground_Basic>();
        if (groundBasic != null)
        {
            groundBasic.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        Ground_Fast groundFast = target.GetComponent<Ground_Fast>();
        if (groundFast != null)
        {
            groundFast.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        Ground_Tank groundTank = target.GetComponent<Ground_Tank>();
        if (groundTank != null)
        {
            groundTank.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // === AJOUTE ICI TES NOUVEAUX TYPES D'ENNEMIS ===
        /*
        EliteEnemy elite = target.GetComponent<EliteEnemy>();
        if (elite != null)
        {
            elite.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }
        */

        // Fallback
        Destroy(gameObject);
        return;
    }


}
