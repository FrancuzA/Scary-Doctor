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
        MusicInstance = RuntimeManager.CreateInstance(LVLMusic);
    }
    public void SetLVL1()
    {
        MusicInstance.setParameterByName("LVLNumber", 0);
        MusicInstance.start();
    }

    public void SetLVL2()
    {
        MusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        MusicInstance.setParameterByName("LVLNumber", 1);
        MusicInstance.start();
    }
}
