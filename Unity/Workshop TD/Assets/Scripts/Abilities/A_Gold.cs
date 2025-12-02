using UnityEngine;

public class A_Gold : MonoBehaviour
{
    public Spawn_ability spawner;
    public int pointIndex;

    [Header("Infos Upgrade")]
    public string A_gold = "Gold Upgrade";
    public int goldAmount = 100; // quantité d'or donnée

    private void OnTriggerEnter(Collider other)
    {
        Info_Mecha mecha = other.GetComponent<Info_Mecha>();
        if (mecha != null)
        {
            Debug.Log("Upgrade Gold récupéré ! +" + goldAmount);

            // Ajouter l'or au joueur
            GameManager.instance.AddGold(goldAmount);

            // Informer le mecha (si tu veux garder la trace)
            mecha.PickupUpgrade(A_gold);

            // Détruire l'upgrade
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (spawner != null)
            spawner.FreePoint(pointIndex);
    }
}
