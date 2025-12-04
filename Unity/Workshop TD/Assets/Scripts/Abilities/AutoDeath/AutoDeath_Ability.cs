using UnityEngine;
using System.Collections;

public class AutoDeath_Ability : MonoBehaviour
{
    public GameObject AutoDeath_Zone;
    public KeyCode activationKey = KeyCode.R;

    private bool isReady = true;
    private Mecha_Inventory mecha_Inventory;

    private GameObject cliqueur;

    private void Start()
    {
        cliqueur = GameObject.Find("Cliquer");

        mecha_Inventory = GetComponent<Mecha_Inventory>();

        if (AutoDeath_Zone != null)
            AutoDeath_Zone.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(activationKey)
            && isReady
            && mecha_Inventory.HasAutoDeathToken())
        {
            StartCoroutine(ActivateAndDie());
        }
    }

    private IEnumerator ActivateAndDie()
    {
        isReady = false;

        // Consomme la clé
        mecha_Inventory.ConsumeAutoDeathToken();

        // Active la zone
        AutoDeath_Zone.SetActive(true);

        // Attente de 1 seconde
        yield return new WaitForSeconds(1f);

        // Destruction du mecha
        cliqueur.GetComponent<Cliqueur>().Mechas_Dead(gameObject);
    }
}
