using UnityEngine;
using System.Collections;

public class AOE_Ability : MonoBehaviour
{
    [Header("AOE Settings")]
    public GameObject AOE_ZonePrefab;   // ✅ PREFAB ici
    private KeyCode activationKey = KeyCode.F;
    public float cooldown = 0.5f;

    private bool isReady = true;
    private Mecha_AbilityManager abilityManager;
    private Info_Mecha info;

    private void Start()
    {
        abilityManager = GetComponent<Mecha_AbilityManager>();

        info = GetComponent<Info_Mecha>();
    }

    private void Update()
    {
        // ✅ Uniquement le mecha sélectionné
        if (info != null && info.mechas_selec)
        {
            if (Input.GetKeyDown(activationKey)
                && isReady)
            {
                StartCoroutine(ActivateAOE());
            }
        }
    }

    private IEnumerator ActivateAOE()
    {
        isReady = false;

        // ✅ Instancie la zone à la position du mecha
        GameObject aoe = Instantiate(
            AOE_ZonePrefab,
            transform.position,
            Quaternion.identity
        );

        // ✅ Nettoyage automatique
        Destroy(aoe, 1f);

        yield return new WaitForSeconds(cooldown);

        isReady = true;

        // consommation de l'ability
        abilityManager.ConsumeAbility();
    }
}
