using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Cliqueur : MonoBehaviour
{

    // Cursor
    public float profondeur_detection = 1000;
    public Transform cursor;
    public LayerMask Mask;
    public bool mechas_selec;

    // Mechas
    public List<GameObject> New_Target = new List<GameObject>();
    
    public int ref_id_selec;
    public GameObject test_memori;

    public GameObject[] prefab_mechas;
    public int ID_mechas_Spawn;

    public int cmp_mechas_spawn = 0;

    // Game Manageur
    public GameManager game_manager;

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
                    New_Target[ref_id_selec].transform.position = cursor.position;
                    mechas_selec = false;
                }

                if (Input.GetMouseButtonDown(1) & cmp_mechas_spawn < 5 & game_manager.gold > prefab_mechas[ID_mechas_Spawn].GetComponent<Info_Mecha>().prix)
                {

                    game_manager.gold = game_manager.gold - prefab_mechas[ID_mechas_Spawn].GetComponent<Info_Mecha>().prix;

                    GameObject Mechas_Instantiate = Instantiate(prefab_mechas[ID_mechas_Spawn], cursor.transform.position, Quaternion.identity);

                    New_Target.Add(Mechas_Instantiate.gameObject.GetComponent<Mechas_Move_Test>().target = new GameObject("Target"));

                    New_Target[New_Target.Count - 1].transform.position = cursor.transform.position;

                    Mechas_Instantiate.gameObject.GetComponent<Info_Mecha>().id = cmp_mechas_spawn;

                    cmp_mechas_spawn++;

                }

            }

            if (hit.collider.name == "Boite 0")
            {
                if (Input.GetMouseButtonDown(0)) { ID_mechas_Spawn = 0; }
                
            }
            if (hit.collider.name == "Boite 1")
            {
                if (Input.GetMouseButtonDown(0)) { ID_mechas_Spawn = 1; }

            }
            if (hit.collider.name == "Boite 2")
            {
                if (Input.GetMouseButtonDown(0)) { ID_mechas_Spawn = 2; }
            }
        }
    }
}