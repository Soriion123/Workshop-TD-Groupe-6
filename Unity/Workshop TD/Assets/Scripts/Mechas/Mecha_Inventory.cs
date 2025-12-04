using UnityEngine;

public class Mecha_Inventory : MonoBehaviour 
{
    public int slowTokens = 0;

    public bool HasSlowToken()
    {
        return slowTokens > 0;
    }

    public void AddSlowToken()
    {
        slowTokens++;
    }

    public void ConsumeSlowToken()
    {
        if (slowTokens > 0)
            slowTokens--;
    }


    public int AOETokens = 0;

    public bool HasAOEToken()
    {
        return AOETokens > 0;
    }

    public void AddAOEToken()
    {
        AOETokens++;
    }

    public void ConsumeAOEToken()
    {
        if (AOETokens > 0)
            AOETokens--;
    }

}
