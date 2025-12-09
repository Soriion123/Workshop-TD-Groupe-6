using UnityEngine;
using UnityEngine.AI;

public class A_Jump : MonoBehaviour
{
    [Header("Activation")]
    private KeyCode activationKey = KeyCode.F;

    [Header("Teleport Settings")]
    public float maxTeleportRange = 10f;
    public float slowTimeScale = 0.2f;

    [Header("Visuals")]
    public GameObject teleportRangePreview;   // cercle de portée (ENFANT du mecha)

    private bool isAiming = false;
    private bool isReady = true;

    private Mecha_AbilityManager abilityManager;
    private NavMeshAgent agent;
    private Info_Mecha info;

    private Vector3 targetPosition;

    private GameObject cliqueur;
    private Transform mecha;

    private void Start()
    {
        abilityManager = GetComponent<Mecha_AbilityManager>();

        agent = GetComponent<NavMeshAgent>();
        info = GetComponent<Info_Mecha>();
        mecha = transform;

        // ✅ Récupération automatique du curseur
        cliqueur = GameObject.Find("Cursor");

        if (teleportRangePreview != null)
        {
            teleportRangePreview.SetActive(false);
            teleportRangePreview.transform.localScale = Vector3.one * maxTeleportRange * 2f;
        }
    }

    private void Update()
    {
        // ✅ Seulement le mecha sélectionné
        if (info != null && info.mechas_selec)
        {
            if (Input.GetKeyDown(activationKey)
                && isReady
                && !isAiming)
            {
                StartTeleportAim();
            }
        }

        if (isAiming)
        {
            UpdateTargetPositionFromCliqueur();

            if (Input.GetMouseButtonDown(0))
                ConfirmTeleport();
        }
    }

    private void StartTeleportAim()
    {
        isReady = false;
        isAiming = true;

        Time.timeScale = slowTimeScale;

        teleportRangePreview.SetActive(true);
    }

    private void UpdateTargetPositionFromCliqueur()
    {
        // ✅ Position du curseur SANS raycast
        Vector3 rawTarget = cliqueur.transform.position;

        // ✅ Clamp dans la portée max
        targetPosition = mecha.position +
            Vector3.ClampMagnitude(rawTarget - mecha.position, maxTeleportRange);
    }

    private void ConfirmTeleport()
    {
        Vector3 finalPos = new Vector3(
            targetPosition.x,
            targetPosition.y + 0.5f,
            targetPosition.z
        );

        if (agent != null)
        {
            agent.ResetPath();     // 🔥 empêche le retour arrière
            agent.Warp(finalPos); // ✅ vrai teleport propre
        }
        else
        {
            mecha.position = finalPos;
        }

        EndTeleport();
    }

    private void EndTeleport()
    {
        isAiming = false;
        isReady = true;

        teleportRangePreview.SetActive(false);
        Time.timeScale = 1f;
        // consommation de l'ability
        abilityManager.ConsumeAbility();
    }
}
