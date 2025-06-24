using FMOD.Studio;
using UnityEngine;
using FMODUnity;

public class RandomLaughtPlayer : MonoBehaviour
{
    public EventReference laughtSound;
    public EventInstance laughtSoundInstance;
    public float Timer;

    private void Start()
    {
        Timer = RNG_Custom.random.Next(100, 1000);
    }
    void FixedUpdate()
    {
        if (Timer > 0f)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0f)
            {
                laughtSoundInstance = RuntimeManager.CreateInstance(laughtSound);
                laughtSoundInstance.start();
                laughtSoundInstance.release();
                Timer = RNG_Custom.random.Next(100, 1000);
            }
        }
    }
}
