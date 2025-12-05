using UnityEngine;
using UnityEngine.AI;

public class TeleportAbility : MonoBehaviour
{
    [Header("Activation")]
    public KeyCode activationKey = KeyCode.T;

    [Header("Teleport Settings")]
    public float maxTeleportRange = 10f;
    public float slowTimeScale = 0.2f;

    [Header("Visuals")]
    public GameObject teleportRangePreview;   // grand cercle de portée
    public LayerMask groundLayer;

    private bool isAiming = false;
    private bool isReady = true;

    private Mecha_Inventory inventory;
    private Camera mainCam;
    private NavMeshAgent agent;

    private Vector3 targetPosition; // position finale du TP (invisible)

    private void Start()
    {
        inventory = GetComponent<Mecha_Inventory>();
        mainCam = Camera.main;
        agent = GetComponent<NavMeshAgent>();

        if (teleportRangePreview != null)
            teleportRangePreview.SetActive(false);

        // 🔵 Mise à l’échelle auto de la zone de portée
        if (teleportRangePreview != null)
            teleportRangePreview.transform.localScale = Vector3.one * maxTeleportRange * 2f;
    }

    private void Update()
    {
        // ✅ Activation du mode téléportation
        if (Input.GetKeyDown(activationKey)
            && isReady
            && !isAiming
            && inventory.HasTeleportToken())
        {
            StartTeleportAim();
        }

        // ✅ Pendant la visée
        if (isAiming)
        {
            UpdateTargetPosition();

            // ✅ Clic gauche = téléportation
            if (Input.GetMouseButtonDown(0))
                ConfirmTeleport();

            // ✅ Clic droit = annulation
            if (Input.GetMouseButtonDown(1))
                CancelTeleport();
        }
    }

    // ===========================
    // 🎯 DÉMARRAGE DU MODE TP
    // ===========================
    private void StartTeleportAim()
    {
        isReady = false;
        isAiming = true;

        // ✅ Consomme le token
        inventory.ConsumeTeleportToken();

        // ✅ Ralenti du temps
        Time.timeScale = slowTimeScale;

        // ✅ Affiche uniquement la zone de portée
        teleportRangePreview.SetActive(true);
    }

    // ===========================
    // 🎯 CALCUL POSITION INVISIBLE
    // ===========================
    private void UpdateTargetPosition()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundLayer))
        {
            Vector3 rawTarget = hit.point;

            // ✅ Clamp dans la portée max
            targetPosition = transform.position +
                Vector3.ClampMagnitude(rawTarget - transform.position, maxTeleportRange);
        }
    }

    // ===========================
    // ✅ CONFIRMATION DU TP
    // ===========================
    private void ConfirmTeleport()
    {
        if (agent != null)
            agent.Warp(targetPosition);
        else
            transform.position = targetPosition;

        EndTeleport();
    }

    // ===========================
    // ❌ ANNULATION
    // ===========================
    private void CancelTeleport()
    {
        EndTeleport();
    }

    // ===========================
    // 🔚 FIN DU MODE TP
    // ===========================
    private void EndTeleport()
    {
        isAiming = false;
        isReady = true;

        teleportRangePreview.SetActive(false);

        // ✅ Retour au temps normal
        Time.timeScale = 1f;
    }
}
