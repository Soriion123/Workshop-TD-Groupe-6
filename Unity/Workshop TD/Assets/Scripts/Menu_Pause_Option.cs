using UnityEngine;

public class Menu_Pause_Option : MonoBehaviour
{

    public GameObject Panel_Option;
    public GameObject Panel_Credits;
    public GameObject Panel_Comande;

    public GameObject Panel_ref;

    public bool anime_go;
    public bool anime_back;


    /*
    public bool Option_active;
    public bool Credits_active;
    public bool Comande_active;
    */

    public bool retour_pause;

    public bool autre_option = false;

    public Vector2 Panel_memori;

    public GameManager GameManager;
    public Intro_Screen Intro_Screen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Panel_memori = Panel_Option.GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Option_active)
        {
            Panel_Option.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(Panel_Option.GetComponent<RectTransform>().anchoredPosition, Vector2.zero, Time.deltaTime * 5f);
            if (Panel_Option.GetComponent<RectTransform>().anchoredPosition == Vector2.zero) { Option_active = false; }
        }
        if (Credits_active)
        {
            Panel_Credits.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(Panel_Credits.GetComponent<RectTransform>().anchoredPosition, Vector2.zero, Time.deltaTime * 5f);
            if (Panel_Credits.GetComponent<RectTransform>().anchoredPosition == Vector2.zero) { Credits_active = false; }
        }
        if (Credits_active)
        {
            Panel_Comande.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(Panel_Comande.GetComponent<RectTransform>().anchoredPosition, Vector2.zero, Time.deltaTime * 5f);
            if (Panel_Comande.GetComponent<RectTransform>().anchoredPosition == Vector2.zero) { Comande_active = false; }
        }
        */

        if (anime_go)
        {
            anime_menu_fonction_2(Panel_ref, anime_back);
        }
        print(Time.timeScale);

        /*
        if (retour_pause)
        {
            Time.timeScale = 1;

            print(Panel_Option.GetComponent<RectTransform>().anchoredPosition);
            Panel_Option.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(Panel_Option.GetComponent<RectTransform>().anchoredPosition, Panel_memori, Time.deltaTime * 5f);
        }
        */

    }

    public void Option_Menu()
    {
        autre_option = true;
        Panel_ref = Panel_Option;
        anime_go = true;
        //retour_pause = true;
        anime_back = false;
        Time.timeScale = 1 ;
        AudioManager.Instance.Play("SwapUI");
    }

    public void Credits_Menu()
    {
        Panel_ref = Panel_Credits;
        anime_back = false;

        autre_option = true;
        anime_go = true;
        //retour_pause = true;
        Time.timeScale = 1;
        AudioManager.Instance.Play("SwapUI");
    }

    public void Comande_Menu()
    {
        Panel_ref = Panel_Comande;
        anime_back = false;

        autre_option = true;
        anime_go = true;
        //retour_pause = true;
        Time.timeScale = 1;
        AudioManager.Instance.Play("SwapUI");
    }

    public void retour()
    {
        anime_go = true;
        retour_pause = true;
        anime_back = true;
        Time.timeScale = 1;
        AudioManager.Instance.Play("SwapUI");
    }

    public void anime_menu_fonction_2(GameObject Panel_GO, bool back)
    {
        if (!back)
        {
            Panel_GO.GetComponent<RectTransform>().anchoredPosition = Vector2.LerpUnclamped(Panel_GO.GetComponent<RectTransform>().anchoredPosition, Vector2.zero, Time.deltaTime * 5f);
            if (Panel_GO.GetComponent<RectTransform>().anchoredPosition.x < 0.5)
            {
                Panel_GO.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                Time.timeScale = 0;
                anime_go = false;
            }
        }
        else
        {
            Panel_GO.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(Panel_GO.GetComponent<RectTransform>().anchoredPosition, Panel_memori, Time.deltaTime * 5f);
            
            if (Panel_GO.GetComponent<RectTransform>().anchoredPosition.x > Panel_memori.x - 0.5)
            {
                print("a");
                Panel_GO.GetComponent<RectTransform>().anchoredPosition = Panel_memori;
                anime_go = false;
                autre_option = false;

                if (!GameManager.anime_bool_revers & !Intro_Screen.Return_Start_Menu)
                {
                    Time.timeScale = 0;
                }

            }
            
        }

    }

}
