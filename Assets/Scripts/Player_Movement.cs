using FMOD.Studio;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using FMODUnity;
using UnityEditor.SearchService;
using System;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private Transform playerObj;
    [SerializeField] private Rigidbody PlayerRigidbody;
    [SerializeField] private float JumpForce;
    [SerializeField] private float Speed;
    [SerializeField] private float maxSlideTime;
    [SerializeField] private float slideForcce;
    [SerializeField] private float slideTimer;
    [SerializeField] private float slideYscale;
    private float startYScale;
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool IsDead = false;
    public bool IsOnGround = false;
    private bool IsSliding = false;
    private bool isPlaying = false; // Tracks if the sound is currently playing
    public EventReference soundEvent; // The FMOD event to play in a loop
    private EventInstance soundInstance; // The FMOD event instance


    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
        soundInstance = RuntimeManager.CreateInstance(soundEvent);
        startYScale = playerObj.localScale.y;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && IsOnGround == true)
        {
            IsOnGround = false;
            StopSlide();
            Jumping();
        }
        if (IsOnGround==true && isPlaying == false) {PlaySound();}
        if(IsOnGround==false && isPlaying == true) { StopSound();}
        if (Input.GetKeyDown(KeyCode.S) && IsSliding == false) { StartSlide(); }

        IsOnGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    }

    private void FixedUpdate()
    {
        PlayerRigidbody.linearVelocity = new Vector3(0f,PlayerRigidbody.linearVelocity.y,Speed);

        if(Input.GetKeyDown(KeyCode.S) && IsSliding == false) {StartSlide();}

        if (IsSliding == true) { SlideMovement();}
    }


    private void Jumping()
    {
        PlayerRigidbody.linearVelocity = new Vector3(PlayerRigidbody.linearVelocity.x, 0f, PlayerRigidbody.linearVelocity.z);
        PlayerRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        Debug.Log("JUmping");
        
    }

    private void PlaySound()
    {
        soundInstance.start();
        isPlaying = true;
    }

    private void StopSound()
    {
        soundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        isPlaying = false;
    }

    private void OnDestroy()
    {
        // Clean up the FMOD event instance when the object is destroyed
        if (soundInstance.isValid())
        {
            soundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            soundInstance.release();
        }
    }

    private void StartSlide() 
    {
        IsSliding = true;
        playerObj.localScale = new Vector3(playerObj.localScale.x,slideYscale,playerObj.localScale.z);
        PlayerRigidbody.AddForce(Vector3.down * 50f, ForceMode.Impulse);
        slideTimer = maxSlideTime;
    }
    private void SlideMovement()
    {
        PlayerRigidbody.AddForce(Vector3.forward * slideForcce, ForceMode.Force);

        slideTimer -= Time.deltaTime;
        if (slideTimer <= 0) { StopSlide(); }
    }

    private void StopSlide()
    {
        IsSliding = false;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }

}
