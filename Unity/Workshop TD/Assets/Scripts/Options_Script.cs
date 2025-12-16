using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Options_Script : MonoBehaviour
{
    public AudioMixer audioMixer;

    /*private void Start()
    {
        audioMixer.SetFloat("MasterVolume", -80f);
    }*/
    public void SetVolume(float value)
    {
        // value doit être entre 0 et 1
        audioMixer.SetFloat("Master", Mathf.Log10(value) * 20);
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaa");
        return;
    }


    public void SetFullScreen(bool IsFullScreen)
    {
        Screen.fullScreen = IsFullScreen;
    }

}
