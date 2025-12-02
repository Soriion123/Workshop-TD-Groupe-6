using UnityEngine;
using System.Collections.Generic;

public class A_Slow : MonoBehaviour
{
    [SerializeField] private float slowMultiplier = 0.5f;
    [SerializeField] private string enemyTag = "Enemy";

    public Spawn_ability spawner;
    public int pointIndex;
    public string A_slow = "Slow Upgrade";

    private List<ISlowable> affectedEnemies = new();

    public bool IsActive { get; private set; } = false;

    public void Activate()
    {
        IsActive = true;

        foreach (var enemy in affectedEnemies)
        {
            enemy.ModifySpeed(slowMultiplier);
        }
    }

    public void Deactivate()
    {
        IsActive = false;

        foreach (var enemy in affectedEnemies)
        {
            enemy.ModifySpeed(1f);
        }

        affectedEnemies.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        Info_Mecha mecha = other.GetComponent<Info_Mecha>();
        if (mecha != null)
        {
            Debug.Log("Upgrade Slow récupéré !");

            // Informer le mecha (si tu veux garder la trace)
            mecha.PickupUpgrade(A_slow);

            // Détruire l'upgrade
            Destroy(gameObject);
        }

        if (!IsActive) return;
        if (!other.CompareTag(enemyTag)) return;

        ISlowable slowable = other.GetComponent<ISlowable>();
        if (slowable != null)
        {
            slowable.ModifySpeed(slowMultiplier);
            affectedEnemies.Add(slowable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ISlowable slowable = other.GetComponent<ISlowable>();
        if (slowable != null && affectedEnemies.Contains(slowable))
        {
            slowable.ModifySpeed(1f);
            affectedEnemies.Remove(slowable);
        }
    }
}
