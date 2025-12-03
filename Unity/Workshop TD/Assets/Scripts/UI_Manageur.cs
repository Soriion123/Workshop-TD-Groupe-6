using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manageur : MonoBehaviour
{

    // Les scriptes qui demande une UI
    public GameManager gameManager;
    public Nexus Nexus;
    public Cliqueur cliqueur;


    public GameObject[] Icone_Mechas;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cliqueur.UI_mechas_scene.Count; i++)
        {
            if (cliqueur.UI_mechas_scene[i] == null)
            {
                Icone_Mechas[i].gameObject.SetActive(false);
            }
            else
            {
                Icone_Mechas[i].gameObject.SetActive(true);


                if (cliqueur.UI_mechas_scene[i].name == "Mechas_Air(Clone)")
                {
                    Icone_Mechas[i].GetComponent<Image>().color = Color.blue;
                }
                if (cliqueur.UI_mechas_scene[i].name == "Mechas_All(Clone)")
                {
                    Icone_Mechas[i].GetComponent<Image>().color = Color.magenta;
                }
                if (cliqueur.UI_mechas_scene[i].name == "Mechas_Ground(Clone)")
                {
                    Icone_Mechas[i].GetComponent<Image>().color = Color.green;
                }

            }

            

        }
    }
}
