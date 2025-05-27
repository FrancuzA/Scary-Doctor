using FMOD.Studio;
using UnityEngine;
using UnityEngine.AI;
using FMODUnity;
using System;
using System.Collections;

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
          StartCoroutine(DestroyObstacle(other.gameObject));
        }
    }

    
    public IEnumerator DestroyObstacle(GameObject obstacle)
    {
        int AnimNum = RNG_Custom.random.Next(1, 3);
        DoctorAnim.SetInteger("RandAnim", AnimNum);
        yield return new WaitForSeconds(0.9f);
        Destroy(obstacle);
    }

    public void _ResetNumberToZero()
    {
        DoctorAnim.SetInteger("RandAnim", 0);
    }
}
