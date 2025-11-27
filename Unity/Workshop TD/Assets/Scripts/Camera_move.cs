using UnityEngine;
using UnityEngine.UIElements;

public class Camera_move : MonoBehaviour
{

    public Transform tr_camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            tr_camera.Rotate(0, 1, 0);
        }

        if (Input.GetKey(KeyCode.E))
        {
            tr_camera.Rotate(0, -1, 0);   
        }
    }
}
