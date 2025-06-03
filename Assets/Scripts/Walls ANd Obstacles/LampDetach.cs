using UnityEngine;

public class LampDetach : MonoBehaviour
{
    public Animator Anim;

    private void Start()
    {
        Anim = GetComponent<Animator>();    
    }
    public void RollForLampFall()
    {
        float RNG = (float)RNG_Custom.random.NextDouble();
        if (RNG <= 0.1) { Anim.SetBool("IsFalling", true); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           RollForLampFall();
        }
    }
}
