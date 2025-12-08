using UnityEngine;

public class AutoDeath_Box : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Mecha_AbilityManager manager = other.GetComponent<Mecha_AbilityManager>();

        if (manager != null)
        {
            manager.SetAbility(AbilityType.AutoDeath);
            Destroy(gameObject);
        }

    }
}
