using UnityEngine;

public class AOEBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Mecha_AbilityManager manager = other.GetComponent<Mecha_AbilityManager>();

        if (manager != null)
        {
            manager.SetAbility(AbilityType.AOE);
            Destroy(gameObject);
        }
    }
}
