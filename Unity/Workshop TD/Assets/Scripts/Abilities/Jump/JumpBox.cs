using UnityEngine;

public class JumpBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Mecha_Inventory inventory = other.GetComponent<Mecha_Inventory>();

        if (inventory != null)
        {
            inventory.AddTeleportToken();

            // Effets / son / particules ici
            Destroy(gameObject);
        }
    }
}
