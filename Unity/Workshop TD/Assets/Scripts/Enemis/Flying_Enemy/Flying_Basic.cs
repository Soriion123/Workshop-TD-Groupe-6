using UnityEngine;

public class Flying_Basic: Flying_Enemy
{


    [Header("Stats")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float obstacleDetectionDistance = 2f;
    [SerializeField] private float avoidanceStrength = 2f;
    [SerializeField] private int goldReward = 1;   // 💰 or gagné à la mort

    public float NexusDamage = 5f; // dégâts infligés à l'objectif

    [Header("Target")]
    //public Transform target;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (target == null) return;

        // Direction vers la cible
        Vector3 direction = (target.position - transform.position).normalized;

        // Détection des obstacles avec raycast
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, obstacleDetectionDistance))
        {
            // Calcul d'une direction de contournement
            Vector3 avoidDirection = Vector3.Cross(Vector3.up, hit.normal).normalized;
            direction = Vector3.Lerp(direction, avoidDirection, 0.7f).normalized;
        }

        // Déplacement
        transform.position += direction * speed * Time.deltaTime;

        // Vérification si la cible est atteinte
        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            ReachTarget();
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