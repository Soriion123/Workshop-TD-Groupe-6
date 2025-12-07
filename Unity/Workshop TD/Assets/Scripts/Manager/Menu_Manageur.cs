using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu_Manageur : MonoBehaviour
{

    public GameObject Canvas_Pause;

    public GameObject Cliquer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

}
