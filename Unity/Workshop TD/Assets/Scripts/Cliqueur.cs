using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Cliqueur : MonoBehaviour
{
    public float profondeur_detection = 1000;
    public Transform cursor;

    public LayerMask Mask;

    public bool mechas_selec;
    public Transform[] New_Target;
    public int ref_id_selec;
    public GameObject test_memori;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * profondeur_detection, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction * profondeur_detection, out hit, profondeur_detection, Mask))
        {
            cursor.position = hit.point;

            if (hit.collider.gameObject.tag == "Mechas")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponent<Info_Mecha>().mechas_selec = true;
                    ref_id_selec = hit.collider.gameObject.GetComponent<Info_Mecha>().id;
                    test_memori = hit.collider.gameObject;
                }
            }

            if (hit.collider.gameObject.tag == "Sol")
            {
                if (Input.GetMouseButtonDown(0) & test_memori.GetComponent<Info_Mecha>().mechas_selec)
                {
                    New_Target[ref_id_selec].position = cursor.position;
                    mechas_selec = false;
                }
            }
        }
    }
}