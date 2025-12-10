using UnityEngine;

public class Cam_test_2 : MonoBehaviour
{
    public Transform tr_camera_Pivot;
    public Transform tr_camera_Position;

    public float speed_cam_Mousse = 1;
    public float speed_cam_ZQSD = 1;
    public float speed_cam_MOLETTE = 1;

    float rotX = 0f; // haut / bas
    float rotY = 0f; // gauche / droite

    void Start()
    {
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

        // --- Rotation au clic droit ---
        if (Input.GetMouseButton(1))
        {
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
            tr_camera_Pivot.Translate(0, -Input.GetAxis("Mouse Y") * Time.deltaTime * speed_cam_Mousse, 0, Space.Self);
            //tr_camera_Pivot.Translate(-Input.GetAxis("Mouse X") * Time.deltaTime * speed_cam_Mousse, 0, 0, Space.Self);
        }

        // --- Zoom molette ---
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
            tr_camera_Position.position += tr_camera_Position.forward * scroll * speed_cam_MOLETTE * 10 * Time.deltaTime;
    }
}
