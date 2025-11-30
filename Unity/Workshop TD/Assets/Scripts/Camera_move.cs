using UnityEngine;
using UnityEngine.UIElements;

public class Camera_move : MonoBehaviour
{

    public Transform tr_camera_Pivot;
    public Transform tr_camera_Position;

    public float speed_cam = 1;

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
            tr_camera_Pivot.Rotate(0, speed_cam , 0,Space.World);
        }

        // Droit
        if (Input.GetKey(KeyCode.D))
        {
            tr_camera_Pivot.Rotate(0, -speed_cam, 0,Space.World);   
        }

        // Haut
        if (Input.GetKey(KeyCode.W))
        {
            tr_camera_Pivot.Rotate(speed_cam, 0, 0, Space.Self);
        }

        // Bas
        if (Input.GetKey(KeyCode.S))
        {
            tr_camera_Pivot.Rotate(-speed_cam, 0, 0, Space.Self);
        }

        // Cam recule
        if (Input.GetKey(KeyCode.Q))
        {
            tr_camera_Position.position += tr_camera_Position.forward * -speed_cam;
        }

        // Cam avance
        if (Input.GetKey(KeyCode.E))
        {
            tr_camera_Position.position += tr_camera_Position.forward * speed_cam;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            tr_camera_Pivot.Translate(0, speed_cam,0,Space.World);
        }

        if (Input.GetKey(KeyCode.C))
        {
            tr_camera_Pivot.Translate(0, -speed_cam, 0, Space.World);
        }
    }
}
