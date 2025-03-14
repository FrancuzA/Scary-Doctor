using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class ObstaclesScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * 3f, ForceMode.Impulse);
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 3f, ForceMode.Impulse);
        }
    }
}
