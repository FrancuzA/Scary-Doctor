using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    public Rigidbody rb;
    public int force;
    public EventReference pushSoundRef;
    private EventInstance pushSoundInst;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Push();
        }
    }

    public void Push()
    {
        pushSoundInst = RuntimeManager.CreateInstance(pushSoundRef);
        pushSoundInst.start();
        rb.AddForce(new Vector3(0f, 0f, force), ForceMode.Impulse);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        pushSoundInst.release();
    }
}
