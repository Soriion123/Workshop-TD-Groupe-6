using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player Money")]
    public int gold = 0;

    public TextMeshProUGUI Gold_Ui;


    public float TimeScale = 1;
    public GameObject Canvas_Pause;
    public bool anime_bool = false;
    public bool anime_bool_revers = false;
    public bool menu_active = false;


    public GameObject Cliquer;

    private void Awake()
    {
        Time.timeScale = 1f;
        // Singleton basique
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }

    public void Update()
    {
        Gold_Ui.text = "GOLD : " + gold.ToString();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Time.timeScale = TimeScale;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menu_active)
            {
                anime_bool = true;
            }
            else if (menu_active)
            {
                Time.timeScale = 1;
                anime_bool_revers = true;
            }
            /*
            Canvas_Pause.SetActive(true);
            Time.timeScale = 0;
            Cliquer.SetActive(false);
            */
        }

        if (anime_bool)
        {
            Canvas_Pause.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(Canvas_Pause.GetComponent<RectTransform>().anchoredPosition, new Vector2(0, 240), Time.deltaTime * 5f);
        }
        if (Canvas_Pause.GetComponent<RectTransform>().anchoredPosition.y > 240 - 1f & anime_bool)
        {
            Canvas_Pause.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 240);
            anime_bool = false;
            menu_active = true;

            Time.timeScale = 0;
        }

        if (anime_bool_revers)
        {
            Canvas_Pause.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(Canvas_Pause.GetComponent<RectTransform>().anchoredPosition, new Vector2(0, -220), Time.deltaTime * 5f);
        }
        if (Canvas_Pause.GetComponent<RectTransform>().anchoredPosition.y < -220 + 1f & anime_bool_revers)
        {
            Canvas_Pause.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -220);
            anime_bool_revers = false; 
            menu_active = false;
            anime_bool_revers = false;
            
        }

    }

}
