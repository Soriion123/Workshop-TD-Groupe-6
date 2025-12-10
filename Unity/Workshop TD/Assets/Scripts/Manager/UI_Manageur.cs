using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


using TMPro;
using System.Collections;

public class UI_Manageur : MonoBehaviour
{

    // Les scriptes qui demande une UI
    public GameManager gameManager;
    public Nexus Nexus;
    public Cliqueur cliqueur;
    

    public TextMeshProUGUI Nexus_Life_UI;

    public GameObject[] Icone_Mechas_Centre;
    public GameObject[] Icone_Mechas_Outline;

    public TextMeshProUGUI[] Niveau_Mechas_UI;

    public TextMeshProUGUI[] text_ui_habiliti;

    public Color[] Color_Mechas;

    public Ground_Spawner Ground_Spawner;
    public TextMeshProUGUI NB_Wave;

    public GameObject Cancas_Win;
    public GameObject Cliquer;

    public TMP_InputField[] Name_Mechas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Nexus_Life_UI.text = Nexus.currenthealth.ToString();
        NB_Wave.text = Ground_Spawner.cmp_Wave.ToString();

        
        if (Ground_Spawner.cmp_Wave == 10)
        {
            StartCoroutine(Last_Wave());
        }

        for (int i = 0; i < cliqueur.UI_mechas_scene.Count; i++)
        {
            if (cliqueur.UI_mechas_scene[i] == null)
            {
                Icone_Mechas_Outline[i].gameObject.SetActive(false);
            }
            else
            {
                Icone_Mechas_Outline[i].gameObject.SetActive(true);

                check_abiliti(i);

                if (cliqueur.UI_mechas_scene[i].name == "Mechas_Air(Clone)")
                {
                    Icone_Mechas_Centre[i].GetComponent<Image>().color = Color.cyan ;
                }
                
                
                if (cliqueur.UI_mechas_scene[i].name == "Mechas_All(Clone)")
                {
                    Icone_Mechas_Centre[i].GetComponent<Image>().color = Color.magenta ;
                }
                
                
                if (cliqueur.UI_mechas_scene[i].name == "Mechas_Ground(Clone)")
                {
                    Icone_Mechas_Centre[i].GetComponent<Image>().color = Color.green ;
                }

                if (cliqueur.UI_mechas_scene[i].GetComponent<Info_Mecha>().mechas_selec == true)
                {
                    Icone_Mechas_Outline[i].GetComponent<Image>().color = Color.yellow;
                }
                
                
                else
                {
                    Icone_Mechas_Outline[i].GetComponent<Image>().color = Color.black;
                }

                Niveau_Mechas_UI[i].text = cliqueur.UI_mechas_scene[i].GetComponent<Info_Mecha>().Niveau_UpGrade.ToString();

                cliqueur.New_Target[cliqueur.UI_mechas_scene[i].GetComponent<Info_Mecha>().id].GetComponentInChildren<TextMeshProUGUI>().text = Name_Mechas[i].text;


            }
        }
    }

    public void check_abiliti(int i)
    {
        if (cliqueur.UI_mechas_scene[i].GetComponent<Mecha_AbilityManager>().slowAbility.enabled)
        {
            text_ui_habiliti[i].text = "Slow";
        }
        else if (cliqueur.UI_mechas_scene[i].GetComponent<Mecha_AbilityManager>().aoeAbility.enabled)
        {
            text_ui_habiliti[i].text = "AOE";
        }
        else if (cliqueur.UI_mechas_scene[i].GetComponent<Mecha_AbilityManager>().autoDeathAbility.enabled)
        {
            text_ui_habiliti[i].text = "AUTO DEAD";
        }
        else if (cliqueur.UI_mechas_scene[i].GetComponent<Mecha_AbilityManager>().teleportAbility.enabled)
        {
            text_ui_habiliti[i].text = "Teleport";
        }
        else
        {
            text_ui_habiliti[i].text = " ";
        }
    }

    IEnumerator Last_Wave()
    {
        yield return new WaitForSeconds(65f);

        Cancas_Win.SetActive(true);
        Cliquer.SetActive(false);
        Time.timeScale = 0;
    }

}
