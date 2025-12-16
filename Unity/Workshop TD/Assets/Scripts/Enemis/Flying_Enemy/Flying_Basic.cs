using UnityEngine;

public class Flying_Basic: Flying_Enemy
{


    [Header("Stats")]
    [SerializeField] public float speed = 5f;
    [HideInInspector] public float originalSpeed;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private int goldReward = 1;   // 💰 or gagné à la mort
    [SerializeField] private GameObject VFX_Death;
    public float NexusDamage = 5f; // dégâts infligés à l'objectif

    [Header("Target")]
    //public Transform target;

    private float currentHealth;

    private float obstacleCheckRadius = 4f;  // rayon de la sphère
    private float climbSpeed = 4f;           // vitesse pour monter
    private float climbHeight = 5f;          // hauteur d’évitement

    private void Start()
    {
        originalSpeed = speed;
        currentHealth = maxHealth;
    }

   

    private void Update()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;

        // --- CHECK OBSTACLES DANS LA SPHÈRE ---
        Collider[] hits = Physics.OverlapSphere(transform.position, obstacleCheckRadius);

        bool wallDetected = false;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Wall"))
            {
                wallDetected = true;
                break;
            }
        }

        // --- SI MUR DÉTECTÉ → MONTER ---
        if (wallDetected)
        {
            Vector3 climbTarget = transform.position + Vector3.up * climbHeight;

            transform.position = Vector3.Lerp(
                transform.position,
                climbTarget,
                Time.deltaTime * climbSpeed
            );
        }
        else
        {
            // Déplacement normal
            transform.position += direction * speed * Time.deltaTime;
        }

        // Vérification objectif
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
        Instantiate(VFX_Death, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.02f);

        AudioManager.Instance.Play("MortOpps");
        GameManager.instance.AddGold(goldReward);
    }
}