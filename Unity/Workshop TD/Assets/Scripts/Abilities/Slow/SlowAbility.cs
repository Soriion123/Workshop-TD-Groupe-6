using UnityEngine;
using System.Collections;

public class SlowAbility : MonoBehaviour
{
    [Header("Ability Settings")]
    public GameObject slowZonePrefab;   // ✅ DOIT être un prefab
    public float activeTime = 3f;
    private KeyCode activationKey = KeyCode.F;

    private bool isActive = false;

    private Mecha_AbilityManager abilityManager;
    private GameObject slowZoneInstance;

    public Info_Mecha info;

    private void Awake()
    {
        // ✅ On récupère le script de sélection
        Info_Mecha info = GetComponent<Info_Mecha>();

        
        abilityManager = GetComponent<Mecha_AbilityManager>();

        // ✅ Suppression de toute zone déjà présente (sécurité anti-bug prefab)
        A_Slow existingZone = GetComponentInChildren<A_Slow>();
        if (existingZone != null)
        {
            Debug.LogWarning($"[{name}] Une zone slow existait déjà, suppression pour éviter partage !");
            Destroy(existingZone.gameObject);
        }

        // ✅ Création d'une instance UNIQUE
        slowZoneInstance = Instantiate(
            slowZonePrefab,
            transform.position,
            Quaternion.identity,
            transform
        );

        slowZoneInstance.name = "SlowZone_Instance_" + gameObject.name;
        slowZoneInstance.SetActive(false);

    }

    private void Update()
    {
       

        // ✅ Seul le mecha sélectionné peut activer l’ability
        if (info != null && info.mechas_selec)
        {
            if (Input.GetKeyDown(activationKey)
                && !isActive)
               
            {
                StartCoroutine(ActivateSlowZone());
            }
        }
    }


    private IEnumerator ActivateSlowZone()
    {
        isActive = true;

        slowZoneInstance.SetActive(true);

        yield return new WaitForSeconds(activeTime);

        slowZoneInstance.SetActive(false);

        isActive = false;

        // consommation de l'ability
        abilityManager.ConsumeAbility();
    }
}
