using UnityEngine;

public class Box__Selec : MonoBehaviour
{

    public GameObject This_BOX;

    public int Type_box;
    public Material[] Material_Box;

    public GameManager Game_Manager;
    
    public GameObject[] prefab_mechas;

    public Material[] Up_Grade_mat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (This_BOX.tag == "Boite 1" || This_BOX.tag == "Boite 2" || This_BOX.tag == "Boite 3")
        {
            if (Type_box == 0) { Switch_color_Box_Crea(0); }
            if (Type_box == 1) { Switch_color_Box_Crea(1); }
            if (Type_box == 2) { Switch_color_Box_Crea(2); }
        }
        else
        {

            /*
            if (Type_box == 0) 
            {
                if (Game_Manager.gold > 1934)
                {
                    This_BOX.GetComponent<MeshRenderer>().material = Material_Box[0];
                }
                else
                {
                    This_BOX.GetComponent<MeshRenderer>().material = Material_Box[3];
                }
            }
            if (Type_box == 1) 
            {
                if (Game_Manager.gold > 2021)
                {
                    This_BOX.GetComponent<MeshRenderer>().material = Material_Box[1];
                }
                else
                {
                    This_BOX.GetComponent<MeshRenderer>().material = Material_Box[4];
                }
            }
            if (Type_box == 2) 
            {
                if (Game_Manager.gold > 1122)
                {
                    This_BOX.GetComponent<MeshRenderer>().material = Material_Box[2];
                }
                else
                {
                    This_BOX.GetComponent<MeshRenderer>().material = Material_Box[5];
                }
            }
            */

            if (Game_Manager.gold < 1122)
            {
                This_BOX.GetComponent<MeshRenderer>().material = Up_Grade_mat[0];
            }
            if (Game_Manager.gold >= 1122 & Game_Manager.gold < 1934)
            {
                This_BOX.GetComponent<MeshRenderer>().material = Up_Grade_mat[1];
            }
            if (Game_Manager.gold >= 1934 & Game_Manager.gold < 1122)
            {
                This_BOX.GetComponent<MeshRenderer>().material = Up_Grade_mat[2];
            }
            if (Game_Manager.gold >= 2021)
            {
                This_BOX.GetComponent<MeshRenderer>().material = Up_Grade_mat[3];
            }

        }
        
    }

    void Switch_color_Box_Crea(int type)
    {
        if (Game_Manager.gold > prefab_mechas[type].GetComponent<Info_Mecha>().prix)
        {
            This_BOX.GetComponent<MeshRenderer>().material = Material_Box[type];
        }
        else if (Type_box == 0)
        {
            This_BOX.GetComponent<MeshRenderer>().material = Material_Box[3];
        }
        else if (Type_box == 1)
        {
            This_BOX.GetComponent<MeshRenderer>().material = Material_Box[4];
        }
        else if (Type_box == 2)
        {
            This_BOX.GetComponent<MeshRenderer>().material = Material_Box[5];
        }
    }

    

}
