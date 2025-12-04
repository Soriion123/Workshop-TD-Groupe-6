using UnityEngine;

public class AOE_Ability : MonoBehaviour
{
    public GameObject AOE_Zone;
    public KeyCode activationKey = KeyCode.E;

    private bool isReady = true;
    private Mecha_Inventory mecha_Inventory;

    private void Start()
    {
        mecha_Inventory = GetComponent<Mecha_Inventory>();

        if (AOE_Zone != null)
            AOE_Zone.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(activationKey)
            && isReady
            && mecha_Inventory.HasAOEToken()) // si tu ajoutes ce token
        {
            ActivateExplosion();
        }
    }

    private void ActivateExplosion()
    {
        isReady = false;

        // Consomme la clé
        mecha_Inventory.ConsumeAOEToken();

        // Active la zone (elle explose toute seule)
        AOE_Zone.SetActive(true);

        isReady = true; // ici pas de cooldown
    }
}
