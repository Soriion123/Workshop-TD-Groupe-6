using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public Material[] Material_cursor;
    public Material Material_memori_target;

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

    public GameObject[] List_de_mechaas;

    public List<GameObject> UI_mechas_scene = new List<GameObject>();


    // UpGrade
    public bool Upgrade_drag = false;
    public bool Trash_drag = false;


    public GameObject Prefab_Target;


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
                // Selection de ID du mechas
                if (Input.GetMouseButtonDown(0))
                {
                    //List_de_mechaas = GameObject.FindGameObjectsWithTag("Mechas");

                    //print(List_de_mechaas);


                    New_selection(hit.collider.gameObject);


                    ref_id_selec = hit.collider.gameObject.GetComponent<Info_Mecha>().id;

                    test_memori = hit.collider.gameObject;

                    New_Target[ref_id_selec].GetComponentInChildren<MeshRenderer>().material = Material_cursor[3];
                }
                
                // Vente du mechas
                if (Input.GetMouseButtonDown(0) & hit.collider.gameObject.GetComponent<Info_Mecha>().mechas_selec)
                {
                    Trash_drag = true;
                    if (hit.collider.gameObject.name == "Mechas_Air(Clone)") { cursor.GetComponent<MeshRenderer>().material = Material_cursor[0]; }
                    if (hit.collider.gameObject.name == "Mechas_All(Clone)") { cursor.GetComponent<MeshRenderer>().material = Material_cursor[1]; }
                    if (hit.collider.gameObject.name == "Mechas_Ground(Clone)") { cursor.GetComponent<MeshRenderer>().material = Material_cursor[2]; }
                }

                if (Input.GetMouseButtonUp(0) & Upgrade_drag)
                {

                    // UpGrade
                    if (hit.collider.gameObject.name == "Mechas_Air(Clone)" & game_manager.gold >= 1934)
                    {
                        game_manager.gold = game_manager.gold - 1934;

                        hit.collider.gameObject.GetComponent<Mechas_Air>().range = hit.collider.gameObject.GetComponent<Mechas_Air>().range * 1.5f ;
                        hit.collider.gameObject.GetComponent<Mechas_Air>().turnSpeed = hit.collider.gameObject.GetComponent<Mechas_Air>().turnSpeed * 1.5f;
                        hit.collider.gameObject.GetComponent<Mechas_Air>().fireRate = hit.collider.gameObject.GetComponent<Mechas_Air>().fireRate * 1.5f;
                        
                        hit.collider.gameObject.GetComponent<Info_Mecha>().Niveau_UpGrade++;

                    }

                    if (hit.collider.gameObject.name == "Mechas_All(Clone)" & game_manager.gold >= 2021)
                    {
                        game_manager.gold = game_manager.gold - 2021;

                        hit.collider.gameObject.GetComponent<Mechas_All>().range = hit.collider.gameObject.GetComponent<Mechas_All>().range * 1.5f;
                        hit.collider.gameObject.GetComponent<Mechas_All>().turnSpeed = hit.collider.gameObject.GetComponent<Mechas_All>().turnSpeed * 1.5f;
                        hit.collider.gameObject.GetComponent<Mechas_All>().fireRate = hit.collider.gameObject.GetComponent<Mechas_All>().fireRate * 1.5f;
                        
                        hit.collider.gameObject.GetComponent<Info_Mecha>().Niveau_UpGrade++;
                    }

                    if (hit.collider.gameObject.name == "Mechas_Ground(Clone)" & game_manager.gold >= 1122)
                    {
                        game_manager.gold = game_manager.gold - 1122;

                        hit.collider.gameObject.GetComponent<Mechas_Ground>().range = hit.collider.gameObject.GetComponent<Mechas_Ground>().range * 1.5f;
                        hit.collider.gameObject.GetComponent<Mechas_Ground>().turnSpeed = hit.collider.gameObject.GetComponent<Mechas_Ground>().turnSpeed * 1.5f;
                        hit.collider.gameObject.GetComponent<Mechas_Ground>().fireRate = hit.collider.gameObject.GetComponent<Mechas_Ground>().fireRate * 1.5f;

                        hit.collider.gameObject.GetComponent<Info_Mecha>().Niveau_UpGrade++;
                    }

                    // Sol Aire * 1.5
                    // All * 1.4

                    // Sol prix 1122
                    // Aire prix 1934
                    // All prix 2021

                }


            }

            if (hit.collider.gameObject.tag == "Sol")
            {
                
                // Selection New Target Mechas 
                if (Input.GetMouseButtonDown(0) & test_memori.GetComponent<Info_Mecha>().mechas_selec)
                {
                    New_Target[ref_id_selec].transform.position = cursor.position;
                }

                // Creation Mechas "Drop" 
                if (Input.GetMouseButtonUp(0) & cmp_mechas_spawn < Limit_mechas & game_manager.gold >= prefab_mechas[ID_mechas_Spawn].GetComponent<Info_Mecha>().prix & Mechas_drag)
                {
                    Mechas_drag = false;
                    game_manager.gold = game_manager.gold - prefab_mechas[ID_mechas_Spawn].GetComponent<Info_Mecha>().prix;

                    cursor.GetComponent<MeshRenderer>().material = Material_cursor[3];

                    GameObject Mechas_Instantiate = Instantiate(prefab_mechas[ID_mechas_Spawn], new Vector3(cursor.transform.position.x, cursor.transform.position.y + 1, cursor.transform.position.z), Quaternion.identity);

                    New_Target.Add(Mechas_Instantiate.gameObject.GetComponent<Mechas_Move_Test>().target = Instantiate(Prefab_Target));

                    New_Target[New_Target.Count - 1].transform.position = cursor.transform.position;

                    Mechas_Instantiate.gameObject.GetComponent<Info_Mecha>().id = cmp_mechas_spawn;

                    cmp_mechas_spawn++;

                    if (UI_mechas_scene.Count == 5)
                    {
                        for (int i = 0; i < UI_mechas_scene.Count; i++)
                        {
                            if (UI_mechas_scene[i] == null)
                            {
                                UI_mechas_scene[i] = Mechas_Instantiate;

                                Mechas_Instantiate.gameObject.GetComponent<Info_Mecha>().id_ui = i;
                                
                                return;
                            }
                        }
                    }
                    else
                    {
                        UI_mechas_scene.Add(Mechas_Instantiate);

                        Mechas_Instantiate.gameObject.GetComponent<Info_Mecha>().id_ui = cmp_mechas_spawn - 1;
                    }

                    New_Target[New_Target.Count - 1].GetComponentInChildren<MeshRenderer>().material = Material_cursor[ID_mechas_Spawn];

                }

            }


            if (hit.collider.tag == "Trash")
            {
                if (Input.GetMouseButtonUp(0) & Trash_drag)
                {
                    game_manager.gold = game_manager.gold + prefab_mechas[ID_mechas_Spawn].GetComponent<Info_Mecha>().prix / 2;

                    Mechas_Dead(UI_mechas_scene[ref_id_selec]);
                }
            }

            if (Input.GetMouseButtonUp(0)) { Mechas_drag = false; Upgrade_drag = false; Trash_drag = false; cursor.GetComponent<MeshRenderer>().material = Material_cursor[3]; }

            // Selection "Drag" 
            if (hit.collider.tag == "Boite 1")
            {
                if (Input.GetMouseButtonDown(0)) { ID_mechas_Spawn = 0; Mechas_drag = true; cursor.GetComponent<MeshRenderer>().material = Material_cursor[0]; }
            }
            if (hit.collider.tag == "Boite 2")
            {
                if (Input.GetMouseButtonDown(0)) { ID_mechas_Spawn = 1; Mechas_drag = true; cursor.GetComponent<MeshRenderer>().material = Material_cursor[1]; }
            }
            if (hit.collider.tag == "Boite 3")
            {
                if (Input.GetMouseButtonDown(0)) { ID_mechas_Spawn = 2; Mechas_drag = true; cursor.GetComponent<MeshRenderer>().material = Material_cursor[2]; }
            }
            if (hit.collider.tag == "Boite UpGrade")
            {
                if (Input.GetMouseButtonDown(0)) { Upgrade_drag = true; }
            }
            



            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                recherche_ID_en_vie(0);
                New_Target[ref_id_selec].GetComponentInChildren<MeshRenderer>().material = Material_cursor[3];
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                recherche_ID_en_vie(1);
                New_Target[ref_id_selec].GetComponentInChildren<MeshRenderer>().material = Material_cursor[3];
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                recherche_ID_en_vie(2);
                New_Target[ref_id_selec].GetComponentInChildren<MeshRenderer>().material = Material_cursor[3];
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                recherche_ID_en_vie(3);
                New_Target[ref_id_selec].GetComponentInChildren<MeshRenderer>().material = Material_cursor[3];
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                recherche_ID_en_vie(4);
                New_Target[ref_id_selec].GetComponentInChildren<MeshRenderer>().material = Material_cursor[3];
            }
            

        }
    }
    

    public void recherche_ID_en_vie(int touche)
    {

        for (int i = 0; i < UI_mechas_scene.Count; i++)
        {
            if (UI_mechas_scene[i] != null)
            {
                UI_mechas_scene[i].GetComponent<Info_Mecha>().mechas_selec = false;

                if (UI_mechas_scene[i].name == "Mechas_Air(Clone)") { New_Target[i].GetComponentInChildren<MeshRenderer>().material = Material_cursor[0]; }
                if (UI_mechas_scene[i].name == "Mechas_All(Clone)") { New_Target[i].GetComponentInChildren<MeshRenderer>().material = Material_cursor[1]; }
                if (UI_mechas_scene[i].name == "Mechas_Ground(Clone)") { New_Target[i].GetComponentInChildren<MeshRenderer>().material = Material_cursor[2]; }

            }
        }


        for (int i = 0; i < UI_mechas_scene.Count; i++)
        {
            if (UI_mechas_scene[i] != null && i == touche)
            {
                UI_mechas_scene[i].GetComponent<Info_Mecha>().mechas_selec = true;
                ref_id_selec = UI_mechas_scene[i].gameObject.GetComponent<Info_Mecha>().id;
                test_memori = UI_mechas_scene[i];
            }
        }

    }

    
    public void New_selection(GameObject New_selec)
    {
        for (int i = 0; i < UI_mechas_scene.Count; i++)
        {
            if (UI_mechas_scene[i] != null)
            {
                UI_mechas_scene[i].GetComponent<Info_Mecha>().mechas_selec = false;

                if (UI_mechas_scene[i].name == "Mechas_Air(Clone)") { New_Target[i].GetComponentInChildren<MeshRenderer>().material = Material_cursor[0]; }
                if (UI_mechas_scene[i].name == "Mechas_All(Clone)") { New_Target[i].GetComponentInChildren<MeshRenderer>().material = Material_cursor[1]; }
                if (UI_mechas_scene[i].name == "Mechas_Ground(Clone)") { New_Target[i].GetComponentInChildren<MeshRenderer>().material = Material_cursor[2]; }

            }
        }

        New_selec.GetComponent<Info_Mecha>().mechas_selec = true;
    }

    public void Mechas_Dead(GameObject Victime)
    {
        UI_mechas_scene[Victime.GetComponent<Info_Mecha>().id_ui] = null;

        //ID_in_scene_test[ref_id_selec] = 100;

        GameObject.Destroy(New_Target[ref_id_selec]);

        New_Target[ref_id_selec] = Debug_Mechas_Selec;
        test_memori = Debug_Mechas_Selec;
        Limit_mechas++;

        GameObject.Destroy(Victime);
        return;
    }

}