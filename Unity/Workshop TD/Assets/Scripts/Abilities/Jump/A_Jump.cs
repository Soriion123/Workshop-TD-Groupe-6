using UnityEngine;

public class A_Jump : MonoBehaviour
{
    [Header("Teleport Settings")]
    public KeyCode activationKey = KeyCode.T;
    public GameObject teleportPreview;     // Zone visuelle au sol
    public LayerMask groundLayer;          // Layer du sol
    public float slowTimeScale = 0.2f;     // Ralenti du temps

    private bool isAiming = false;
    private bool isReady = true;

    private Mecha_Inventory inventory;
    private Camera mainCam;

    private void Start()
    {
        inventory = GetComponent<Mecha_Inventory>();
        mainCam = Camera.main;

        if (teleportPreview != null)
            teleportPreview.SetActive(false);
    }

    private void Update()
    {
        // ✅ Activation du mode TP
        if (Input.GetKeyDown(activationKey)
            && isReady
            && !isAiming
            && inventory.HasTeleportToken())
        {
            StartTeleportAim();
        }

        // ✅ Pendant le mode visée
        if (isAiming)
        {
            UpdatePreviewPosition();

            // ✅ Clic gauche = téléportation
            if (Input.GetMouseButtonDown(0))
            {
                ConfirmTeleport();
            }

            // ✅ Clic droit = annuler
            if (Input.GetMouseButtonDown(1))
            {
                CancelTeleport();
            }
        }
    }

    // =============================
    // 🎯 ACTIVATION DU MODE TP
    // =============================
    private void StartTeleportAim()
    {
        isReady = false;
        isAiming = true;

        // ✅ Consomme le token
        inventory.ConsumeTeleportToken();

        // ✅ Active le ralenti
        Time.timeScale = slowTimeScale;

        // ✅ Affiche la preview
        teleportPreview.SetActive(true);
    }

    // =============================
    // 🎯 UPDATE POSITION PREVIEW
    // =============================
    private void UpdatePreviewPosition()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundLayer))
        {
            teleportPreview.transform.position = hit.point;
        }
    }

    // =============================
    // ✅ CONFIRMATION DU TP
    // =============================
    private void ConfirmTeleport()
    {
        transform.position = teleportPreview.transform.position;

        EndTeleport();
    }

    // =============================
    // ❌ ANNULATION
    // =============================
    private void CancelTeleport()
    {
        EndTeleport();
    }

    // =============================
    // 🔚 FIN DU MODE TP
    // =============================
    private void EndTeleport()
    {
        isAiming = false;
        isReady = true;

        teleportPreview.SetActive(false);

        // ✅ Retour au temps normal
        Time.timeScale = 1f;
    }
}
