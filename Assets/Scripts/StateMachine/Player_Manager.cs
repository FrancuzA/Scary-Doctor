using FMOD.Studio;
using UnityEngine;
using FMODUnity;

public class Player_Manager : MonoBehaviour
{
    public Rigidbody PlayerRigidbody;
    public  float Speed = 3.6f;
    public Collider SlidingCollider;
    public Collider PlayerCollider;
    public float StartingMilestone;
    private int NextMileStone = 50;
    private float CurrentDistance = 0;
    public float MusicVolume;
    public LayerMask obstacle;
    public EventReference MusicLoop; // The FMOD event to play in a loop
    public EventReference soundDeath;
    public EventReference soundJumpStart;
    public EventReference soundJumpEnd;
    public EventReference soundSlide;
    public EventReference soundBoyRun;
    private EventInstance MusicSoundInstance; // The FMOD event instance
    private EventInstance DeathSoundInstance;
    public EventInstance JumpStartInstance;
    public EventInstance JumpEndInstance;
    public EventInstance SlideInstance;
    public EventInstance BoyRunInstance;
    public GameObject DeathUI;
    public static Player_Manager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        MusicSoundInstance = RuntimeManager.CreateInstance(MusicLoop);
        DeathSoundInstance = RuntimeManager.CreateInstance(soundDeath);
        JumpStartInstance = RuntimeManager.CreateInstance(soundJumpStart);
        JumpEndInstance = RuntimeManager.CreateInstance(soundJumpEnd);
        SlideInstance = RuntimeManager.CreateInstance(soundSlide);
        BoyRunInstance = RuntimeManager.CreateInstance(soundBoyRun);
        PlaySound();
    }


    void Update()
    {
        if (PlayerRigidbody.transform.position.y < -1) { Death(); }
    }

    private void FixedUpdate()
    {
        SpeedUpGame();
        MusicSoundInstance.setVolume(MusicVolume);
    }

    private void PlaySound()
    {
        MusicSoundInstance.start();
    }

    private void StopSound()
    {
        MusicSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    private void OnDestroy()
    {
        if (MusicSoundInstance.isValid())
        {
            MusicSoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            MusicSoundInstance.release();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Death();
        }
    }
    
    public void Death()
    {
        MusicSoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        BoyRunInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        Enemy_Mouvement.MonsterRunInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        DeathSoundInstance.start();
        DeathUI.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void SpeedUpGame()
    {

        CurrentDistance += Time.deltaTime;
        if (CurrentDistance > NextMileStone)
        {
            NextMileStone += 10;
            Speed = 2*Mathf.Log(Speed, 2);
        }
    }
   
    public void StartGame()
    {
        Enemy_Mouvement.StartMoving();
    }
}
