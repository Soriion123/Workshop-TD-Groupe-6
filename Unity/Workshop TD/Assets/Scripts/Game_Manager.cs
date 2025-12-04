using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player Money")]
    public int gold = 0;

    public TextMeshProUGUI Gold_Ui;


    public float TimeScale = 1;

    private void Awake()
    {
        // Singleton basique
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("💰 Gold actuel : " + gold);
    }

    public void Update()
    {
        Gold_Ui.text = gold.ToString();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Time.timeScale = TimeScale;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Time.timeScale = 1;
        }

    }

}
