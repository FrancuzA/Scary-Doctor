using FMOD.Studio;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using FMODUnity;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody PlayerRigidbody;
    [SerializeField] private float JumpForce;
    [SerializeField] private float Speed;
    public bool IsDead = false;
    private bool IsJumping = false;
    public EventReference soundEvent; // The FMOD event to play in a loop
    private EventInstance soundInstance; // The FMOD event instance
    private bool isPlaying = false;     // Tracks if the sound is currently playing


    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
        soundInstance = RuntimeManager.CreateInstance(soundEvent);
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsJumping==false) 
        {
         StartCoroutine(Jumping());
        }

        if(IsJumping==false && isPlaying == false) {PlaySound();}
        if(IsJumping==true && isPlaying == true) { StopSound();}
    }

    private void FixedUpdate()
    {
        PlayerRigidbody.linearVelocity = new Vector3(0f,0f,Speed);
    }


    IEnumerator Jumping()
    {
        IsJumping = true;
        PlayerRigidbody.AddForce(new Vector3(0, JumpForce, 0));
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => Mathf.Abs(PlayerRigidbody.linearVelocity.y)<= 0.01);
        IsJumping = false;
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
}
