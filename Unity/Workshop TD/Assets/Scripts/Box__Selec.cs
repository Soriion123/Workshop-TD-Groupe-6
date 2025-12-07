using UnityEngine;

public class Box__Selec : MonoBehaviour
{

    public GameObject This_BOX;

    public int Type_box;
    public Material[] Material_Box;

    public GameManager Game_Manager;
    
    public GameObject[] prefab_mechas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Type_box == 0) { Switch_color(0); }
        if (Type_box == 1) { Switch_color(1); }
        if (Type_box == 2) { Switch_color(2); }
    }

    void Switch_color(int type)
    {

        if (Game_Manager.gold > prefab_mechas[type].GetComponent<Info_Mecha>().prix)
        {
            This_BOX.GetComponent<MeshRenderer>().material = Material_Box[type];
        }
        else
        {
            This_BOX.GetComponent<MeshRenderer>().material = Material_Box[3];
        }
    }

}
