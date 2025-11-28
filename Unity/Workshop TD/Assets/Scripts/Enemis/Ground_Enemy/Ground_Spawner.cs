using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Ground_Spawner : MonoBehaviour
{
    [SerializeField] private float countdown;

    [Header("Spawn Settings")]
    [SerializeField] private GameObject SpawnPointGround;

    [Header("Target Settings")]
    public Transform target;

    [Header("Random Spawn Area")]
    public Vector2 spawnAreaSize = new Vector2(2f, 2f);

    public WaveGround[] waves;
    public int currentWaveIndex = 0;

    private bool waveIsRunning = false;

    private void Start()
    {
        if (waves == null || waves.Length == 0)
        {
            Debug.LogError("Aucune vague définie dans l'inspector !");
            enabled = false;
            return;
        }

        countdown = waves[0].timeToNextWave;

        for (int i = 0; i < waves.Length; i++)
            waves[i].enemiesLeft = waves[i].enemies != null ? waves[i].enemies.Length : 0;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f && !waveIsRunning)
        {
            StartCoroutine(SpawnWaveGround());
            waveIsRunning = true;
        }
    }

    private IEnumerator SpawnWaveGround()
    {
        if (currentWaveIndex >= waves.Length) yield break;

        WaveGround wave = waves[currentWaveIndex];
        if (wave.enemies == null || wave.enemies.Length == 0)
        {
            Debug.LogWarning("Wave " + currentWaveIndex + " vide.");
            currentWaveIndex++;
            waveIsRunning = false;
            yield break;
        }

        for (int i = 0; i < wave.enemies.Length; i++)
        {
            Vector3 localOffset = new Vector3(
                Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
                0f,
                Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f)
            );

            Vector3 worldOffset = SpawnPointGround.transform.rotation * localOffset;
            Vector3 spawnPos = SpawnPointGround.transform.position + worldOffset;

            if (NavMesh.SamplePosition(spawnPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
                spawnPos = hit.position;

            Ground_Enemy enemy = Instantiate(wave.enemies[i], spawnPos, Quaternion.identity);
            enemy.target = target; // assignation de la cible peu importe le type



            yield return new WaitForSeconds(wave.timeToNextEnnemy);
        }

        currentWaveIndex++;
        if (currentWaveIndex < waves.Length)
            countdown = waves[currentWaveIndex].timeToNextWave;

        waveIsRunning = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (SpawnPointGround == null) return;

        Matrix4x4 old = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(SpawnPointGround.transform.position, SpawnPointGround.transform.rotation, Vector3.one);

        Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
        Gizmos.DrawCube(Vector3.zero, new Vector3(spawnAreaSize.x, 0.1f, spawnAreaSize.y));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(spawnAreaSize.x, 0.1f, spawnAreaSize.y));

        Gizmos.matrix = old;
    }
}

[System.Serializable]
public class WaveGround
{
    public Ground_Enemy[] enemies;   // <-- le spawner lit maintenant TOUS les types
    public float timeToNextEnnemy;
    public float timeToNextWave;

    [HideInInspector] public int enemiesLeft;
}
