using UnityEngine;
using UnityEngine.AI;

public class Mechas_Move_Test : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] public GameObject target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.transform.position);
    }
}
