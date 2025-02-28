using FMOD.Studio;
using System.Collections;
using UnityEngine;
using FMODUnity;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private Transform playerObj;
    [SerializeField] private Rigidbody PlayerRigidbody;
    [SerializeField] private float JumpForce;
    [SerializeField] private float Speed;
    [SerializeField] private float maxSlideTime;
    [SerializeField] private float slideForcce;
    [SerializeField] private float slideTimer;
    [SerializeField] private float gettingUpAnimationLength = 1.0f;
    [SerializeField] private GameObject SlidingCollider;
    [SerializeField] private float AnimSpeed;
    private int NextMileStone = 10;
    private float CurrentDistance = 0;
    public Animator characterAnim;
    public float playerHeight;
    public LayerMask Ground;
    public LayerMask obstacle;
    private Coroutine gettingUpRoutine;
    private bool isJumping = false;
    public bool IsDead = false;
    public bool IsOnGround = false;
    public bool IsSomethingBlocking;
    private bool IsSliding = false;
    private bool isPlaying = false; // Tracks if the sound is currently playing
    public EventReference soundEvent; // The FMOD event to play in a loop
    public EventReference soundDeath;
    public EventReference soundJump;
    private EventInstance soundInstance; // The FMOD event instance
    private EventInstance DeathSoundInstance;
    private EventInstance JumpSoundinstance;
    public MusicPlayer Mplayer;
    public GameObject DeathUI;


    void Start()
    {
        characterAnim.speed = AnimSpeed;
        PlayerRigidbody = GetComponent<Rigidbody>();
        soundInstance = RuntimeManager.CreateInstance(soundEvent);
        DeathSoundInstance = RuntimeManager.CreateInstance(soundDeath);
        JumpSoundinstance = RuntimeManager.CreateInstance(soundJump);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !IsSliding && characterAnim.GetBool("IsRunning")) { StartSlide(); }
        if (Input.GetKeyDown(KeyCode.W) && IsOnGround && !IsSomethingBlocking && !isJumping && !characterAnim.GetBool("IsGettingUp"))
        {
            if (gettingUpRoutine != null)
            {
                StopCoroutine(gettingUpRoutine);
                gettingUpRoutine = null;
            }
            StopSlide(true);
            StartCoroutine(JumpRoutine());
        }
        IsOnGround = Physics.Raycast(playerObj.transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);
        characterAnim.SetBool("IsGrounded", IsOnGround);
        if (IsOnGround == true && isPlaying == false && PlayerRigidbody.linearVelocity.x > 0) { PlaySound(); }
        if (IsOnGround == false && isPlaying == true) { StopSound(); }
        if (PlayerRigidbody.transform.position.y < -1) { Death(); }
        IsSomethingBlocking = Physics.Raycast(SlidingCollider.transform.position - new Vector3(0, 0.5f, 0), Vector3.up, playerHeight, obstacle);
        SpeedUpGame();
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = PlayerRigidbody.position + Vector3.forward * Speed * Time.deltaTime;
        PlayerRigidbody.MovePosition(newPosition);

        if (IsSliding)
        {
            SlideMovement();
        }

    }

    private IEnumerator GettingUpRoutine()
    {

        characterAnim.SetBool("IsGettingUp", true);
        characterAnim.SetBool("IsRunning", false);
        float timer = 0;
        while (timer < gettingUpAnimationLength)
        {
            if (isJumping) yield break;
            timer += Time.deltaTime;
            yield return null;
        }

        characterAnim.SetBool("IsGettingUp", false);
        characterAnim.SetBool("IsRunning", true);
        gettingUpRoutine = null;
    }

    private IEnumerator JumpRoutine()
    {
        isJumping = true;
        characterAnim.SetBool("IsRunning", false);
        characterAnim.SetBool("IsJumping", true);
        if (gettingUpRoutine != null)
        {
            StopCoroutine(gettingUpRoutine);
            gettingUpRoutine = null;
        }
        if (IsSliding) StopSlide(true);

        // Jump physics
        PlayerRigidbody.linearVelocity = new Vector3(PlayerRigidbody.linearVelocity.x, 0f, PlayerRigidbody.linearVelocity.z);
        PlayerRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        JumpSoundinstance.start();

        // Wait for landing
        yield return new WaitUntil(() => IsOnGround);
        yield return new WaitForFixedUpdate();
        // Landing logic
        characterAnim.SetBool("IsJumping", false);
        characterAnim.SetBool("IsLanding", true);
        yield return new WaitUntil(() => characterAnim.GetCurrentAnimatorStateInfo(0).IsName("rig|Landing_Animation"));
        characterAnim.SetBool("IsLanding", false);
        characterAnim.SetBool("IsRunning", true);
        isJumping = false;
    }

    private void StopSlide(bool fromJump = false)
    {
        IsSliding = false;
        characterAnim.SetBool("IsSliding", false);
        GetComponent<CapsuleCollider>().enabled = true;
        SlidingCollider.SetActive(false);

        if (!fromJump && !isJumping)
        {
            if (gettingUpRoutine != null) StopCoroutine(gettingUpRoutine);
            gettingUpRoutine = StartCoroutine(GettingUpRoutine());
        }
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
        if (soundInstance.isValid())
        {
            soundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            soundInstance.release();
        }
    }

    private void StartSlide()
    {
        characterAnim.SetBool("IsRunning", false);
        characterAnim.SetBool("IsGettingUp", false);
        IsSliding = true;
        characterAnim.SetBool("IsSliding", true);
        SlidingCollider.SetActive(true);
        GetComponent<CapsuleCollider>().enabled = false;
        slideTimer = maxSlideTime;
    }
    private void SlideMovement()
    {
        PlayerRigidbody.AddForce(Vector3.forward * slideForcce, ForceMode.Force);

        slideTimer -= Time.deltaTime;
        if (slideTimer <= 0 && !IsSomethingBlocking && IsSliding) { StopSlide(); }
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
        Debug.Log("Distance "+CurrentDistance + " Milestone "+ NextMileStone+ " Current speed " +Speed+ " Log od speed " + 2*Mathf.Log(Speed, 2));
    }
   
}
