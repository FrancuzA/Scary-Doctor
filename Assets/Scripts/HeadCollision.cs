using UnityEngine;

public class HeadCollision : MonoBehaviour
{
    public Animator ANim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collision"))
        {
            StartCoroutine(Player_Manager.instance.Stun());
            ANim.SetTrigger("HeadCollision");
        }
    }
}
