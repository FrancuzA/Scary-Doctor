using FMOD.Studio;
using UnityEngine;
using FMODUnity;
using System.Collections;

public class Player_Manager : MonoBehaviour
{
    private Animator PlayerANim;
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
    public Spawner spawner;
    public GameObject Doctor;
    public GameObject StunVFX;
    public GameObject PlayerMesh;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        PlayerANim = gameObject.GetComponent<Animator>();
        Time.timeScale = 1.0f;
        MusicLVL.instance.SetLVL1();
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
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(Stun());
        }
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
        MusicSoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        MusicSoundInstance.release();
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

        if (other.CompareTag("Spawner"))
        {
            spawner.SpawnSegment();
        }
    }
    
    public void Death()
    {
        MusicSoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        BoyRunInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        DeathSoundInstance.start();
        StopSound();
        Point_System.instance.CheckForHighScore();
        Enemy_Mouvement.instance.GameOver();
        StartCoroutine(DeathSequance());
    }

    public IEnumerator DeathSequance()
    {
        Speed = 0f;
        PlayerANim.SetTrigger("GameOver");
        PlayerMesh.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        Doctor.SetActive(false);
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
        Enemy_Mouvement.instance.StartMoving();
        Point_System.instance.GameStarted = true;
    }

    public IEnumerator Stun()
    {
        StunVFX.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        StunVFX.SetActive(false);
    }
}
