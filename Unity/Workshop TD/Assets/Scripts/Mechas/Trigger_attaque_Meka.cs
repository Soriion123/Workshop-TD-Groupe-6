using UnityEngine;

public class Trigger_attaque_Meka : MonoBehaviour
{

    //[SerializeField] private GameObject Projot_Prefab;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject Canon;
    [SerializeField] private Transform Shoot_Point;
    private Transform target;
    public float range = 15f;

    public Transform PartToRotate;
    public float turnSpeed = 6f;

    public string ennemyTag = "Enemy";

    public float fireRate = 1f;
    private float fireCountDown;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] Enemy = GameObject.FindGameObjectsWithTag(ennemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnnemy = null;

        foreach (GameObject ennemy in Enemy)
        {
            float distanceToEnnemy = Vector3.Distance(transform.position, ennemy.transform.position);
            if (distanceToEnnemy < shortestDistance)
            {
                shortestDistance = distanceToEnnemy;
                nearestEnnemy = ennemy;
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

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

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

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            print("Enemy In Range");
            Canon.transform.LookAt(other.transform.position);
            //Instantiate(Projot_Prefab, Shoot_Point.gameObject.transform.position, Quaternion.identity);
        }
    }*/

}
