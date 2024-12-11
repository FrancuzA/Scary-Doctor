using UnityEngine;
using UnityEngine.AI;

public class Enemy_Mouvement : MonoBehaviour
{
    public Rigidbody DoctorRB;
    public BoxCollider DeathCollider;

    private void Start()
    {
        DoctorRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        DoctorRB.linearVelocity = new Vector3(0f, 0f, 3.5f);
    }

}
