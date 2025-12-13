using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro_Screen : MonoBehaviour
{
    public Vector2 memori_test;
    public GameObject Panel_Intro;
    public bool Return_Start_Menu = false;

    public bool intro_fini = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        memori_test = Panel_Intro.GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {

        if (Panel_Intro.GetComponent<RectTransform>().anchoredPosition.y > -460 + 0.1 && !intro_fini)
        {
            StartCoroutine(time_start());
        }

        if (Return_Start_Menu)
        {
            Panel_Intro.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(Panel_Intro.GetComponent<RectTransform>().anchoredPosition, Vector2.zero, Time.deltaTime * 5f);

            if (Panel_Intro.GetComponent<RectTransform>().anchoredPosition == Vector2.zero)
            {
                SceneManager.LoadScene("Menu Start");
            }

        }

    }

    IEnumerator time_start()
    {
        yield return new WaitForSeconds(1.2f);
        Panel_Intro.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(Panel_Intro.GetComponent<RectTransform>().anchoredPosition, new Vector2(0, -500), Time.deltaTime * 5f);

        if (Panel_Intro.GetComponent<RectTransform>().anchoredPosition.y < -500 + 0.1f)
        {
            intro_fini = true;
        }

    }

    public void return_to_home()
    {
        Return_Start_Menu = true;
        Time.timeScale = 1.0f;
    }

}
