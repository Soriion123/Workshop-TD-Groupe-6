using UnityEngine;

public class Nexus : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 100f;
    public float currenthealth;

    private void Start()
    {
        currenthealth = maxHealth;
        Debug.Log("Vie Nexus : " + currenthealth); // Affiche la vie initiale
    }

    // Appelé quand un ennemi touche le bâtiment
    public void TakeDamage(float amount)
    {
        currenthealth -= amount;

        // 🔥 Affichage dans la console
        Debug.Log("Vie Nexus : " + currenthealth);

        if (currenthealth <= 0)
        {
            Die();
            Destroy(gameObject);
            return;
        }
    }

    void Die()
    {
        Debug.Log("❌ Le Nexus a été détruit !");
        // Tu pourra mettre un game over ici plus tard
    }
}
