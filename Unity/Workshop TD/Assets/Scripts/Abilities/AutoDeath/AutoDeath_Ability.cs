using UnityEngine;
using System.Collections;

public class AutoDeath_Ability : MonoBehaviour
{
    public GameObject AutoDeath_ZonePrefab;   // ✅ PREFAB ici
    private KeyCode activationKey = KeyCode.Space;

    private bool isActive = false;

    private Mecha_AbilityManager abilityManager;
    private GameObject cliqueur;
    private Info_Mecha info;

    private void Start()
    {
        info = GetComponent<Info_Mecha>();

        abilityManager = GetComponent<Mecha_AbilityManager>();

        cliqueur = GameObject.Find("Cliquer");
    }

    private void Update()
    {
        if (info != null && info.mechas_selec)
        {
            if (Input.GetKeyDown(activationKey)
                && !isActive) 
            {
                StartCoroutine(ActivateAndDie());
            }
        }
    }

    private IEnumerator ActivateAndDie()
    {
        isActive = true;

        // ✅ INSTANCIATION DE L’EXPLOSION
        GameObject explosion = Instantiate(
            AutoDeath_ZonePrefab,
            transform.position,
            Quaternion.identity
        );
        AudioManager.Instance.Play("AutoDeath");
        // (optionnel) destruction auto de la zone après 2s
        Destroy(explosion, 2f);

        // ✅ Attends que l’explosion fasse ses dégâts
        yield return new WaitForSeconds(1f);

        // ✅ Tue le mecha
        cliqueur.GetComponent<Cliqueur>().Mechas_Dead(gameObject);

        // consommation de l'ability
        abilityManager.ConsumeAbility();
    }
}
