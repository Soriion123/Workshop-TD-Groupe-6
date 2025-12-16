using System.Collections;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public static VictoryManager Instance;

    private int spawnersFinished = 0;
    private int totalSpawners = 0;
    private bool victoryLaunched = false;
    public GameObject Cliquer;
    
    public GameObject Cancas_Win;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        totalSpawners = FindObjectsOfType<Ground_Spawner>().Length;
    }

    // Appelé par un spawner UNIQUEMENT à sa dernière wave
    public void NotifySpawnerFinished()
    {
        spawnersFinished++;

        if (spawnersFinished >= totalSpawners && !victoryLaunched)
        {
            StartCoroutine(CheckVictoryCondition());
        }
    }

    private IEnumerator CheckVictoryCondition()
    {
        // Attend qu'il n'y ait plus d'ennemis
        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            Debug.Log("Enemies left: " + GameObject.FindGameObjectsWithTag("Enemy").Length);

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(2f);

        victoryLaunched = true;
        LaunchVictory();
    }

    private void LaunchVictory()
    {
        Debug.Log("🎉 VICTOIRE");

        Menu_Manageur menu = FindFirstObjectByType<Menu_Manageur>();
        menu.Last_screen(Cancas_Win);

        AudioManager.Instance.StopLoop();
        AudioManager.Instance.Play("Victoire");
        Cliquer.SetActive(false);
    }

}
