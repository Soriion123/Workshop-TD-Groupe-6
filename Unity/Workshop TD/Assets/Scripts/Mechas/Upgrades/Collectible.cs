using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Spawn_Upgrade spawner;
    public int pointIndex;

    public string upgradeName = "Upgrade";  // nom affiché dans l'inspector du Mecha

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet qui entre possède un script Info_Mecha (ton mecha)
        Info_Mecha mecha = other.GetComponent<Info_Mecha>();
        if (mecha != null)
        {
            Debug.Log("Objet récupéré !");

            // On envoie l'upgrade au Mecha
            mecha.PickupUpgrade(upgradeName);

            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (spawner != null)
            spawner.FreePoint(pointIndex);
    }
}
