using UnityEngine;

public class Mechas_Air : MonoBehaviour
{

    //[SerializeField] private GameObject Projot_Prefab;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject Canon;
    [SerializeField] private Transform Shoot_Point;
    private Transform target;
    public float range = 15f;

    public Transform PartToRotate;
    public float turnSpeed = 6f;

    [SerializeField] private string ennemyTag = "Enemy"; // <-- CHOIX DU TAG
    [SerializeField] private LayerMask Layer;  // <-- CHOIX DU LAYER


    public float fireRate = 1f;
    private float fireCountDown;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
            // Vérifie si l'ennemi est sur le layer voulu
            if ((Layer.value & (1 << enemy.layer)) == 0)
            {
                continue; // pas le bon layer → on saute cet ennemi
            }

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnnemy = enemy;
            }
        }

        if (nearestEnnemy != null && shortestDistance <= range)
        {
            target = nearestEnnemy.transform;
        }
        else
        {
            target = null;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        
        Vector3 dir = target.position - PartToRotate.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        PartToRotate.rotation = Quaternion.Lerp(
            PartToRotate.rotation,
            lookRotation,
            Time.deltaTime * turnSpeed);


        if (fireCountDown <= 0)
        {
            Shoot();
            fireCountDown = 1 / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }


    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(Bullet, Shoot_Point.position, Shoot_Point.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
