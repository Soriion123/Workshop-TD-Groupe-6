using UnityEngine;

public class Trigger_attaque_Meka : MonoBehaviour
{

    [SerializeField] private GameObject Projot_Prefab;
    [SerializeField] private GameObject Canon;
    [SerializeField] private Transform Shoot_Point;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            print("Enemy In Range");
            //Canon.transform.LookAt(other.transform.position);
            //Instantiate(Projot_Prefab, Shoot_Point.gameObject.transform.position, Quaternion.identity);
        }
    }

}
