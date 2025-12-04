using UnityEngine;

public class SlowAbility : MonoBehaviour
{
    [Header("Ability Settings")]
    public GameObject slowZone;
    public float activeTime = 3f;
    public KeyCode activationKey = KeyCode.F;

    private bool isActive = false;

    private Mecha_Inventory mecha_Inventory;

    private void Start()
    {
        mecha_Inventory = GetComponent<Mecha_Inventory>();

        if (slowZone != null)
            slowZone.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(activationKey) && !isActive && mecha_Inventory.HasSlowToken())
        {
            StartCoroutine(ActivateSlowZone());
        }
    }

    private System.Collections.IEnumerator ActivateSlowZone()
    {
        isActive = true;

        // Consommer 1 token
        mecha_Inventory.ConsumeSlowToken();

        // Activer la zone
        slowZone.SetActive(true);

        yield return new WaitForSeconds(activeTime);

        // Désactiver la zone
        slowZone.SetActive(false);

        isActive = false;
    }
}
