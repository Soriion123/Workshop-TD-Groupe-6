using Unity.VisualScripting;
using UnityEngine;

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
            }
        }
    }
}
