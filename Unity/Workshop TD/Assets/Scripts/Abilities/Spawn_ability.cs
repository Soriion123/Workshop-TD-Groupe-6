using UnityEngine;
using System.Collections.Generic;

public class Spawn_ability : MonoBehaviour
{
    [Header("Liste de prefabs upgrades possibles")]
    public GameObject[] upgradePrefabs;

    [Header("Points de spawn prédéfinis")]
    public Transform[] spawnPoints;

    [Header("Timing")]
    public float spawnInterval = 2f;

    private List<int> freePoints = new List<int>();

    private void Start()
    {
        // Initialiser les points libres
        for (int i = 0; i < spawnPoints.Length; i++)
            freePoints.Add(i);

        InvokeRepeating(nameof(SpawnUpgrade), 0f, spawnInterval);
    }

    void SpawnUpgrade()
    {
        if (freePoints.Count == 0)
            return;

        if (upgradePrefabs.Length == 0)
            return;

        // Choisir un point libre
        int index = freePoints[Random.Range(0, freePoints.Count)];
        Transform chosenPoint = spawnPoints[index];

        // Choisir un upgrade aléatoire
        GameObject chosenUpgrade = upgradePrefabs[Random.Range(0, upgradePrefabs.Length)];

        // Instancier l’upgrade
        GameObject spawned = Instantiate(chosenUpgrade, chosenPoint.position, chosenPoint.rotation);

        // Marquer comme occupé
        freePoints.Remove(index);

        // ★ Assigner l’index et le spawner au script du collectible
        spawned.GetComponent<A_UpgradePickup>().Init(this, index);
    }

    public void FreePoint(int index)
    {
        if (!freePoints.Contains(index))
            freePoints.Add(index);
    }
}
