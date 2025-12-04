using UnityEngine;

public class AutoDeath_Box : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Mecha_Inventory inventory = other.GetComponent<Mecha_Inventory>();

        if (inventory != null)
        {
            inventory.AddAutoDeathToken();

            // Effets / son / particules ici
            Destroy(gameObject);
        }
    }
}
