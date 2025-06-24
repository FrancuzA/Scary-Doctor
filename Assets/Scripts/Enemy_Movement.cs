using FMOD.Studio;
using UnityEngine;
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
    private float Speed;
    public GameObject OverBoy;
    public GameObject DestroyVFX;
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
        Speed = 3.4f;
        CanMove = false;
        DoctorRB = GetComponent<Rigidbody>();
        DestroySoundInstance = RuntimeManager.CreateInstance(DestorySound);
    }
    public void Update()
    {
        if (CanMove)
        {
            DoctorRB.linearVelocity = new Vector3(0f, 0f, Speed);
        }
    }
    public void StartMoving()
    {
        DoctorAnim.SetTrigger("Start");
        Player_Manager.instance.MonsterRunEmitter.enabled = true;
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
        yield return new WaitForSeconds(0.4f);
        DestroyVFX.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Destroy(obstacle);
        yield return new WaitForSeconds(0.5f);
        DestroyVFX.SetActive(false);
    }

    public void _ResetNumberToZero()
    {
        DoctorAnim.SetInteger("RandAnim", 0);
    }

    public void GameOver()
    {
        StartCoroutine(DeatSequance());
    }

    public IEnumerator DeatSequance()
    {
        Speed = 0f;
        yield return new WaitForSeconds(0.5f);
        DoctorAnim.SetTrigger("GameOver");
        OverBoy.SetActive(true);
    }
}
