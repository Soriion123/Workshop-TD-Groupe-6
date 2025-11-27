using UnityEngine;

public class Flying_Basics: MonoBehaviour
{


    [Header("Stats")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float obstacleDetectionDistance = 2f;
    [SerializeField] private float avoidanceStrength = 2f;

    [Header("Target")]
    public Transform target;

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
        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}