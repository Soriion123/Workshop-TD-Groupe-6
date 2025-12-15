using UnityEngine;

public class UI_cansel_Clic : MonoBehaviour
{

    public GameObject Cliquer;

    public void ui_enter()
    {
        Cliquer.SetActive(false);
    }

    public void ui_exit()
    {
        Cliquer.SetActive(true);
    }


}
