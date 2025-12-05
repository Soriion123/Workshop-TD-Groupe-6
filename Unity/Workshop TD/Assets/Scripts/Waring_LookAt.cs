using UnityEngine;

public class Waring_LookAt : MonoBehaviour
{


    public Canvas canvas;
    public GameObject cameraScene;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.rotation = cameraScene.transform.rotation;
    }
}
