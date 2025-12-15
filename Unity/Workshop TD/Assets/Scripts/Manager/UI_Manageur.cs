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
    public Image Nexus_Bar;

    public GameObject[] Icone_Mechas_Centre;
    public GameObject[] Icone_Mechas_Outline;

    public GameObject[] StarUp;

    //public TextMeshProUGUI[] text_ui_habiliti;
    public GameObject[] ability_ui_mechas;
    // public Texture[] Textures_ui_abiliti;
    public Sprite[] sprites_ui_abiliti;

    public Color[] Color_Mechas;

    public Ground_Spawner Ground_Spawner;
    public TextMeshProUGUI NB_Wave;
    public Image bar_wave;

    public GameObject Cancas_Win;
    public GameObject Cliquer;

    public TMP_InputField[] Name_Mechas;

    public GameObject Waring_UI;

    public Menu_Manageur Menu_Manageur;

    public GameObject info_ability;
    public TextMeshProUGUI Text_info_ability;
    public GameObject image_abilty;

    public Sprite[] Image_Types_Mechas;
    public GameObject[] UI_Types_Mechas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Nexus_Life_UI.text = "Piggy Bank : " + Nexus.currenthealth.ToString();
        Nexus_Bar.fillAmount = Nexus.currenthealth/ Nexus.maxHealth;
        NB_Wave.text = Ground_Spawner.cmp_Wave.ToString();
        
        if (Ground_Spawner.countdown > 0)
        {
            Waring_UI.SetActive(false);
            bar_wave.fillAmount = Ground_Spawner.countdown / Ground_Spawner.waves[Ground_Spawner.currentWaveIndex].timeToNextWave;
        }
        else if (Ground_Spawner.countdown < 0)
        {
            Waring_UI.SetActive(true);
        }

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
                    UI_Types_Mechas[i].GetComponent<Image>().sprite = Image_Types_Mechas[1];
                }
                
                
                if (cliqueur.UI_mechas_scene[i].name == "Mechas_All(Clone)")
                {
                    Icone_Mechas_Centre[i].GetComponent<Image>().color = Color.magenta ;
                    UI_Types_Mechas[i].GetComponent<Image>().sprite = Image_Types_Mechas[2];
                }
                
                
                if (cliqueur.UI_mechas_scene[i].name == "Mechas_Ground(Clone)")
                {
                    Icone_Mechas_Centre[i].GetComponent<Image>().color = Color.green ;
                    UI_Types_Mechas[i].GetComponent<Image>().sprite = Image_Types_Mechas[0];
                }

                if (cliqueur.UI_mechas_scene[i].GetComponent<Info_Mecha>().mechas_selec == true)
                {
                    Icone_Mechas_Outline[i].GetComponent<Image>().color = Color.yellow;
                }
                
                
                else
                {
                    Icone_Mechas_Outline[i].GetComponent<Image>().color = Color.black;
                }

                if (cliqueur.UI_mechas_scene[i].GetComponent<Info_Mecha>().Niveau_UpGrade == 0)
                {
                    StarUp[i].SetActive(false);
                }
                else
                {
                    StarUp[i].SetActive(true);
                }

                cliqueur.New_Target[cliqueur.UI_mechas_scene[i].GetComponent<Info_Mecha>().id].GetComponentInChildren<TextMeshProUGUI>().text = Name_Mechas[i].text;
            }

        }
    }

    public void check_abiliti(int i)
    {
        
        if (cliqueur.UI_mechas_scene[i].GetComponent<Mecha_AbilityManager>().slowAbility.enabled)
        {
            //text_ui_habiliti[i].text = "Slow";
            ability_ui_mechas[i].GetComponent<Image>().sprite = sprites_ui_abiliti[2];
        }
        else if (cliqueur.UI_mechas_scene[i].GetComponent<Mecha_AbilityManager>().aoeAbility.enabled)
        {
            //text_ui_habiliti[i].text = "AOE";
            ability_ui_mechas[i].GetComponent<Image>().sprite = sprites_ui_abiliti[4];
            //4
        }
        else if (cliqueur.UI_mechas_scene[i].GetComponent<Mecha_AbilityManager>().autoDeathAbility.enabled)
        {
            //text_ui_habiliti[i].text = "AUTO DEAD";
            ability_ui_mechas[i].GetComponent<Image>().sprite = sprites_ui_abiliti[1];
            //1
        }
        else if (cliqueur.UI_mechas_scene[i].GetComponent<Mecha_AbilityManager>().teleportAbility.enabled)
        {
            //text_ui_habiliti[i].text = "Teleport";
            ability_ui_mechas[i].GetComponent<Image>().sprite = sprites_ui_abiliti[3];
            //3
        }
        else
        {
            //text_ui_habiliti[i].text = " ";
            ability_ui_mechas[i].GetComponent<Image>().sprite = sprites_ui_abiliti[0];
            //0
        }
    }

    IEnumerator Last_Wave()
    {

        // Fair la bar de progersse pour la dernier vagues

        yield return new WaitForSeconds(60f);


        Menu_Manageur.Last_screen(Cancas_Win);
        /*
        Cancas_Win.SetActive(true);
        Time.timeScale = 0;
        */
        Cliquer.SetActive(false);
    }

    public void Info_panel_enter(int id)
    {
        
        
        if (ability_ui_mechas[id].GetComponent<Image>().sprite == sprites_ui_abiliti[1])
        {
            // Auto Dead
            info_ability.SetActive(true);
            Text_info_ability.text = "AutoDeath : This mecha self-destructs and eliminates all enemies in a large area around it.";
            image_abilty.GetComponent<Image>().sprite = sprites_ui_abiliti[1];
        }
        else if(ability_ui_mechas[id].GetComponent<Image>().sprite == sprites_ui_abiliti[2])
        {
            // Slow
            info_ability.SetActive(true);
            Text_info_ability.text = "Slow : Creates a zone that slows enemies around the mecha; duration 3 seconds.";
            image_abilty.GetComponent<Image>().sprite = sprites_ui_abiliti[2];
        }
        else if(ability_ui_mechas[id].GetComponent<Image>().sprite == sprites_ui_abiliti[3])
        {
            // Teleport
            info_ability.SetActive(true);
            Text_info_ability.text = "Teleport : Creates a sphere after activation; click inside it again to teleport this mecha.";
            image_abilty.GetComponent<Image>().sprite = sprites_ui_abiliti[3];
        }
        else if (ability_ui_mechas[id].GetComponent<Image>().sprite == sprites_ui_abiliti[4])
        {
            // AOE
            info_ability.SetActive(true);
            Text_info_ability.text = "AOE : Inflicts damage in a circle around this mecha to all enemies within range.";
            image_abilty.GetComponent<Image>().sprite = sprites_ui_abiliti[4];
        }
    }

    public void Info_panel_exit()
    {
        info_ability.SetActive(false);
    }

}
