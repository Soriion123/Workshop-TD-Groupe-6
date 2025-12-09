using UnityEngine;

public class A_UpgradePickup : MonoBehaviour
{
    private Spawn_ability spawner;
    private int pointIndex;

    public void Init(Spawn_ability spawner, int index)
    {
        this.spawner = spawner;
        this.pointIndex = index;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mechas"))
        {
            // Libérer le point
            spawner.FreePoint(pointIndex);

            // détruire / appliquer upgrade
            Destroy(gameObject);
        }
    }
}
