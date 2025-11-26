using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Enemy_Spawn : MonoBehaviour
{

    [SerializeField] private GameObject Enemy;
    [SerializeField] private float comteur_time = 1;
    [SerializeField] private float comteur_comteur_shoot = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        comteur_time += Time.deltaTime;

        if (comteur_time > comteur_comteur_shoot)
        {
            Instantiate(Enemy, this.gameObject.transform.position, Quaternion.identity);
            comteur_time = 0;
        }
    }
}
