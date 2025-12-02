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

    public int Limit_mechas = 5;

    public GameObject Debug_Mechas_Selec;

    public bool Mechas_drag = false;

    //private int test_aaa;
    //public List<int> ID_in_scene_test = new List<int>();

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
                
                if (Input.GetMouseButtonDown(2) & hit.collider.gameObject.GetComponent<Info_Mecha>().mechas_selec)
                {
                    game_manager.gold = game_manager.gold + prefab_mechas[ID_mechas_Spawn].GetComponent<Info_Mecha>().prix / 2;

                    //ID_in_scene_test[ref_id_selec] = 100;

                    GameObject.Destroy(New_Target[ref_id_selec]);
                    
                    New_Target[ref_id_selec] = Debug_Mechas_Selec;
                    test_memori = Debug_Mechas_Selec;
                    Limit_mechas++;
                    
                    GameObject.Destroy(hit.collider.gameObject);
                    return;
                    
                }
                
            }

            if (hit.collider.gameObject.tag == "Sol")
            {
                if (Input.GetMouseButtonDown(0) & test_memori.GetComponent<Info_Mecha>().mechas_selec)
                {
                    New_Target[ref_id_selec].transform.position = cursor.position;
                    mechas_selec = false;
                }

                if (Input.GetMouseButtonUp(1) & cmp_mechas_spawn < Limit_mechas & game_manager.gold >= prefab_mechas[ID_mechas_Spawn].GetComponent<Info_Mecha>().prix & Mechas_drag)
                {
                    Mechas_drag = false;
                    game_manager.gold = game_manager.gold - prefab_mechas[ID_mechas_Spawn].GetComponent<Info_Mecha>().prix;


                    GameObject Mechas_Instantiate = Instantiate(prefab_mechas[ID_mechas_Spawn], new Vector3(cursor.transform.position.x, cursor.transform.position.y + 1, cursor.transform.position.z), Quaternion.identity);

                    New_Target.Add(Mechas_Instantiate.gameObject.GetComponent<Mechas_Move_Test>().target = new GameObject("Target"));

                    New_Target[New_Target.Count - 1].transform.position = cursor.transform.position;

                    Mechas_Instantiate.gameObject.GetComponent<Info_Mecha>().id = cmp_mechas_spawn;

                    cmp_mechas_spawn++;

                    /*
                    if (ID_in_scene_test.Count == 5)
                    {
                        for (int i = 0; i < ID_in_scene_test.Count; i++)
                        {
                            if (ID_in_scene_test[i] == 100)
                            {
                                ID_in_scene_test[i] = cmp_mechas_spawn;
                                return;
                            }
                        }
                    }
                    else
                    {
                        ID_in_scene_test.Add(cmp_mechas_spawn);
                    }
                    */

                }

            }

            if (hit.collider.tag == "Boite 1")
            {
                if (Input.GetMouseButtonDown(1)) { ID_mechas_Spawn = 0; Mechas_drag = true; }
                if (Input.GetMouseButtonUp(1)) { Mechas_drag = false; }

            }
            if (hit.collider.tag == "Boite 2")
            {
                if (Input.GetMouseButtonDown(1)) { ID_mechas_Spawn = 1; Mechas_drag = true; }
                if (Input.GetMouseButtonUp(1)) { Mechas_drag = false; }
            }
            if (hit.collider.tag == "Boite 3")
            {
                if (Input.GetMouseButtonDown(1)) { ID_mechas_Spawn = 2; Mechas_drag = true; }
                if (Input.GetMouseButtonUp(1)) { Mechas_drag = false; }
            }

            
            /*
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                recherche_ID_en_vie(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                recherche_ID_en_vie(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                recherche_ID_en_vie(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                recherche_ID_en_vie(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                recherche_ID_en_vie(4);
            }
            */

        }
    }
    /*
    public void recherche_ID_en_vie(int touche)
    {
        ref_id_selec = ID_in_scene_test[touche];
    }
    */
}