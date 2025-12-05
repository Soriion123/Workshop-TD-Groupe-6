using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu_Manageur : MonoBehaviour
{
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

}
