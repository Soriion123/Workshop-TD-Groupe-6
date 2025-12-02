using UnityEngine;
using UnityEngine.UIElements;

public class Camera_move : MonoBehaviour
{

    public Transform tr_camera_Pivot;
    public Transform tr_camera_Position;

    public float speed_cam_Mousse = 1;
    public float speed_cam_ZQSD = 1;
    public float speed_cam_MOLETTE = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // Gauche
        if (Input.GetKey(KeyCode.A))
        {
            tr_camera_Pivot.Rotate(0, speed_cam_ZQSD * Time.deltaTime , 0,Space.World);
        }

        // Droit
        if (Input.GetKey(KeyCode.D))
        {
            tr_camera_Pivot.Rotate(0, -speed_cam_ZQSD * Time.deltaTime, 0,Space.World);   
        }

        // Haut
        if (Input.GetKey(KeyCode.W))
        {
            tr_camera_Pivot.Rotate(speed_cam_ZQSD * Time.deltaTime, 0, 0, Space.Self);
        }

        // Bas
        if (Input.GetKey(KeyCode.S))
        {
            tr_camera_Pivot.Rotate(-speed_cam_ZQSD * Time.deltaTime, 0, 0, Space.Self);
        }
        


        if (Input.GetMouseButton(1))
        {
            tr_camera_Pivot.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * speed_cam_Mousse, 0, Space.World);

            tr_camera_Pivot.Rotate(-Input.GetAxis("Mouse Y") * Time.deltaTime * speed_cam_Mousse, 0, 0, Space.Self);
        }


        if (Input.GetMouseButton(2))
        {
            tr_camera_Pivot.Translate(0, -Input.GetAxis("Mouse Y") * Time.deltaTime * speed_cam_Mousse, 0,Space.Self);
            tr_camera_Pivot.Translate(-Input.GetAxis("Mouse X") * Time.deltaTime * speed_cam_Mousse, 0, 0,Space.Self);
        }


        // Cam recule
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            tr_camera_Position.position += tr_camera_Position.forward * -speed_cam_MOLETTE * 10 * Time.deltaTime;
        }

        // Cam avance
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            tr_camera_Position.position += tr_camera_Position.forward * speed_cam_MOLETTE * 10 * Time.deltaTime;
        }

    }
}
