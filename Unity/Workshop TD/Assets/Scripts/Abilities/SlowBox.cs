using UnityEngine;

public class SlowBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Mecha_Inventory inventory = other.GetComponent<Mecha_Inventory>();

        if (inventory != null)
        {
            inventory.AddSlowToken();

            // Effets / son / particules ici
            Destroy(gameObject);
        }
    }
}
