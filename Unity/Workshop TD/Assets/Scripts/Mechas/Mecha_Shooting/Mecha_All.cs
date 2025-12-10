using UnityEngine;

public class Mechas_All : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform Shoot_Point;

    private Transform target;
    public float range = 15f;

    public Transform PartToRotate;
    public float turnSpeed = 6f;

    [SerializeField] private string ennemyTag = "Enemy";
    [SerializeField] private string wallTag = "Wall";   // ← Tag des murs
    [SerializeField] private string SolTag = "Sol";   // ← Tag des murs
    [SerializeField] private LayerMask Layer;  // <-- CHOIX DU LAYER

    public float fireRate = 1f;
    private float fireCountDown;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(ennemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            // En dehors de la portée → skip
            if (distanceToEnemy > range)
                continue;

            // --- Check ligne de vue avec Raycast + tag ---
            Vector3 dir = (enemy.transform.position - Shoot_Point.position).normalized;

            if (Physics.Raycast(Shoot_Point.position, dir, out RaycastHit hit, distanceToEnemy))
            {
                // Si le raycast touche un mur → ennemi non visible
                if (hit.collider.CompareTag(wallTag))
                    continue;
            }
            if (hit.collider.CompareTag(SolTag))
            {
                // Il y a un mur → ennemi invisible
                continue;
            }
            // Vérifie si l'ennemi est sur le layer voulu
            if ((Layer.value & (1 << enemy.layer)) == 0)
            {
                continue; // pas le bon layer → on saute cet ennemi
            }

            // Ennemi visible
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnnemy = enemy;
            }
        }

        if (nearestEnnemy != null)
            target = nearestEnnemy.transform;
        else
            target = null;
    }

    void Update()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - PartToRotate.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        PartToRotate.rotation = Quaternion.Lerp(
            PartToRotate.rotation,
            lookRotation,
            Time.deltaTime * turnSpeed
        );

        if (fireCountDown <= 0)
        {
            Shoot();
            fireCountDown = 1 / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(Bullet, Shoot_Point.position, Shoot_Point.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
