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

        // Collision avec la cible
        /*if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }*/

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    /*void HitTarget()
    {
        // Récupération du script EnnemySmall
        EnnemySmall enemy = target.GetComponent<EnnemySmall>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject); // La bullet disparaît après l'impact
    }*/
}
