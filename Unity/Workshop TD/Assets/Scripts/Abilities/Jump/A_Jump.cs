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

    public GameObject cliqueur;

    private bool isAiming = false;
    private bool isReady = true;

    private Mecha_Inventory inventory;
    private NavMeshAgent agent;

    private Vector3 targetPosition;

    public Transform mecha;

    private void Start()
    {
        inventory = GetComponent<Mecha_Inventory>();
        agent = GetComponent<NavMeshAgent>();
        mecha = GetComponent<Transform>();

        // ✅ Récupération automatique du cliqueur
        cliqueur = GameObject.Find("Cursor");

        if (teleportRangePreview != null)
        {
            teleportRangePreview.SetActive(false);
            teleportRangePreview.transform.localScale = Vector3.one * maxTeleportRange * 2f;
        }


    }

    private void Update()
    {



        if (Input.GetKeyDown(activationKey) & isReady & !isAiming & inventory.HasTeleportToken())
        {
            StartTeleportAim();
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

        inventory.ConsumeTeleportToken();
        Time.timeScale = slowTimeScale;

        teleportRangePreview.SetActive(true);
    }

    private void UpdateTargetPositionFromCliqueur()
    {

        targetPosition = cliqueur.transform.position;

    }


    // ===========================
    private void ConfirmTeleport()
    {
        if (agent != null)
        {
            agent.ResetPath(); // 🔥 Stop le déplacement en cours
            agent.Warp(new Vector3(
                targetPosition.x,
                targetPosition.y + 0.5f,
                targetPosition.z
            ));
        }
        else
        {
            // sécurité au cas où
            mecha.position = new Vector3(
                targetPosition.x,
                targetPosition.y + 0.5f,
                targetPosition.z
            );
        }

        EndTeleport();
    }

    private void EndTeleport()
    {
        isAiming = false;
        isReady = true;

        teleportRangePreview.SetActive(false);
        Time.timeScale = 1f;
    }
}
