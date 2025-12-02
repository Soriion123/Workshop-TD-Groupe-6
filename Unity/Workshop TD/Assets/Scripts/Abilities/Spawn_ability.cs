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
        {
            Debug.Log("Tous les points sont occupés !");
            return;
        }

        if (upgradePrefabs.Length == 0)
        {
            Debug.LogWarning("Aucun prefab d’upgrade défini !");
            return;
        }

        // Choisir un point libre
        int index = freePoints[Random.Range(0, freePoints.Count)];
        Transform chosenPoint = spawnPoints[index];

        // Choisir un upgrade aléatoire
        GameObject chosenUpgrade = upgradePrefabs[Random.Range(0, upgradePrefabs.Length)];

        // Instancier l’upgrade
        GameObject spawned = Instantiate(chosenUpgrade, chosenPoint.position, chosenPoint.rotation);

        // Marquer le point comme occupé
        freePoints.Remove(index);

        // Donner les infos au Collectible
        A_Gold cp = spawned.GetComponent<A_Gold>();
        if (cp == null)
            cp = spawned.AddComponent<A_Gold>();

        /*A_Slow cp = spawned.GetComponent<A_Slow>();
        if (cp == null)
            cp = spawned.AddComponent<A_Slow>();*/

        cp.spawner = this;
        cp.pointIndex = index;
    }

    public void FreePoint(int index)
    {
        if (!freePoints.Contains(index))
            freePoints.Add(index);
    }
}
