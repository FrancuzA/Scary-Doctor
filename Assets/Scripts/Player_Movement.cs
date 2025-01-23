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
    [SerializeField] private GameObject SlidingCollider;
    [SerializeField] private float AnimSpeed;
    public Animator RunAnim;
    public float playerHeight;
    public LayerMask Ground;
    public LayerMask obstacle;
    public bool IsDead = false;
    public bool IsOnGround = false;
    public bool IsSomethingBlocking;
    private bool IsSliding = false;
    private bool isPlaying = false; // Tracks if the sound is currently playing
    public EventReference soundEvent; // The FMOD event to play in a loop
    private EventInstance soundInstance; // The FMOD event instance
    public MusicPlayer Mplayer;
    public GameObject DeathUI;
    public Animator Animator;


    void Start()
    {
        RunAnim.speed = AnimSpeed;
        PlayerRigidbody = GetComponent<Rigidbody>();
        soundInstance = RuntimeManager.CreateInstance(soundEvent);
    }


    void Update()
    {
        Debug.Log("Jumping? " + Animator.GetBool("IsJumping"));
        if (Input.GetKeyDown(KeyCode.S) && IsSliding == false) { StartSlide(); }
        if (Input.GetKeyDown(KeyCode.W) && IsOnGround == true && IsSomethingBlocking == false)
        {
            StopSlide();
            StartCoroutine(UpdateJumpFallState());
        }
        if (IsOnGround == true && isPlaying == false && PlayerRigidbody.linearVelocity.x > 0) { PlaySound(); }
        if (IsOnGround == false && isPlaying == true) { StopSound(); }
        if (Input.GetKeyDown(KeyCode.S) && IsSliding == false) { StartSlide(); }

        IsOnGround = Physics.Raycast(playerObj.transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);
        if (IsOnGround)
        {
            Debug.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.2f), Color.green); // Ground hit
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.2f), Color.red); // No ground hit
        }
        IsSomethingBlocking = Physics.Raycast(SlidingCollider.transform.position - new Vector3(0, 0.5f, 0), Vector3.up, playerHeight, obstacle);
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = PlayerRigidbody.position + Vector3.forward * Speed * Time.deltaTime;

        PlayerRigidbody.MovePosition(newPosition);
        if (IsSliding == true) { SlideMovement(); }
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
        SlidingCollider.SetActive(true);
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        slideTimer = maxSlideTime;
    }
    private void SlideMovement()
    {
        PlayerRigidbody.AddForce(Vector3.forward * slideForcce, ForceMode.Force);

        slideTimer -= Time.deltaTime;
        if (slideTimer <= 0 && IsSomethingBlocking == false) { StopSlide(); }
    }

    private void StopSlide()
    {
        IsSliding = false;
        this.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        SlidingCollider.SetActive(false);
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
        Mplayer.currentTrack.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        soundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        Enemy_Mouvement.soundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        DeathUI.SetActive(true);
        Time.timeScale = 0f;
    }


    private IEnumerator UpdateJumpFallState()
    {
        Animator.SetBool("IsRunning", false);
        StopSlide();
        Animator.SetBool("IsJumping", true);
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        PlayerRigidbody.linearVelocity = new Vector3(PlayerRigidbody.linearVelocity.x, 0f, PlayerRigidbody.linearVelocity.z);
        PlayerRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        Debug.Log("JUmping");
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;

        yield return new WaitUntil(() => IsOnGround == true);
        Debug.Log("IsOnGround");
        Animator.SetBool("IsJumping", false);
        Animator.SetBool("IsFalling", true);
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        /*while (!IsOnGround)
        {
            if (PlayerRigidbody.linearVelocity.y < 0 && !Animator.GetBool("IsFalling"))
            {
                Animator.SetBool("IsJumping", false);
                Animator.SetBool("IsFalling", true);
                Debug.Log("Falling");
            }
            yield return null;
        }*/

        Animator.SetBool("IsFalling", false);
        Animator.SetBool("IsRunning", true);
        Debug.Log("Landed");
    }
}
