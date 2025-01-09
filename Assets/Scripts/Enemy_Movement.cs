using FMOD.Studio;
using UnityEngine;
using UnityEngine.AI;
using FMODUnity;

public class Enemy_Mouvement : MonoBehaviour
{
    public Rigidbody DoctorRB;
    public BoxCollider DeathCollider;
    public EventReference soundEvent; // The FMOD event to play in a loop
    private EventInstance soundInstance; // The FMOD event instance

    private void Start()
    {
        DoctorRB = GetComponent<Rigidbody>();
        soundInstance = RuntimeManager.CreateInstance(soundEvent);
        soundInstance.start();
    }

    private void FixedUpdate()
    {
        DoctorRB.linearVelocity = new Vector3(0f, 0f, 3.5f);
    }

}
