using UnityEngine;

public class FootCollision : MonoBehaviour
{
    public Animator ANim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collision"))
        {
            Debug.Log("collision detected foot");
            ANim.SetTrigger("FootCollision");
        }
    }
}
