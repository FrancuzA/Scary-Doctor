using UnityEngine;

public class Door_Jumpscare : MonoBehaviour
{
    private void Awake()
    {
        float RNG = Random.Range(0f, 10f);
        if (RNG <= 3f) { Debug.Log("BOOOO"); };
    }
}
