    using UnityEngine;

public class Info_Mecha : MonoBehaviour
{

    [SerializeField] public bool mechas_selec;
    [SerializeField] public int id;

    public int prix;

    [Header("Dernier upgrade ramassé (debug)")]
    public string lastUpgradeCollected = "Aucun";

    public void PickupUpgrade(string upgradeName)
    {
        lastUpgradeCollected = upgradeName;
        Debug.Log("Le mecha a ramassé : " + lastUpgradeCollected);
    }
}
