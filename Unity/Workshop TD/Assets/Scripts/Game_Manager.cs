using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player Money")]
    public int gold = 0;

    public TextMeshProUGUI Gold_Ui;

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
    }

}
