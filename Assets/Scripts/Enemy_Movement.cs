using FMOD.Studio;
using UnityEngine;
using UnityEngine.AI;
using FMODUnity;
using System;

[Serializable]
public class Enemy_Mouvement : MonoBehaviour
{
    public Rigidbody DoctorRB;
    public BoxCollider DeathCollider;
    public EventReference DestorySound;
    private EventInstance DestroySoundInstance;
    private bool CanMove = false;
    public Animator DoctorAnim;
    public static Enemy_Mouvement instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        CanMove = false;
        DoctorRB = GetComponent<Rigidbody>();
        DestroySoundInstance = RuntimeManager.CreateInstance(DestorySound);
    }

    private void FixedUpdate()
    {
        if(CanMove)
        {
            DoctorRB.linearVelocity = new Vector3(0f, 0f, 3.5f);
        }
    }
    public void StartMoving()
    {
        DoctorAnim.SetTrigger("Start");
        CanMove = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
        }
    }
}
