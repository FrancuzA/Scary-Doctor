using Unity.VisualScripting;
using UnityEngine;

public class Deloader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            
            //Destroy(other.gameObject);
            Debug.Log("deloading...");
        }
    }
}
