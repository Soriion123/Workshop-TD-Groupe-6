using UnityEngine;

public class Options_Script : MonoBehaviour
{
    
    public void SetVolume(float volume)
    {
        print(volume);
    }


    public void SetFullScreen(bool IsFullScreen)
    {
        Screen.fullScreen = IsFullScreen;
    }

}
