using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Enemy_Move : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.Find("Enemy Target");
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.transform.position);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.2f)
        {
            Destroy(gameObject);
            return;
        }

    }
}
