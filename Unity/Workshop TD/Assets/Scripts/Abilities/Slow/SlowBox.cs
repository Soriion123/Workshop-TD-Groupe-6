using UnityEngine;

public class SlowBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Mecha_AbilityManager manager = other.GetComponent<Mecha_AbilityManager>();

        if (manager != null)
        {
            manager.SetAbility(AbilityType.Slow);
            Destroy(gameObject);
        }

    }
}
