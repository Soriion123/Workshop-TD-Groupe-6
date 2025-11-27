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
        // essaie Flying
        Flying_Basics fly = target.GetComponent<Flying_Basics>();
        if (fly != null)
        {
            fly.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // essaie Ground
        Ground_Basic ground = target.GetComponent<Ground_Basic>();
        if (ground != null)
        {
            ground.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        Destroy(gameObject);
    }

}
