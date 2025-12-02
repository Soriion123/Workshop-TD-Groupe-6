using UnityEngine;
using UnityEngine.AI;

public class Ground_Basic : Ground_Enemy
{
    private NavMeshAgent agent;

    [Header("Target")]
    //public Transform target;

    [Header("Stats")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float currentspeed = 5f;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private int goldReward = 1;   // 💰 or gagné à la mort
    public float NexusDamage = 5f; // dégâts infligés à l'objectif

    private float currentHealth;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        currentspeed = speed;
        currentHealth = maxHealth;

        // si jamais le spawner n’a pas fourni de target
        if (target == null)
        {
            GameObject t = GameObject.Find("Enemy Target / Nexus");
            if (t != null) target = t.transform;
        }
    }

    public void ModifySpeed(float multiplier)
    {
        currentspeed = speed * multiplier;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * currentspeed * Time.deltaTime);

        if (target != null)
        {
            agent.speed = speed;
            agent.SetDestination(target.position);
        }
    }
    

    private void ReachTarget()
    {
        // On récupère le script BuildingHealth sur la cible
        Nexus building = target.GetComponent<Nexus>();

        if (building != null)
        {
            building.TakeDamage(NexusDamage);
        }

        // ❗ Pas de gold ici
        Destroy(gameObject);
        return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Nexus"))
        {
            Nexus building = other.GetComponent<Nexus>();

            if (building != null)
            {
                building.TakeDamage(NexusDamage);
            }

            Destroy(gameObject); // L'ennemi disparaît après impact
        }
    }


    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0) Die();
    }


    private void Die()
    {
        Destroy(gameObject);
        GameManager.instance.AddGold(goldReward);
    }
}
