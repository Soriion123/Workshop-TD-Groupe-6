using UnityEngine;

public class AOEBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Mecha_Inventory inventory = other.GetComponent<Mecha_Inventory>();

        if (inventory != null)
        {
            inventory.AddAOEToken();

            // Effets / son / particules ici
            Destroy(gameObject);
        }
    }
}
