using UnityEngine;

public class Spawn_Upgrade : MonoBehaviour
{
    public GameObject prefab;          // L'objet à faire apparaître
    public Transform[] spawnPoints;    // Les positions prédéfinies
    public float spawnInterval = 2f;   // Intervalle entre les apparitions

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
    }

    void SpawnObject()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("Aucun point de spawn défini !");
            return;
        }

        // Choisir un point aléatoire
        Transform chosenPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(prefab, chosenPoint.position, chosenPoint.rotation);
    }
}
