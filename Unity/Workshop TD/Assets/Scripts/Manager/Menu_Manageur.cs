using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;


public class Menu_Manageur : MonoBehaviour
{

    public GameObject Panel_Intro;

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
        test_memori = Canvas_Commands.GetComponent<RectTransform>().anchoredPosition;

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
        AudioManager.Instance.StopLoop();

        SceneManager.LoadScene("LD Test 5");
        AudioManager.Instance.Play("SwapUI");
    }

    public void Quit_Game()
    {
        Application.Quit();
        AudioManager.Instance.Play("SwapUI");
    }

    public void Continue_Game()
    {
        Time.timeScale = 1f;
        Canvas_Pause.SetActive(false);
        Cliquer.SetActive(true);
        AudioManager.Instance.Play("SwapUI");
    }

    public void Back_To_Intro()
    {
        AudioManager.Instance.StopLoop();

        SceneManager.LoadScene("Menu Start");
        AudioManager.Instance.Play("SwapUI");
    }

    public void Clic_Commands()
    {
        anime_selec = Canvas_Commands;

        back_menu = false;
        anime_menu = true;
        AudioManager.Instance.Play("SwapUI");
    }

    public void Clic_Credit()
    {
        anime_selec = Canvas_Credis;

        back_menu = false;
        anime_menu = true;
        AudioManager.Instance.Play("SwapUI");
    }

    public void Clic_Open_Option()
    {
        anime_selec = Canvas_Options;
        back_menu = false;
        anime_menu = true;
        AudioManager.Instance.Play("SwapUI");
    }

    public void clic_retour()
    {
        back_menu = true;
        anime_menu = true;
        AudioManager.Instance.Play("SwapUI");
    }

    public void Last_screen(GameObject last_dead)
    {
        anime_selec = last_dead;
        back_menu = false;
        anime_menu = true;
        Time.timeScale = 1f;
    }


    public void anime_menu_fonction(GameObject Panel_GO, bool back)
    {


        if (!back)
        {
            print(Time.timeScale);
            Panel_GO.GetComponent<RectTransform>().anchoredPosition = Vector2.LerpUnclamped(Panel_GO.GetComponent<RectTransform>().anchoredPosition, Vector2.zero, Time.deltaTime * 5f);
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
