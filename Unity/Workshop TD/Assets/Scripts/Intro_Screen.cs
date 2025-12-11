using System.Collections;
using UnityEngine;

public class Intro_Screen : MonoBehaviour
{

    public GameObject Panel_Intro;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Panel_Intro.GetComponent<RectTransform>().anchoredPosition.y > -460 + 0.1)
        {
            StartCoroutine(time_start());
        }


        /*
        if (Panel_Intro.GetComponent<RectTransform>().anchoredPosition == Vector2.zero)
        {
            Panel_Intro.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        else
        {
            
        }
        */

    }

    IEnumerator time_start()
    {
        yield return new WaitForSeconds(1.5f);
        Panel_Intro.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(Panel_Intro.GetComponent<RectTransform>().anchoredPosition, new Vector2(0, -460), Time.deltaTime * 5f);
    }

}
