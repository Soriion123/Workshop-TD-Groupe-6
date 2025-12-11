using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;


public class Menu_Manageur : MonoBehaviour
{

    public GameObject Canvas_Pause;
    public GameObject Canvas_Commands;
    public GameObject Canvas_Credis;
    public GameObject Canvas_Options;

    public bool anime_menu = false;
    public bool back_menu = false;
    public GameObject anime_selec;
    public Vector2 test_memori;

    public GameObject Cliquer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
        test_memori = Canvas_Commands.GetComponent<RectTransform>().anchoredPosition ;
    }

    // Update is called once per frame
    void Update()
    {
        if (anime_menu)
        {
            anime_menu_fonction(anime_selec, back_menu);
        }
    }

    public void Start_Game()
    {
        SceneManager.LoadScene("LD Test 4");
    }

    public void Quit_Game()
    {
        Application.Quit();
    }

    public void Continue_Game()
    {
        Time.timeScale = 1f;
        Canvas_Pause.SetActive(false);
        Cliquer.SetActive(true);
    }

    public void Back_To_Intro()
    {
        SceneManager.LoadScene("Menu Start");
    }

    public void Clic_Commands()
    {
        anime_selec = Canvas_Commands;

        back_menu = false;
        anime_menu = true;
    }

    public void Clic_Credit()
    {
        anime_selec = Canvas_Credis;

        back_menu = false;
        anime_menu = true;
    }

    public void Clic_Open_Option()
    {
        anime_selec = Canvas_Options;
        back_menu = false;
        anime_menu = true;
    }

    public void clic_retour()
    {
        back_menu = true;
        anime_menu = true;
    }

    public void anime_menu_fonction(GameObject Panel_GO , bool back)
    {


        if (!back)
        {
            Panel_GO.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(Panel_GO.GetComponent<RectTransform>().anchoredPosition, Vector2.zero, Time.deltaTime * 5f);
        }
        else
        {
            Panel_GO.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(Panel_GO.GetComponent<RectTransform>().anchoredPosition, test_memori, Time.deltaTime * 5f);
            if (Panel_GO.GetComponent<RectTransform>().anchoredPosition.y < test_memori.y + 0.1) 
            {
                Panel_GO.GetComponent<RectTransform>().anchoredPosition = test_memori;
            }
        }

    }

}
