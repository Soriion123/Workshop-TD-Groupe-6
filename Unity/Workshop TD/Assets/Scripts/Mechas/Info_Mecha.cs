    using UnityEngine;

public class Info_Mecha : MonoBehaviour
{

    [SerializeField] public bool mechas_selec;
    [SerializeField] public int id;

    public int id_ui;

    public GameObject icone_selec;

    public int prix;

    public int Niveau_UpGrade;

    [Header("Dernier upgrade ramassé (debug)")]
    public string lastUpgradeCollected = "Aucun";

    public void PickupUpgrade(string upgradeName)
    {
        lastUpgradeCollected = upgradeName;
        Debug.Log("Le mecha a ramassé : " + lastUpgradeCollected);
    }

    public void Update()
    {
        if (mechas_selec)
        {
            icone_selec.SetActive(true);
        }
        else
        {
            icone_selec.SetActive(false);
        }
    }
}
