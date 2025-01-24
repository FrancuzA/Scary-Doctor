using FMOD.Studio;
using UnityEngine;
using UnityEngine.AI;
using FMODUnity;

public class Enemy_Mouvement : MonoBehaviour
{
    public Rigidbody DoctorRB;
    public BoxCollider DeathCollider;
    public EventReference soundEvent; // The FMOD event to play in a loop
    public EventReference DestorySound;
    private EventInstance DestroySoundInstance;
    public static EventInstance soundInstance; // The FMOD event instance

    private void Start()
    {
        DoctorRB = GetComponent<Rigidbody>();
        soundInstance = RuntimeManager.CreateInstance(soundEvent);
        DestroySoundInstance = RuntimeManager.CreateInstance(DestorySound);
    }

    private void FixedUpdate()
    {
        DoctorRB.linearVelocity = new Vector3(0f, 0f, 3.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            DestroySoundInstance.start();
            Destroy(other.gameObject);
        }
    }
}
