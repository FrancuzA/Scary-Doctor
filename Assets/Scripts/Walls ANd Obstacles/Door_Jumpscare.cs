using UnityEngine;

public class Door_Jumpscare : MonoBehaviour
{
    public Animator DoorAnim;
    private void Awake()
    {
        float RNG = Random.Range(0f, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float RNG = Random.Range(0f, 10f);
            if (RNG <= 3f) 
            { 
             DoorAnim.SetBool("JumpScare", true);
            }
        }
    }
}
