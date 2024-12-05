using UnityEngine;
using UnityEngine.AI;

public class Enemy_Mouvement : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent Doctor_NavAgent;

    private void Start()
    {
        DoctorDestination();
    }

    private void FixedUpdate()
    {
        DoctorDestination();
    }


    public void DoctorDestination()
    {
     Doctor_NavAgent.SetDestination(player.transform.position);
    }

}
