using UnityEngine;
using System.Collections;

public class Flying_Spawner : MonoBehaviour
{
    [SerializeField] private float countdown;

    [Header("Spawn Settings")]
    [SerializeField] private GameObject SpawnPoint;

    [Header("Target Settings")]
    public Transform target;   // 👈 Cible vers laquelle les ennemis marchent

    [Header("Random Spawn Area (Rectangular + Rotation)")]
    public Vector2 spawnAreaSize = new Vector2(2f, 2f); // largeur X, longueur Z

    public Wave[] waves;
    public int currentWaveIndex = 0;

    private bool waveIsRunning = false;

    private void Start()
    {
        countdown = waves[0].timeToNextWave;

        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].ennemiesLeft = waves[i].ennemySmalls.Length;
        }
    }

    private void Update()
    {


        countdown -= Time.deltaTime;

        if (countdown <= 0 && !waveIsRunning)
        {
            StartCoroutine(SpawnWaveFlying());
            waveIsRunning = true;
        }
    }

    private IEnumerator SpawnWaveFlying()
    {
        if (currentWaveIndex < waves.Length)
        {
            Wave wave = waves[currentWaveIndex];

            for (int i = 0; i < wave.ennemySmalls.Length; i++)
            {
                // ----- RANDOM OFFSET LOCAL -----
                Vector3 localOffset = new Vector3(
                    Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
                    0f,
                    Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f)
                );

                // ----- ROTATION DE LA ZONE -----
                Vector3 worldOffset = SpawnPoint.transform.rotation * localOffset;

                // ----- POSITION FINALE -----
                Vector3 spawnPosition = SpawnPoint.transform.position + worldOffset;

                // ----- INSTANTIATION -----
                Flying_Basics Flying_Basic = Instantiate(
                    wave.ennemySmalls[i],
                    spawnPosition,
                    Quaternion.identity
                );

                // ----- ASSIGNATION DE LA CIBLE -----
                Flying_Basic.target = target;

                yield return new WaitForSeconds(wave.timeToNextEnnemy);
            }

            // Passer à la vague suivante
            currentWaveIndex++;
            if (currentWaveIndex < waves.Length)
                countdown = waves[currentWaveIndex].timeToNextWave;

            waveIsRunning = false;
        }
    }

    // ----- GIZMOS AVEC ROTATION -----
    private void OnDrawGizmosSelected()
    {
        if (SpawnPoint == null)
            return;

        // Sauvegarde de la matrice actuelle
        Matrix4x4 oldMatrix = Gizmos.matrix;

        // Matrice alignée sur le SpawnPoint
        Gizmos.matrix = Matrix4x4.TRS(
            SpawnPoint.transform.position,
            SpawnPoint.transform.rotation,
            Vector3.one
        );

        // Zone semi-transparente
        Gizmos.color = new Color(0f, 1f, 0f, 0.35f);
        Gizmos.DrawCube(Vector3.zero, new Vector3(spawnAreaSize.x, 0.1f, spawnAreaSize.y));

        // Contour
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(spawnAreaSize.x, 0.1f, spawnAreaSize.y));

        // Restauration de la matrice
        Gizmos.matrix = oldMatrix;
    }
}

[System.Serializable]
public class Wave
{
    public Flying_Basics[] ennemySmalls;
    public float timeToNextEnnemy;
    public float timeToNextWave;

    [HideInInspector] public int ennemiesLeft;
}
