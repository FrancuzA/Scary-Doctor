using FMOD.Studio;
using UnityEngine;
using UnityEngine.AI;
using FMODUnity;

public class Enemy_Mouvement : MonoBehaviour
{
    public static Rigidbody DoctorRB;
    public BoxCollider DeathCollider;
    public EventReference MonsterRunEvent; // The FMOD event to play in a loop
    public EventReference DestorySound;
    private EventInstance DestroySoundInstance;
    public static EventInstance MonsterRunInstance; // The FMOD event instance
    private static bool CanMove = false;

    private void Start()
    {
        CanMove = false;
        DoctorRB = GetComponent<Rigidbody>();
        MonsterRunInstance = RuntimeManager.CreateInstance(MonsterRunEvent);
        DestroySoundInstance = RuntimeManager.CreateInstance(DestorySound);
    }

    private void FixedUpdate()
    {
        if(CanMove)
        {
            DoctorRB.linearVelocity = new Vector3(0f, 0f, 3.5f);
        }
    }
    public static void StartMoving()
    {
        CanMove = true;
        MonsterRunInstance.start();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
        }
    }
}
