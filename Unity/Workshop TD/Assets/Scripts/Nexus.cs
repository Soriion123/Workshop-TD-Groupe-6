using UnityEngine;

public class Nexus : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 100f;
    public float currenthealth;


    public GameObject Canvas_Dead;
    public GameObject Cliquer;

    public Menu_Manageur Menu_Manageur;

    private void Start()
    {
        currenthealth = maxHealth;
        // Debug.Log("Vie Nexus : " + currenthealth); // Affiche la vie initiale
    }

    // Appelé quand un ennemi touche le bâtiment
    public void TakeDamage(float amount)
    {
        currenthealth -= amount;
        AudioManager.Instance.Play("NexusHit");

        // 🔥 Affichage dans la console
        // Debug.Log("Vie Nexus : " + currenthealth);

        if (currenthealth <= 0)
        {
            //Canvas_Dead.SetActive(true);
            Menu_Manageur.Last_screen(Canvas_Dead);
            //Time.timeScale = 0;
            Cliquer.SetActive(false);

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
