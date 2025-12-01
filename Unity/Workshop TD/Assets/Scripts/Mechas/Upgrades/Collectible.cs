using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Entity"))
        {
            // Action quand l'entité récupère l’objet
            Debug.Log("Objet récupéré !");
            Destroy(gameObject);
        }
    }
}
