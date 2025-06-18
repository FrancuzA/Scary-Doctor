using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class MusicLVL : MonoBehaviour
{
    public static MusicLVL instance;
    public EventReference LVLMusic;
    public EventInstance MusicInstance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        SetLVL1();
    }
    public void SetLVL1()
    {
        Player_Manager.instance.MusicSoundInstance.setParameterByName("LVLNumber", 0);
    }

    public void SetLVL2()
    {
        Player_Manager.instance.MusicSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        Player_Manager.instance.MusicSoundInstance.setParameterByName("LVLNumber", 1);
    }
}
