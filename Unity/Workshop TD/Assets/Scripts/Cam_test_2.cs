using UnityEngine;

public class Cam_test_2 : MonoBehaviour
{
    // Code avec de aide de l'IA
    
    public Cliqueur cliqueur;

    public Transform tr_camera_Pivot;
    public Transform tr_camera_Position;

    public float speed_cam_Mousse = 1;
    public float speed_cam_ZQSD = 1;
    public float speed_cam_MOLETTE = 1;

    float rotX = 0f; // haut / bas
    float rotY = 0f; // gauche / droite


    public Quaternion Memori_cam_1;
    public Vector3 Memori_cam_2;

    public bool clic_cam = false;

    void Start()
    {
        Memori_cam_1 = tr_camera_Pivot.rotation;
        Memori_cam_2 = tr_camera_Position.position;

        Vector3 e = tr_camera_Pivot.eulerAngles;
        rotX = e.x;  // vertical
        rotY = e.y;  // horizontal
    }

    void Update()
    {
        // --- Rotations ZQSD (inchangées) ---
        if (Input.GetKey(KeyCode.A)) rotY += speed_cam_ZQSD * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) rotY -= speed_cam_ZQSD * Time.deltaTime;
        if (Input.GetKey(KeyCode.W)) rotX += speed_cam_ZQSD * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) rotX -= speed_cam_ZQSD * Time.deltaTime;

        if (Input.GetMouseButtonUp(2) || Input.GetMouseButtonUp(1))
        {
            clic_cam = false ;
            Cursor.SetCursor(cliqueur.Image_souris[2], new Vector2(0, 166), CursorMode.Auto);
        }

        // --- Rotation au clic droit ---
        if (Input.GetMouseButton(1))
        {
            Cursor.SetCursor(cliqueur.Image_souris[1], new Vector2(0, 166), CursorMode.Auto);
            clic_cam = true;

            // Gauche / droite -> Y
            rotY += Input.GetAxis("Mouse X") * Time.deltaTime * speed_cam_Mousse;

            // Haut / bas -> X
            rotX -= Input.GetAxis("Mouse Y") * Time.deltaTime * speed_cam_Mousse;
        }

        // --- CLAMP EXACT SELON TA DEMANDE ---
        rotY = Mathf.Clamp(rotY, 0f, 90f);     // Clic droit gauche/droite
        rotX = Mathf.Clamp(rotX, -20f, 60f);    // Clic droit haut/bas

        // Appliquer la rotation
        tr_camera_Pivot.rotation = Quaternion.Euler(rotX, rotY, 0f);

        // --- Middle click (pan) ---
        if (Input.GetMouseButton(2))
        {
            Cursor.SetCursor(cliqueur.Image_souris[1], new Vector2(0, 166), CursorMode.Auto);
            clic_cam = true;

            tr_camera_Pivot.Translate(
                0,
                -Input.GetAxis("Mouse Y") * Time.deltaTime * speed_cam_Mousse,
                0,
                Space.Self
            );

            // ?? CLAMP PAN (LOCAL POSITION)
            Vector3 pos = tr_camera_Pivot.localPosition;

            pos.x = Mathf.Clamp(pos.x, -15f, -10f); // X
            pos.y = Mathf.Clamp(pos.y, 0f, 60f);    // Y
            pos.z = Mathf.Clamp(pos.z, 80f, 85f);   // Z

            tr_camera_Pivot.localPosition = pos;
        }

        // --- Zoom molette ---
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            tr_camera_Position.position +=
                tr_camera_Position.forward * scroll * speed_cam_MOLETTE * 10f * Time.deltaTime;

            // ?? CLAMP ZOOM (LOCAL POSITION)
            Vector3 zoomPos = tr_camera_Position.localPosition;

            zoomPos.x = Mathf.Clamp(zoomPos.x, 2f, 10f);       // X
            zoomPos.y = Mathf.Clamp(zoomPos.y, -10f, 30f);     // Y
            zoomPos.z = Mathf.Clamp(zoomPos.z, -125f, -15f);   // Z

            tr_camera_Position.localPosition = zoomPos;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // Reset transforms

            tr_camera_Pivot.rotation = Quaternion.Slerp(tr_camera_Pivot.rotation, Memori_cam_1, Time.timeScale * 3);

            //tr_camera_Pivot.rotation = Memori_cam_1;
            tr_camera_Position.position = Memori_cam_2;

            // Reset des variables internes sinon la rotation est écrasée ensuite
            Vector3 e = Memori_cam_1.eulerAngles;
            rotX = e.x;
            rotY = e.y;
        }

    }
}
